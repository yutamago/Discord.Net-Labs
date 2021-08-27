using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.ApplicationCommands
{
    public struct TypeReaderResult : IResult
    {
        public object Value { get; }

        public ApplicationCommandError? Error { get; }

        public string ErrorReason { get; }

        public bool IsSuccess => !Error.HasValue;

        public TypeReaderResult ( object value, ApplicationCommandError? error, string reason )
        {
            Value = value;
            Error = error;
            ErrorReason = reason;
        }

        public static TypeReaderResult FromSuccess (object value) =>
            new TypeReaderResult(value, null, null);

        public static TypeReaderResult FromError (Exception ex) =>
            new TypeReaderResult(null, ApplicationCommandError.Exception, ex.Message);

        public static TypeReaderResult FromError (ApplicationCommandError error, string reason) =>
            new TypeReaderResult(null, error, reason);

        public static TypeReaderResult FromError (IResult result) =>
            new TypeReaderResult(null, result.Error, result.ErrorReason);

        public override string ToString ( ) => IsSuccess ? "Success" : $"{Error}: {ErrorReason}";
    }
}
