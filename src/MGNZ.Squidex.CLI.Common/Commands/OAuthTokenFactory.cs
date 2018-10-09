namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client;
  using MGNZ.Squidex.Client.Transport;
  using MGNZ.Squidex.CLI.Common.Configuration;

  using Refit;

  using Serilog;

  public interface IOAuthTokenFactory
  {
    Task<string> GetOrRefreshOAuthToken(string aliasCredentials);
  }

  public class OAuthTokenFactory : IOAuthTokenFactory
  {
    private readonly ApplicationConfiguration _configuration;
    private readonly ILogger _logger;

    private readonly Dictionary<string, (ISquidexOAuthClient client, string accessToken, int expiresIn, string tokenType)> _aliasKeyedOAuthClientCache;

    public OAuthTokenFactory(ILogger logger, ApplicationConfiguration configuration)
    {
      _logger = logger;
      _configuration = configuration;

      _aliasKeyedOAuthClientCache = new Dictionary<string, (ISquidexOAuthClient client, string accessToken, int expiresIn, string tokenType)>();
    }

    public async Task<string> GetOrRefreshOAuthToken(string aliasCredentials)
    {
      var settings = GetAliasCredentials(aliasCredentials);

      if (_aliasKeyedOAuthClientCache.ContainsKey(aliasCredentials))
      {
        // todo : log auth client in cache

        var existingClientDetails = _aliasKeyedOAuthClientCache[aliasCredentials];
        if (KeyNeedsRefreshed(existingClientDetails))
        {
          // todo : log auth token expired; refreshing

          var refreshedToken = await RefreshToken(existingClientDetails.client, settings.oauthAppName, settings.oauthClientId, settings.oauthClientSecret);

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

    private bool KeyNeedsRefreshed((ISquidexOAuthClient client, string accessToken, int expiresIn, string tokenType) existingClientDetails)
    {
      return true;
    }

    private (Uri baseAddress, string oauthAppName, string oauthClientId, string oauthClientSecret) GetAliasCredentials(string aliasCredentials)
    {
      if (!_configuration.AliasCredentials.ContainsKey(aliasCredentials))
        throw new KeyNotFoundException(); // todo : message and log

      var credentials = _configuration.AliasCredentials[aliasCredentials];

      return (credentials.BaseAddress, credentials.App, credentials.ClientId, credentials.ClientSecret);
    }

    private async Task<(string accessToken, int expiresIn, string tokenType)> RefreshToken(ISquidexOAuthClient oauthClient, string oauthAppName, string oauthClientId, string oauthClientSecret)
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