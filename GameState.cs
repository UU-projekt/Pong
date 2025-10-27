public enum StateOption
{
    PLAYING,
    MENU,
    GAME_COMPLETED,
    SHOULD_EXIT
}
public class GameState
{
    public StateOption state { get; set; }
    public string? LeftPlayerName { get; set; }
    public string? RightPlayerName { get; set; }
    public string? Winner { get; set; }
}