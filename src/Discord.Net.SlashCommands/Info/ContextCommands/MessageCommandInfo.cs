using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Discord.ApplicationCommands
{
    public class MessageCommandInfo : ContextCommandInfo
    {
        internal MessageCommandInfo (Builders.ContextCommandBuilder builder, ModuleInfo module, InteractionService commandService)
            : base(builder, module, commandService) { }

        public override async Task<IResult> ExecuteAsync (IInteractionContext context, IServiceProvider services)
        {
            if (!( context.Interaction is SocketMessageCommand messageCommand ))
                return ExecuteResult.FromError(InteractionError.ParseFailed, $"Provided {nameof(IInteractionContext)} does not belong to a Message Command");

            services = services ?? EmptyServiceProvider.Instance;

            try
            {
                var message = messageCommand.Data.Message;

                object[] args = new object[1] { message };

                if (CommandService._runAsync)
                {
                    _ = Task.Run(async ( ) =>
                    {
                        await ExecuteInternalAsync(context, args, services).ConfigureAwait(false);
                    });
                }
                else
                    return await ExecuteInternalAsync(context, args, services).ConfigureAwait(false);

                return ExecuteResult.FromSuccess();
            }
            catch (Exception ex)
            {
                return ExecuteResult.FromError(ex);
            }
        }

        protected override string GetLogString (IInteractionContext context)
        {
            if (context.Guild != null)
                return $"Message Command: \"{Name}\" for {context.User} in {context.Guild}/{context.Channel}";
            else
                return $"Message Command: \"{Name}\" for {context.User} in {context.Channel}";
        }
    }
}
