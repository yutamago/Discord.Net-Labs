namespace Discord.ApplicationCommands
{
    public interface IResult
    {
        ApplicationCommandError? Error { get; }
        string ErrorReason { get; }
        bool IsSuccess { get; }
    }
}
