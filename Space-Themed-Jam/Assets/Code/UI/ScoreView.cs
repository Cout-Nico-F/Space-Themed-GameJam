using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int _currentScore;

    
    public void ResetScore()
    {
        UpdateScore(0);
    }

    private void UpdateScore(int newScore)
    {
        scoreText.SetText(_currentScore.ToString());
    }

    public void AddScore(int points)
    {
        _currentScore += points;
        UpdateScore(_currentScore);
    }
}
