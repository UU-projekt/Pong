public enum StateOption
{
    PLAYING,
    MENU,
    GAME_COMPLETED,
    SHOULD_EXIT
}

public struct GameInformation
{
    public TimeSpan Duration;
    public int Bounces;
    public string? Winner;
}


public enum Direction
{
    LEFT,
    RIGHT
}

public class GameState
{
    public StateOption State { get; set; }
    public string? LeftPlayerName { get; private set; }
    public string? RightPlayerName { get; private set; }
    public GameInformation LastGame;


    public GameState(string leftPlayer, string rightPlayer)
    {
        LeftPlayerName = leftPlayer;
        RightPlayerName = rightPlayer;
        State = StateOption.PLAYING;
    }

    public void SetWinner(Direction winner, int bounces, TimeSpan duration)
    {
        var winnerName = winner == Direction.LEFT ? LeftPlayerName : RightPlayerName;

        LastGame = new GameInformation() { Winner = winnerName, Bounces = bounces, Duration = duration };

        State = StateOption.GAME_COMPLETED;
    }
}