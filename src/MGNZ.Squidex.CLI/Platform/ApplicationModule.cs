namespace MGNZ.Squidex.CLI.Platform
{
  using Autofac;

  internal class ApplicationModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<AcceptsLogViaCtor>().As<IExample>();
      builder.RegisterType<AcceptsLogViaProperty>().As<IExample>();
      builder.RegisterType<AcceptsLogViaCtorITypeSafeOldLogger>().As<IExample>();
    }
  }
}