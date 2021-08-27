using System;

namespace Discord.ApplicationCommands
{
    public class ExecuteResult : IResult
    {
        public Exception Exception { get; }
        public ApplicationCommandError? Error { get; }

        public string ErrorReason { get; }

        public bool IsSuccess => !Error.HasValue;

        private ExecuteResult (Exception exception, ApplicationCommandError? commandError, string errorReason)
        {
            Exception = exception;
            Error = commandError;
            ErrorReason = errorReason;
        }

        public static ExecuteResult FromSuccess ( ) =>
            new ExecuteResult(null, null, null);

        public static ExecuteResult FromError (ApplicationCommandError commandError, string reason) =>
            new ExecuteResult(null, commandError, reason);

        public static ExecuteResult FromError (Exception exception) =>
            new ExecuteResult(exception, ApplicationCommandError.Exception, exception.Message);

        public static ExecuteResult FromError (IResult result) =>
            new ExecuteResult(null, result.Error, result.ErrorReason);

        public override string ToString ( ) => IsSuccess ? "Success" : $"{Error}: {ErrorReason}";
    }
}
