using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


public abstract class AWinWindow : BaseWindow
{
    [SerializeField] protected TextMeshProUGUI _winPlayerTxt;
    [SerializeField] protected TextMeshProUGUI _playerOneItemTxt;
    [SerializeField] protected TextMeshProUGUI _playerTwoItemTxt;
    
    [SerializeField] protected Button _resumeButton;

    [Inject] protected GameManager _gameManager;
    

    public virtual void Initialize(GameData gameData, GameEnum.RoundResult roundResult, int roundNum)
    {
        string winTxt = roundResult == GameEnum.RoundResult.Draw ? roundResult.ToString() : $"{gameData.RoundInfos[roundNum].WinnerPlayer.Name} win!";
        _winPlayerTxt.text = winTxt;
        
        _playerOneItemTxt.text = $"{gameData.RoundInfos[roundNum].FirstPlayer.Name}: {gameData.RoundInfos[roundNum].FirstPlayer.GameItem}";
        _playerTwoItemTxt.text = $"{gameData.RoundInfos[roundNum].SecondPlayer.Name}: {gameData.RoundInfos[roundNum].SecondPlayer.GameItem}";
        
        _resumeButton.onClick.AddListener(Hide);
        
    }
    
    
    public override void Hide()
    {
        _resumeButton.onClick.RemoveAllListeners();
        base.Hide();
    }
}
