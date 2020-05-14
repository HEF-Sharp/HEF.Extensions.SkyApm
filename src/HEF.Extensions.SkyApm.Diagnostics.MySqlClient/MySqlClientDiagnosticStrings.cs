namespace SkyApm.Diagnostics.MySqlClient
{
    internal static class MySqlClientDiagnosticStrings
    {
        public const string DiagnosticListenerName = "MySqlClientDiagnosticListener";

        public const string MySqlClientPrefix = "MySqlClient ";

        public const string MySqlBeforeExecuteCommand = "MySql.Data.MySqlClient.WriteCommandBefore";
        public const string MySqlAfterExecuteCommand = "MySql.Data.MySqlClient.WriteCommandAfter";
        public const string MySqlErrorExecuteCommand = "MySql.Data.MySqlClient.WriteCommandError";
    }
}
