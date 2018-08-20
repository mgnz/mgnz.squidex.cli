namespace MGNZ.Squidex.CLI.Common.Commands
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Autofac;

  using MediatR;

  using MGNZ.Squidex.CLI.Common.CLI;

  using Serilog.Core;

  public class BaseHandler<TRequest> : IRequestHandler<TRequest>
    where TRequest : IRequest<Unit>
  {
    public BaseHandler(Logger logger, IContainer container)
    {
      Logger = logger;
      Container = container;
    }

    protected Logger Logger { get; }
    protected IContainer Container { get; }

    /// <inheritdoc />
    public virtual Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }

  public class DeleteAppHandler : BaseHandler<DeleteAppRequest>
  {
    public DeleteAppHandler(Logger logger, IContainer container, DeleteAppRequest request)
      : base(logger, container)
    {
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(DeleteAppRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class DeleteAssetHandler : BaseHandler<DeleteAssetRequest>
  {
    public DeleteAssetHandler(Logger logger, IContainer container, DeleteAssetRequest request)
      : base(logger,
      container)
    {
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(DeleteAssetRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class DeleteContentHandler : BaseHandler<DeleteContentRequest>
  {
    public DeleteContentHandler(Logger logger, IContainer container, DeleteContentRequest request)
      : base(logger,
      container)
    {
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(DeleteContentRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class DeleteSchemaHandler : BaseHandler<DeleteSchemaRequest>
  {
    public DeleteSchemaHandler(Logger logger, IContainer container, DeleteSchemaRequest request)
      : base(logger,
      container)
    {
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(DeleteSchemaRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class ListAppHandler : BaseHandler<ListAppRequest>
  {
    public ListAppHandler(Logger logger, IContainer container, ListAppRequest request)
      : base(logger, container)
    {
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(ListAppRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class ListAssetHandler : BaseHandler<ListAssetRequest>
  {
    public ListAssetHandler(Logger logger, IContainer container, ListAssetRequest request)
      : base(logger, container)
    {
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(ListAssetRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class ListSchemaHandler : BaseHandler<ListSchemaRequest>
  {
    public ListSchemaHandler(Logger logger, IContainer container, ListSchemaRequest request)
      : base(logger,
      container)
    {
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(ListSchemaRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class LoginAppHandler : BaseHandler<LoginAppRequest>
  {
    public LoginAppHandler(Logger logger, IContainer container, LoginAppRequest request)
      : base(logger, container)
    {
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(LoginAppRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class LogoutAppHandler : BaseHandler<LogoutAppRequest>
  {
    public LogoutAppHandler(Logger logger, IContainer container, LogoutAppRequest request)
      : base(logger, container)
    {
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(LogoutAppRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class NewAppHandler : BaseHandler<NewAppRequest>
  {
    public NewAppHandler(Logger logger, IContainer container, NewAppRequest request)
      : base(logger, container)
    {
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(NewAppRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class ImportAssetHandler : BaseHandler<ImportAssetRequest>
  {
    public ImportAssetHandler(Logger logger, IContainer container, ImportAssetRequest request)
      : base(logger,
      container)
    {
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(ImportAssetRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class ImportContentHandler : BaseHandler<ImportContentRequest>
  {
    public ImportContentHandler(Logger logger, IContainer container, ImportContentRequest request)
      : base(logger,
      container)
    {
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(ImportContentRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class ImportSchemaHandler : BaseHandler<ImportSchemaRequest>
  {
    public ImportSchemaHandler(Logger logger, IContainer container, ImportSchemaRequest request)
      : base(logger,
      container)
    {
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(ImportSchemaRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class ExportAssetHandler : BaseHandler<ExportAssetRequest>
  {
    public ExportAssetHandler(Logger logger, IContainer container, ExportAssetRequest request)
      : base(logger,
      container)
    {
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(ExportAssetRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class ExportContentHandler : BaseHandler<ExportContentRequest>
  {
    public ExportContentHandler(Logger logger, IContainer container, ExportContentRequest request)
      : base(logger,
      container)
    {
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(ExportContentRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class ExportSchemaHandler : BaseHandler<ExportSchemaRequest>
  {
    public ExportSchemaHandler(Logger logger, IContainer container, ExportSchemaRequest request)
      : base(logger,
      container)
    {
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(ExportSchemaRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }

  public class TagAssetHandler : BaseHandler<TagAssetRequest>
  {
    public TagAssetHandler(Logger logger, IContainer container, TagAssetRequest request)
      : base(logger, container)
    {
    }

    /// <inheritdoc />
    public override async Task<Unit> Handle(TagAssetRequest request, CancellationToken cancellationToken)
    {
      return await base.Handle(request, cancellationToken);
    }
  }
}