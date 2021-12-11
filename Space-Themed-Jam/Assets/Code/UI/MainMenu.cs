using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button highScoresButton;
    [SerializeField] private Button quitButton;

    private EventQueue _eventQueue;
        
    private void Awake()
    {
        startGameButton.onClick.AddListener(StartGame);
        highScoresButton.onClick.AddListener(GoToHighScores);
        quitButton.onClick.AddListener(Quit);
    }

    private void Start()
    {
        _eventQueue = ServiceLocator.Instance.GetService<EventQueue>();
    }

    private void GoToHighScores()
    {
    }

    private void StartGame()
    {
        _eventQueue.EnqueueEvent(new StartGameEvent());
    }

    private void Quit()
    {
        Application.Quit();
    }

}