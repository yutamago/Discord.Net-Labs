using System;

namespace Discord.ApplicationCommands
{
    public class EmptyServiceProvider : IServiceProvider
    {
        public static EmptyServiceProvider Instance => new EmptyServiceProvider();

        public object GetService (Type serviceType) => null;
    }
}
