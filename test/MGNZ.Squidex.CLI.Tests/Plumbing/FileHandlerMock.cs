namespace MGNZ.Squidex.CLI.Tests.Plumbing
{
  using System;
  using System.Threading.Tasks;

  using MGNZ.Squidex.Client.Transport;
  using MGNZ.Squidex.CLI.Common.Platform;
  using MGNZ.Squidex.CLI.Tests.Platform;

  using Moq;

  public class FileHandlerMock
  {
    private Mock<IFileHandler> _mock = null;

    public FileHandlerMock(MockBehavior behavior = MockBehavior.Default)
    {
      _mock = new Mock<IFileHandler>(behavior);
    }

    public Mock<IFileHandler> Mock { get => _mock; private set => _mock = value; }

    public Function<IFileHandler, Task<QueryResponse<dynamic>>> ReadFile { get; set; } = null;
    public Method<IFileHandler> WriteFile { get; set; } = null;

    public static Function<IFileHandler, Task<QueryResponse<dynamic>>> WithReadFileAs(string path, QueryResponse<dynamic> returns = null, Exception throws = null) => new Function<IFileHandler, Task<QueryResponse<dynamic>>>()
    {
      Call = (i) => i.ReadFile(path),
      Returns = Task.FromResult(returns),
      Throws = throws
    };
    public static Function<IFileHandler, Task<QueryResponse<dynamic>>> WithReadFileAsNoOp() => new Function<IFileHandler, Task<QueryResponse<dynamic>>>()
    {
      Call = null,
      Returns = null,
      Throws = null
    };

    public static Method<IFileHandler> WithWriteFileAs(string path, Exception throws = null) => new Method<IFileHandler>()
    {
      Call = (i) => i.ReadFile(path),
      Throws = null
    };
    public static Method<IFileHandler> WithWriteFileAsNoOp() => new Method<IFileHandler>()
    {
      Call = null,
      Throws = null
    };

    public IFileHandler Build()
    {
      ReadFile?.Apply(ref _mock);
      WriteFile?.Apply(ref _mock);

      return Mock.Object;
    }
  }
}