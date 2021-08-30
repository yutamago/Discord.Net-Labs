using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.ApplicationCommands
{
    public struct TypeReaderResult : IResult
    {
        public object Value { get; }

        public InteractionError? Error { get; }

        public string ErrorReason { get; }

        public bool IsSuccess => !Error.HasValue;

        public TypeReaderResult ( object value, InteractionError? error, string reason )
        {
            Value = value;
            Error = error;
            ErrorReason = reason;
        }

        public static TypeReaderResult FromSuccess (object value) =>
            new TypeReaderResult(value, null, null);

        public static TypeReaderResult FromError (Exception ex) =>
            new TypeReaderResult(null, InteractionError.Exception, ex.Message);

        public static TypeReaderResult FromError (InteractionError error, string reason) =>
            new TypeReaderResult(null, error, reason);

        public static TypeReaderResult FromError (IResult result) =>
            new TypeReaderResult(null, result.Error, result.ErrorReason);

        public override string ToString ( ) => IsSuccess ? "Success" : $"{Error}: {ErrorReason}";
    }
}
