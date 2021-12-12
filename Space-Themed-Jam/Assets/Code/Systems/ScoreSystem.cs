﻿using System;

public class ScoreSystem : IEventObserver, IScoreSystem
{
    public int CurrentScore => _currentScore;

    private readonly DataStore _dataStore;
    private int _currentScore;
    private const string Userdata = "UserData";

        
    public ScoreSystem(DataStore dataStore)
    {
        _dataStore = dataStore;
        var eventQueue = ServiceLocator.Instance.GetService<EventQueue>();
        eventQueue.Subscribe(EventIds.Victory, this);
        //eventQueue.Subscribe(EventIds.ShipDestroyed, this);
    }

        
    public void Reset()
    {
        _currentScore = 0;
        // ScoreView.ResetScore
    }


    public void Process(EventData eventData)
    {
        if (eventData.EventId == EventIds.Victory)
        {
            //UpdateBestScores(_currentScore);
            return;
        }

        /*
        if (eventData.EventId == EventIds.ShipDestroyed)
        {
            AddScore();
        }
        */
    }

    private void AddScore()
    {
        throw new NotImplementedException();
    }

    private void UpdateBestScores(string playerName, int newScore)
    {
        var bestScores = GetUserData().BestScores;
        var playerNames = GetUserData().PlayerNames;

        var scoreIndex = 0;
        for (; scoreIndex < bestScores.Length; scoreIndex++)
        {
            if (bestScores[scoreIndex] < newScore) break;
        }

        var isTheNewScoreBetter = scoreIndex < bestScores.Length;
        if (!isTheNewScoreBetter) return;

        var oldScore = bestScores[scoreIndex];
        var oldName = playerNames[scoreIndex];

        bestScores[scoreIndex] = newScore;
        playerNames[scoreIndex] = playerName;
        scoreIndex += 1;
        for (; scoreIndex < bestScores.Length; ++scoreIndex)
        {
            newScore = bestScores[scoreIndex];
            bestScores[scoreIndex] = oldScore;
            oldScore = newScore;
            playerName = playerNames[scoreIndex];
            playerNames[scoreIndex] = oldName;
            oldName = playerName;
        }

        SaveUserData(playerNames, bestScores);
    }

    public UserData GetUserData()
    {
        var userData = _dataStore.GetData<UserData>(Userdata) ?? new UserData();
        return userData;
    }

    public void SaveUserData(string[] playerNames, int[] bestScores)
    {
        var userData = new UserData { PlayerNames = playerNames, BestScores = bestScores };
        _dataStore.SetData(userData, Userdata);
    }
}
