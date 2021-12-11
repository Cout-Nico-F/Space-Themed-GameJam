public class ScoreSystem : IScoreSystem
{
    public int CurrentScore => _currentScore;

    private readonly DataStore _dataStore;
    private int _currentScore;
    private const string Userdata = "UserData";

        
    public ScoreSystem(DataStore dataStore)
    {
        _dataStore = dataStore;
    }

        
    public void Reset()
    {
        _currentScore = 0;
        // ScoreView.ResetScore
    }

    public string[] GetPlayerNames()
    {
        var userData = _dataStore.GetData<UserData>(Userdata) ?? new UserData();
        return userData.PlayerNames;
    }

    public int[] GetBestScores()
    {
        var userData = _dataStore.GetData<UserData>(Userdata) ?? new UserData();
        return userData.BestScores;
    }

    private void SaveUserData(string[] playerNames, int[] bestScores)
    {
        var userData = new UserData {PlayerNames = playerNames, BestScores = bestScores};
        _dataStore.SetData(userData, Userdata);
    }
}
