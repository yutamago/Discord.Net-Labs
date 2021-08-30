namespace Discord.ApplicationCommands
{
    public interface IResult
    {
        InteractionError? Error { get; }
        string ErrorReason { get; }
        bool IsSuccess { get; }
    }
}
