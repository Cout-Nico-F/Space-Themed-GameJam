using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameStates CurrentGameState { get; set; }

    private StateMachine _stateMachine;
    private SceneController _sceneController;
    private IScoreSystem _scoreSystem;
    private UserData _userData;
    private InMenuState _inMenu;
    private PausedState _paused;
    private PlayingState _playing;
    private GameOverState _gameOver;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _stateMachine = new StateMachine();
            
        // instanciamos los diferentes estados
        _inMenu = new InMenuState(this);
        _paused = new PausedState();
        _playing = new PlayingState(this);
        _gameOver = new GameOverState();

        // definimos las transiciones
        AddTransition(_inMenu, _playing, OnStatePlaying());
        AddTransition(_playing, _paused, OnStatePaused());
        AddTransition(_playing, _gameOver, OnStateGameOver());
        AddTransition(_paused, _playing, OnStatePlaying());
        AddTransition(_paused, _inMenu, OnStateInMenu());
        AddTransition(_gameOver, _playing, OnStatePlaying());
        AddTransition(_gameOver, _inMenu, OnStateInMenu());
            
        void AddTransition(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            
        // definimos las condiciones
        Func<bool> OnStatePlaying() => () => CurrentGameState == GameStates.Playing;
        Func<bool> OnStatePaused() => () => CurrentGameState == GameStates.Paused;
        Func<bool> OnStateGameOver() => () => CurrentGameState == GameStates.GameOver;
        Func<bool> OnStateInMenu() => () => CurrentGameState == GameStates.InMenu;
    }

    private void Start()
    {
        _sceneController = ServiceLocator.Instance.GetService<SceneController>();
        _scoreSystem = ServiceLocator.Instance.GetService<IScoreSystem>();
            
        CurrentGameState = GameStates.InMenu;
        _stateMachine.SetState(_inMenu);
    }

    private void Update()
    {
        _stateMachine.Tick();
    }

    public void GoToMenu()
    {
        _sceneController.LoadScene("Menu");
    }

    public void GameOver()
    {
        
    }

        
    public void SaveData(string name, int score)
    {
        if (name == String.Empty)
        {
            name = "User name";
        }
            
        var playerNames = new string[12];
        var playerScores = new int[12];
        int length;
        int pos;

        //data = DataStore.Instance.GetData<UserData>("data");

        if (_userData == null)
        {
            playerNames[0] = name;
            playerScores[0] = score;
            length = 1;
            pos = 0;
            _userData = new UserData();
            //DataStore.Instance.SetData(data, "data");
        }
        else
        {
            length = this._userData.BestScores.Length + 1;
            if (length > 12)
            {
                length = 12;
            }

            playerNames = this._userData.PlayerNames;
            playerScores = this._userData.BestScores;            
            int scorePrev;
            string namePrev;

            var i = 0;
            while (playerScores[i] > score)
            {                
                i++;
            }

            pos = i;
            scorePrev = playerScores[i];
            namePrev = playerNames[i];
            playerScores[i] = score;
            playerNames[i] = name;            
            i++;

            for (var j = i; j < length; j++)
            {
                var scoreTemp = playerScores[j];
                name = playerNames[j];
                playerScores[j] = scorePrev;
                playerNames[j] = namePrev;
                scorePrev = scoreTemp;
                namePrev = name;
            }            

            _userData = new UserData();
            //DataStore.Instance.SetData(data, "data");
        }

        //_sceneController.LoadScene("HighScores");
    }
}
