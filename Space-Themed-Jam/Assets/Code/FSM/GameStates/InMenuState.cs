public class InMenuState : IState, IEventObserver
{
    private readonly GameManager _gameManager;
    private bool _startGamePressed;
        
    public InMenuState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Tick()
    {
        if (_startGamePressed)
            _gameManager.CurrentGameState = GameStates.Playing;
    }

    public void OnEnter()
    {
        _startGamePressed = false;
        ServiceLocator.Instance.GetService<EventQueue>().Subscribe(EventIds.StartGamePressed, this);
        new LoadSceneCommand("Menu").Execute();
    }

    public void OnExit()
    {
        _startGamePressed = false;
        ServiceLocator.Instance.GetService<EventQueue>().Unsubscribe(EventIds.StartGamePressed, this);
    }

    public void Process(EventData eventData)
    {
        if (eventData.EventId == EventIds.StartGamePressed)
        {
            _startGamePressed = true;
        }
    }
}
