namespace MGNZ.Squidex.CLI.Common.Configuration
{
  using System;

  public class Credentials
  {
    public Uri BaseAddress { get; set; }
    public string App { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
  }
}