using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.ApplicationCommands
{
    public abstract class RuntimeResult : IResult
    {
        public InteractionError? Error { get; }

        public string ErrorReason { get; }

        public bool IsSuccess => !Error.HasValue;

        protected RuntimeResult (InteractionError? error, string reason )
        {
            Error = error;
            ErrorReason = reason;
        }

        public override string ToString ( ) => ErrorReason ?? ( IsSuccess ? "Successful" : "Unsuccessful" );
    }
}
