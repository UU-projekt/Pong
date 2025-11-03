public enum StateOption
{
    PLAYING,
    MENU,
    GAME_COMPLETED,
    SHOULD_EXIT,
    LEADERBOARD,
    __DEV_GUITEST
}


public struct GameInformation
{
    public TimeSpan Duration;
    public int Bounces;
    public string? Winner;
    public AIDifficulty Difficulty;
}


public enum Direction
{
    LEFT,
    RIGHT
}

public class GameState
{
    public StateOption State { get; set; }
    public List<GameInformation> leaderboard = new();
    public string? LeftPlayerName { get; private set; }
    public string? RightPlayerName { get; private set; }
    public AIDifficulty difficulty;
    public GameInformation LastGame;


    public GameState(string leftPlayer, string rightPlayer)
    {
        LeftPlayerName = leftPlayer;
        RightPlayerName = rightPlayer;
        State = StateOption.PLAYING;
    }

    public void StartGame(AIDifficulty difficultyChoice)
    {
        difficulty = difficultyChoice;
        State = StateOption.PLAYING;
    }

    public void SetWinner(Direction winner, int bounces, TimeSpan duration)
    {
        var winnerName = winner == Direction.LEFT ? LeftPlayerName : RightPlayerName;

        LastGame = new GameInformation() { Winner = winnerName, Bounces = bounces, Duration = duration, Difficulty = difficulty };
        leaderboard.Add(LastGame);

        State = StateOption.GAME_COMPLETED;
    }
}