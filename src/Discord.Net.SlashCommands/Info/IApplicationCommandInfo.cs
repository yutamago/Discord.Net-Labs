namespace Discord.ApplicationCommands
{
    public interface IApplicationCommandInfo
    {
        string Name { get; }
        ApplicationCommandType CommandType { get; }
    }
}
