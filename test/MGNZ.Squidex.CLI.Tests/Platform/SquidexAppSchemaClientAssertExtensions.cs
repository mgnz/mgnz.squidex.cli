namespace MGNZ.Squidex.CLI.Tests.Platform
{
  using System;
  using System.Threading.Tasks;

  using FluentAssertions;

  using MGNZ.Squidex.Client;
  using MGNZ.Squidex.CLI.Common.Commands;

  internal static class SquidexContentClientAssertExtensions
  {
    public static async Task AssertContentMustExists(this ISquidexContentClient that, string application, string schema, string id, TimeSpan? delay = null)
    {
      // because of eventual consistency
      if (delay.HasValue) await Task.Delay(delay.Value);

      var exists = false;

      try
      {
        await that.Get<dynamic>(application, schema, id);
        exists = true;
      }
      catch (Exception e)
      {
        exists = false;
      }

      exists.Should().BeTrue();
    }

    public static async Task AssertContentMustNotExists(this ISquidexContentClient that, string application, string schema, string id, TimeSpan? delay = null)
    {
      // because of eventual consistency
      if (delay.HasValue) await Task.Delay(delay.Value);

      var exists = false;

      try
      {
        await that.Get<dynamic>(application, schema, id);
        exists = true;
      }
      catch (Exception e)
      {
        exists = false;
      }

      exists.Should().BeFalse();
    }
  }

  internal static class SquidexAppSchemaClientAssertExtensions
  {
    public static async Task AssertNoSchemasExist(this ISquidexAppSchemaClient that, string application, TimeSpan? delay = null)
    {
      // because of eventual consistency
      if (delay.HasValue) await Task.Delay(delay.Value);

      var exists = await that.SchemaExists(application);
      exists.Should().BeFalse();
    }

    public static async Task AssertSchemaMustExist(this ISquidexAppSchemaClient that, string application, string name, TimeSpan? delay = null)
    {
      // because of eventual consistency
      if (delay.HasValue) await Task.Delay(delay.Value);

      var exists = await that.SchemaExists(application, name);
      exists.Should().BeTrue();
    }

    public static async Task AssertSchemaMustNotExist(this ISquidexAppSchemaClient that, string application, string name, TimeSpan? delay = null)
    {
      // because of eventual consistency
      if (delay.HasValue) await Task.Delay(delay.Value);

      var exists = await that.SchemaExists(application, name);
      exists.Should().BeFalse();
    }
  }
}