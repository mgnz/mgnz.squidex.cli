namespace MGNZ.Squidex.CLI.Platform.Logging
{
  using System;
  using System.Linq;
  using System.Reflection;

  using Autofac;
  using Autofac.Core;
  using Autofac.Core.Activators.Reflection;

  using Serilog;

  using Module = Autofac.Module;

  internal class ContextualLoggingModule : Module
  {
    private const string TargetTypeParameterName = "Autofac.AutowiringPropertyInjector.InstanceType";
    private readonly bool _autowireProperties;
    private readonly bool _dispose;

    private readonly ILogger _logger;
    private readonly bool _skipRegistration;

    [Obsolete("Do not use this constructor. This is required by the Autofac assembly scanning")]
    public ContextualLoggingModule()
    {
      // Workaround to skip the logger registration when module is loaded by Autofac assembly scanning
      this._skipRegistration = true;
    }

    internal ContextualLoggingModule(ILogger logger = null, bool autowireProperties = false, bool dispose = false)
    {
      this._logger = logger;
      this._autowireProperties = autowireProperties;
      this._dispose = dispose;
      this._skipRegistration = false;
    }

    protected override void Load(ContainerBuilder builder)
    {
      if (this._skipRegistration)
        return;

      if (this._dispose)
      {
        builder.Register(c =>
          {
            var provider = new LoggerProvider(this._logger);
            return provider;
          })
          .AsSelf()
          .AutoActivate()
          .OnRelease(c => c.Release());

        builder.Register((c, p) =>
          {
            var logger = c.Resolve<LoggerProvider>().GetLogger();

            var targetType = p.OfType<NamedParameter>()
              .FirstOrDefault(np => np.Name == TargetTypeParameterName && np.Value is Type);

            if (targetType != null)
              return logger.ForContext((Type) targetType.Value);

            return logger;
          })
          .As<ILogger>()
          .ExternallyOwned();
      }
      else
      {
        builder.Register((c, p) =>
          {
            var targetType = p.OfType<NamedParameter>()
              .FirstOrDefault(np => np.Name == TargetTypeParameterName && np.Value is Type);

            if (targetType != null)
              return (this._logger ?? Log.Logger).ForContext((Type) targetType.Value);

            return this._logger ?? Log.Logger;
          })
          .As<ILogger>()
          .ExternallyOwned();
      }
    }

    protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry,
      IComponentRegistration registration)
    {
      if (this._skipRegistration)
        return;

      // Ignore components that provide loggers (and thus avoid a circular dependency below)
      if (registration.Services.OfType<TypedService>().Any(ts =>
        ts.ServiceType == typeof(ILogger) || ts.ServiceType == typeof(LoggerProvider)))
        return;

      PropertyInfo[ ] targetProperties = null;

      var ra = registration.Activator as ReflectionActivator;
      if (ra != null)
      {
        var ctors = ra.ConstructorFinder.FindConstructors(ra.LimitType);
        var usesLogger =
          ctors.SelectMany(ctor => ctor.GetParameters()).Any(pi => pi.ParameterType == typeof(ILogger));

        if (this._autowireProperties)
        {
          var logProperties = ra.LimitType
            .GetRuntimeProperties()
            .Where(c => c.CanWrite && c.PropertyType == typeof(ILogger) && c.SetMethod.IsPublic &&
                        !c.SetMethod.IsStatic)
            .ToArray();

          if (logProperties.Any())
          {
            targetProperties = logProperties;
            usesLogger = true;
          }
        }

        // Ignore components known to be without logger dependencies
        if (!usesLogger)
          return;
      }

      registration.Preparing += (sender, args) =>
      {
        var log = args.Context.Resolve<ILogger>().ForContext(registration.Activator.LimitType);
        args.Parameters = new[ ] {TypedParameter.From(log)}.Concat(args.Parameters);
      };

      if (targetProperties != null)
        registration.Activating += (sender, args) =>
        {
          var log = args.Context.Resolve<ILogger>().ForContext(registration.Activator.LimitType);
          foreach (var targetProperty in targetProperties) targetProperty.SetValue(args.Instance, log);
        };
    }
  }
}