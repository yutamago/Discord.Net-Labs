using System;

namespace Discord.ApplicationCommands
{
    public struct ParseResult : IResult
    {
        public IApplicationCommand Command { get; }
        public InteractionError? Error { get; }
        public string ErrorReason { get; }
        public bool IsSuccess => !Error.HasValue;

        private ParseResult (IApplicationCommand command, InteractionError? error, string reason)
        {
            Command = command;
            Error = error;
            ErrorReason = reason;
        }

        public static ParseResult FromSuccess (IApplicationCommand command) =>
            new ParseResult(command, null, null);

        public static ParseResult FromError (Exception ex) =>
            new ParseResult(null, InteractionError.Exception, ex.Message);

        public static ParseResult FromError (IResult result) =>
            new ParseResult(null, result.Error, result.ErrorReason);

        public static ParseResult FromError (InteractionError error, string reason) =>
            new ParseResult(null, error, reason);

        public override string ToString ( ) => IsSuccess ? "Success" : $"{Error}: {ErrorReason}";
    }
}
