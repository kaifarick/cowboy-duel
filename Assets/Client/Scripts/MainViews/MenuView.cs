using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuView : MonoBehaviour
{
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Canvas _canvas;
    
    [Header("Selection Mode")]
    [SerializeField] private Button _playerVsComputerBtn;
    [SerializeField] private Button _playerVsPlayerBtn;
    [SerializeField] private Button _survivalModeBtn;
    [SerializeField] private Button _championshipBtn;

    [Inject] private GameManager _gameManager;
    [Inject] private WindowsManager _windowsManager;

    private void Awake()
    {
        _playerVsComputerBtn.onClick.AddListener(() => _gameManager.StartGame(new PlayerVsComputerGameplay(_gameManager)));
        _playerVsPlayerBtn.onClick.AddListener(() => _gameManager.StartGame(new PlayerVsPlayerGameplay(_gameManager)));
        _survivalModeBtn.onClick.AddListener(() => _gameManager.StartGame(new SurvivalGameplay(_gameManager)));
        _championshipBtn.onClick.AddListener(() => _gameManager.StartGame(new ChampionshipGameplay(_gameManager)));

        _settingsButton.onClick.AddListener((() => _windowsManager.OpenWindow<SettingsWindow>()));
    }

    private void Start()
    {
        _gameManager.OnGameStartAction += OnStartGame;
        _gameManager.OnGameEndAction += OnEndGame;
    }

    private void OnStartGame(GamePresenter gamePresenter)
    {
        _canvas.enabled = false;
    }
    
    private void OnEndGame()
    {
        _canvas.enabled = true;
    }
}
