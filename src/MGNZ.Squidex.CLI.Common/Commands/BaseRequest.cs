namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System.Collections.Generic;

  using MediatR;

  public class BaseRequest: IRequest
  {
    public virtual List<(string property, bool isValid, string invalidReason)> Validate() => new List<(string property, bool isValid, string invalidReason)>();
  }
}
