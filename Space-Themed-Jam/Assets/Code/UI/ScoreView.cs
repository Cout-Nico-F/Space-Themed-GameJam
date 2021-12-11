using System;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    public static ScoreView Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _gameOverView;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TMP_InputField _namePlayer;
        
    private int _currentScore;

    public int CurrentScore
    {
        get => _currentScore;
        private set
        {
            _currentScore = value;
            _text.SetText(_currentScore.ToString());
        }
    }
        
    private void Awake()
    {
        Instance = this;
        //_namePlayer.onEndEdit.AddListener(SaveData);
    }
        
    public void AddScore(int scoreToAdd)
    {
        CurrentScore += scoreToAdd;
    }

    public void ActivateGameOverView()
    {
        _gameOverView.SetActive(true);
        _scoreText.text = CurrentScore.ToString();
        _namePlayer.Select();
    }

    private void SaveData(string name)
    {
        _namePlayer.text = String.Empty;
        _gameOverView.SetActive(false);
        GameManager.Instance.SaveData(name, CurrentScore);
    }

}
