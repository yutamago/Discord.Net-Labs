using Discord.ApplicationCommands.Builders;

namespace Discord.ApplicationCommands
{
    internal interface IApplicationCommandModuleBase
    {
        void SetContext (ISlashCommandContext context);

        void BeforeExecute (ExecutableInfo command);

        void AfterExecute (ExecutableInfo command);

        void OnModuleBuilding (ApplicationCommandService commandService, ModuleInfo module);
    }
}
