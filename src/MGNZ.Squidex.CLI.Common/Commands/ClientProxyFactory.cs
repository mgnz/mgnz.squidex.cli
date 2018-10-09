namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System;
  using System.Collections.Generic;
  using System.Net.Http;
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client;
  using MGNZ.Squidex.Client.Handlers;
  using MGNZ.Squidex.Client.Transport;
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

    private readonly Dictionary<string, Dictionary<Type, object>> _aliasKeyedEndpointWithClients;

    private readonly
      Dictionary<string, (ISquidexOAuthClient client, string accessToken, int expiresIn, string tokenType)>
      _aliasKeyedOAuthClientCache;

    public ClientProxyFactory(ILogger logger, ApplicationConfiguration configuration)
    {
      _logger = logger;
      _configuration = configuration;

      _aliasKeyedEndpointWithClients = new Dictionary<string, Dictionary<Type, object>>();
      _aliasKeyedOAuthClientCache =
        new Dictionary<string, (ISquidexOAuthClient client, string accessToken, int expiresIn, string tokenType)>();
    }

    public TProxy GetClientProxy<TProxy>(string aliasCredentials)
      where TProxy : class
    {
      var proxy = GetOrCreateProxy<TProxy>(aliasCredentials);

      return proxy;
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
        endpointClients.Add(newEndpointClient.GetType(), newEndpointClient);

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
      return RestService.For<TProxy>(
        new HttpClient(
          new SimpleAccessTokenHttpClientHandler(
            () => GetOrRefreshOAuthToken(aliasCredentials)))
            {
              BaseAddress = baseAddress
            });
    }

    private async Task<string> GetOrRefreshOAuthToken(string aliasCredentials)
    {
      var settings = GetAliasCredentials(aliasCredentials);

      if (_aliasKeyedOAuthClientCache.ContainsKey(aliasCredentials))
      {
        // todo : log auth client in cache

        var existingClientDetails = _aliasKeyedOAuthClientCache[aliasCredentials];
        if (KeyNeedsRefreshed(existingClientDetails))
        {
          // todo : log auth token expired; refreshing

          var refreshedToken = await RefreshToken(existingClientDetails.client, settings.oauthAppName,
            settings.oauthClientId, settings.oauthClientSecret);

          existingClientDetails.accessToken = refreshedToken.accessToken;
          existingClientDetails.expiresIn = refreshedToken.expiresIn;
          existingClientDetails.tokenType = refreshedToken.tokenType;

          _aliasKeyedOAuthClientCache[aliasCredentials] = existingClientDetails;
        }

        return existingClientDetails.accessToken;
      }
      else
      {
        // todo : log auth client not in cache; obtaining token

        var client = RestService.For<ISquidexOAuthClient>(settings.baseAddress.AbsoluteUri);
        var refreshedToken = await RefreshToken(client, settings.oauthAppName, settings.oauthClientId,
          settings.oauthClientSecret);

        _aliasKeyedOAuthClientCache.Add(aliasCredentials,
          (client, refreshedToken.accessToken, refreshedToken.expiresIn, refreshedToken.tokenType));

        return _aliasKeyedOAuthClientCache[aliasCredentials].accessToken;
      }
    }

    private bool KeyNeedsRefreshed(
      (ISquidexOAuthClient client, string accessToken, int expiresIn, string tokenType) existingClientDetails)
    {
      return true;
    }

    private (Uri baseAddress, string oauthAppName, string oauthClientId, string oauthClientSecret) GetAliasCredentials(
      string aliasCredentials)
    {
      if (!_configuration.AliasCredentials.ContainsKey(aliasCredentials))
        throw new KeyNotFoundException(); // todo : message and log

      var credentials = _configuration.AliasCredentials[aliasCredentials];

      return (credentials.BaseAddress, credentials.App, credentials.ClientId, credentials.ClientSecret);
    }

    private async Task<(string accessToken, int expiresIn, string tokenType)> RefreshToken(
      ISquidexOAuthClient oauthClient, string oauthAppName, string oauthClientId, string oauthClientSecret)
    {
      var response = await oauthClient.GetToken(new GetOAuthTokenRequest
      {
        ClientId = $"{oauthAppName}:{oauthClientId}",
        ClientSecret = oauthClientSecret
      });

      return (response.AccessToken, response.ExpiresIn, response.TokenType);
    }
  }
}