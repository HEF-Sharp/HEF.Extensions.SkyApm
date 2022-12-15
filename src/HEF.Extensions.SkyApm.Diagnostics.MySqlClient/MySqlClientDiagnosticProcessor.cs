using MySqlConnector.Diagnostics;
using SkyApm.Config;
using SkyApm.Tracing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SkyApm.Diagnostics.MySqlClient
{
    public class MySqlClientTracingDiagnosticProcessor : ITracingDiagnosticProcessor
    {
        private readonly ITracingContext _tracingContext;
        private readonly IExitSegmentContextAccessor _contextAccessor;
        private readonly TracingConfig _tracingConfig;

        public MySqlClientTracingDiagnosticProcessor(ITracingContext tracingContext,
            IExitSegmentContextAccessor contextAccessor, IConfigAccessor configAccessor)
        {
            _tracingContext = tracingContext;
            _contextAccessor = contextAccessor;
            _tracingConfig = configAccessor.Get<TracingConfig>();
        }

        public string ListenerName { get; } = MySqlClientDiagnosticStrings.DiagnosticListenerName;

        private static string ResolveOperationName(MySqlDiagnosticsCommand sqlCommand)
        {
            var commandType = sqlCommand.CommandText?.Split(' ');
            return $"{MySqlClientDiagnosticStrings.MySqlClientPrefix}{commandType?.FirstOrDefault()}";
        }

        [DiagnosticName(MySqlClientDiagnosticStrings.MySqlBeforeExecuteCommand)]
        public void BeforeExecuteCommand([Property(Name = "Commands")] IReadOnlyList<MySqlDiagnosticsCommand> sqlCommands)
        {
            var firstSqlCommand = sqlCommands.First();

            var context = _tracingContext.CreateExitSegmentContext(ResolveOperationName(firstSqlCommand),
                firstSqlCommand.Connection.DataSource);

            context.Span.SpanLayer = Tracing.Segments.SpanLayer.DB;
            context.Span.Component = Common.Components.SQLCLIENT;
            context.Span.AddTag(Common.Tags.DB_TYPE, "MySql");
            context.Span.AddTag(Common.Tags.DB_INSTANCE, firstSqlCommand.Connection.Database);
            context.Span.AddTag(Common.Tags.DB_STATEMENT, string.Join(Environment.NewLine, sqlCommands.Select(c => c.CommandText)));
        }

        [DiagnosticName(MySqlClientDiagnosticStrings.MySqlAfterExecuteCommand)]
        public void AfterExecuteCommand()
        {
            var context = _contextAccessor.Context;
            if (context != null)
            {
                _tracingContext.Release(context);
            }
        }

        [DiagnosticName(MySqlClientDiagnosticStrings.MySqlErrorExecuteCommand)]
        public void ErrorExecuteCommand([Property(Name = "Exception")] Exception ex)
        {
            var context = _contextAccessor.Context;
            if (context != null)
            {
                context.Span.ErrorOccurred(ex, _tracingConfig);
                _tracingContext.Release(context);
            }
        }
    }
}
