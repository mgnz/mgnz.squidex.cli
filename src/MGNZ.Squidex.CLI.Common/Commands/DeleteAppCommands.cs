namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System;
  using System.Threading.Tasks;

  using Autofac;

  using MGNZ.Squidex.CLI.Common.CLI;

  using Serilog.Core;

  public interface ICommand
  {
    Task Execute();
  }

  public class BaseCommand<TArguments> : ICommand
    where TArguments : BaseArguments
  {
    public BaseCommand(Logger logger, IContainer container, TArguments arguments)
    {
      this.Logger = logger;
      this.Container = container;
      this.Arguments = arguments;
    }

    protected Logger Logger { get; }
    protected IContainer Container { get; }
    protected TArguments Arguments { get; }

    public virtual async Task Execute()
    {
      throw new NotImplementedException();
    }
  }

  public class DeleteAppCommand : BaseCommand<DeleteAppArguments>
  {
    public DeleteAppCommand(Logger logger, IContainer container, DeleteAppArguments arguments) : base(logger, container,
      arguments)
    {
    }

    public async Task Execute()
    {
      throw new NotImplementedException();
    }
  }

  public class DeleteAssetCommand : BaseCommand<DeleteAssetArguments>
  {
    public DeleteAssetCommand(Logger logger, IContainer container, DeleteAssetArguments arguments) : base(logger,
      container, arguments)
    {
    }

    public async Task Execute()
    {
      throw new NotImplementedException();
    }
  }

  public class DeleteContentCommand : BaseCommand<DeleteContentArguments>
  {
    public DeleteContentCommand(Logger logger, IContainer container, DeleteContentArguments arguments) : base(logger,
      container, arguments)
    {
    }

    public async Task Execute()
    {
      throw new NotImplementedException();
    }
  }

  public class DeleteSchemaCommand : BaseCommand<DeleteSchemaArguments>
  {
    public DeleteSchemaCommand(Logger logger, IContainer container, DeleteSchemaArguments arguments) : base(logger,
      container, arguments)
    {
    }

    public async Task Execute()
    {
      throw new NotImplementedException();
    }
  }

  public class ListAppCommand : BaseCommand<ListAppArguments>
  {
    public ListAppCommand(Logger logger, IContainer container, ListAppArguments arguments) : base(logger, container,
      arguments)
    {
    }

    public async Task Execute()
    {
      throw new NotImplementedException();
    }
  }

  public class ListAssetCommand : BaseCommand<ListAssetArguments>
  {
    public ListAssetCommand(Logger logger, IContainer container, ListAssetArguments arguments) : base(logger, container,
      arguments)
    {
    }

    public async Task Execute()
    {
      throw new NotImplementedException();
    }
  }

  public class ListSchemaCommand : BaseCommand<ListSchemaArguments>
  {
    public ListSchemaCommand(Logger logger, IContainer container, ListSchemaArguments arguments) : base(logger,
      container, arguments)
    {
    }

    public async Task Execute()
    {
      throw new NotImplementedException();
    }
  }

  public class LoginAppCommand : BaseCommand<LoginAppArguments>
  {
    public LoginAppCommand(Logger logger, IContainer container, LoginAppArguments arguments) : base(logger, container,
      arguments)
    {
    }

    public async Task Execute()
    {
      throw new NotImplementedException();
    }
  }

  public class LogoutAppCommand : BaseCommand<LogoutAppArguments>
  {
    public LogoutAppCommand(Logger logger, IContainer container, LogoutAppArguments arguments) : base(logger, container,
      arguments)
    {
    }

    public async Task Execute()
    {
      throw new NotImplementedException();
    }
  }

  public class NewAppCommand : BaseCommand<NewAppArguments>
  {
    public NewAppCommand(Logger logger, IContainer container, NewAppArguments arguments) : base(logger, container,
      arguments)
    {
    }

    public async Task Execute()
    {
      throw new NotImplementedException();
    }
  }

  public class ImportAssetCommand : BaseCommand<ImportAssetArguments>
  {
    public ImportAssetCommand(Logger logger, IContainer container, ImportAssetArguments arguments) : base(logger,
      container, arguments)
    {
    }

    public async Task Execute()
    {
      throw new NotImplementedException();
    }
  }

  public class ImportContentCommand : BaseCommand<ImportContentArguments>
  {
    public ImportContentCommand(Logger logger, IContainer container, ImportContentArguments arguments) : base(logger,
      container, arguments)
    {
    }

    public async Task Execute()
    {
      throw new NotImplementedException();
    }
  }

  public class ImportSchemaCommand : BaseCommand<ImportSchemaArguments>
  {
    public ImportSchemaCommand(Logger logger, IContainer container, ImportSchemaArguments arguments) : base(logger,
      container, arguments)
    {
    }

    public async Task Execute()
    {
      throw new NotImplementedException();
    }
  }

  public class ExportAssetCommand : BaseCommand<ExportAssetArguments>
  {
    public ExportAssetCommand(Logger logger, IContainer container, ExportAssetArguments arguments) : base(logger,
      container, arguments)
    {
    }

    public async Task Execute()
    {
      throw new NotImplementedException();
    }
  }

  public class ExportContentCommand : BaseCommand<ExportContentArguments>
  {
    public ExportContentCommand(Logger logger, IContainer container, ExportContentArguments arguments) : base(logger,
      container, arguments)
    {
    }

    public async Task Execute()
    {
      throw new NotImplementedException();
    }
  }

  public class ExportSchemaCommand : BaseCommand<ExportSchemaArguments>
  {
    public ExportSchemaCommand(Logger logger, IContainer container, ExportSchemaArguments arguments) : base(logger,
      container, arguments)
    {
    }

    public async Task Execute()
    {
      throw new NotImplementedException();
    }
  }

  public class TagAssetCommand : BaseCommand<TagAssetArguments>
  {
    public TagAssetCommand(Logger logger, IContainer container, TagAssetArguments arguments) : base(logger, container,
      arguments)
    {
    }

    public async Task Execute()
    {
      throw new NotImplementedException();
    }
  }
}