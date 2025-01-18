public class GameStateContext
{
    public bool IsLoad { get; }
    public bool IsStoryMode { get; }
    public int DailyPlaytime { get; }

    public GameStateContext(bool isLoad, bool isStoryMode, int dailyPlaytime)
    {
        IsLoad = isLoad;
        IsStoryMode = isStoryMode;
        DailyPlaytime = dailyPlaytime;
    }
}