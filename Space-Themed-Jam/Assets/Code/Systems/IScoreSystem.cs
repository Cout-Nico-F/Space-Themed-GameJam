public interface IScoreSystem
{
    void Reset();
    string[] GetPlayerNames();
    int[] GetBestScores();
    int CurrentScore { get; }
}
