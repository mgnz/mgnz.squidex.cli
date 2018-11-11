namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System;
  using System.Collections.Generic;
  using System.Net.Http;

  using MGNZ.Squidex.Client.Handlers;
  using MGNZ.Squidex.CLI.Common.Configuration;

  using Refit;

  using Serilog;

  public interface IClientProxyFactory
  {
    TProxy GetClientProxy<TProxy>(string aliasCredentials)
      where TProxy : class;
  }

  public class ClientProxyFactory : IClientProxyFactory
  {
    private readonly ApplicationConfiguration _configuration;
    private readonly ILogger _logger;
    private readonly IOAuthTokenFactory _oauthTokenFactory;

    private readonly Dictionary<string, Dictionary<Type, object>> _aliasKeyedEndpointWithClients;

    public ClientProxyFactory(ILogger logger, IOAuthTokenFactory oauthTokenFactory, ApplicationConfiguration configuration)
    {
      _logger = logger;
      _oauthTokenFactory = oauthTokenFactory;
      _configuration = configuration;

      _aliasKeyedEndpointWithClients = new Dictionary<string, Dictionary<Type, object>>();
    }

    public TProxy GetClientProxy<TProxy>(string aliasCredentials)
      where TProxy : class
    {
      return GetOrCreateProxy<TProxy>(aliasCredentials);
    }

    private TProxy GetOrCreateProxy<TProxy>(string aliasCredentials)
      where TProxy : class
    {
      var settings = GetAliasCredentials(aliasCredentials);

      if (_aliasKeyedEndpointWithClients.ContainsKey(aliasCredentials))
      {
        // todo : log endpoint exists

        var endpointClients = _aliasKeyedEndpointWithClients[aliasCredentials];
        if (endpointClients.ContainsKey(typeof(TProxy))) return endpointClients[typeof(TProxy)] as TProxy;
        // todo : creating client and storing

        var newEndpointClient = CreateProxy<TProxy>(aliasCredentials, settings.baseAddress);
        endpointClients[newEndpointClient.GetType()] = newEndpointClient;

        return newEndpointClient;
      }
      else
      {
        // todo : creating endpoint with client and storing

        _aliasKeyedEndpointWithClients.Add(aliasCredentials, new Dictionary<Type, object>());
        var endpointClients = _aliasKeyedEndpointWithClients[aliasCredentials];

        var newEndpointClient = CreateProxy<TProxy>(aliasCredentials, settings.baseAddress);
        endpointClients.Add(newEndpointClient.GetType(), newEndpointClient);

        return newEndpointClient;
      }
    }

    private TProxy CreateProxy<TProxy>(string aliasCredentials, Uri baseAddress)
      where TProxy : class
    {
      // todo : https://github.com/mgnz/mgnz.squidex.cli/issues/27

      return RestService.For<TProxy>(
        new HttpClient(
          new SimpleAccessTokenHttpClientHandler(
            () => _oauthTokenFactory.GetOrRefreshOAuthToken(aliasCredentials)))
            {
              BaseAddress = baseAddress
            });
    }

    private (Uri baseAddress, string oauthAppName, string oauthClientId, string oauthClientSecret) GetAliasCredentials(string aliasCredentials)
    {
      if (!_configuration.AliasCredentials.ContainsKey(aliasCredentials))
        throw new KeyNotFoundException(); // todo : message and log

      var credentials = _configuration.AliasCredentials[aliasCredentials];

      return (credentials.BaseAddress, credentials.App, credentials.ClientId, credentials.ClientSecret);
    }
  }
}