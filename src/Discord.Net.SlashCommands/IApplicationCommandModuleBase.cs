using Discord.ApplicationCommands.Builders;

namespace Discord.ApplicationCommands
{
    internal interface IApplicationCommandModuleBase
    {
        void SetContext (IInteractionContext context);

        void BeforeExecute (ExecutableInfo command);

        void AfterExecute (ExecutableInfo command);

        void OnModuleBuilding (InteractionService commandService, ModuleInfo module);
    }
}
