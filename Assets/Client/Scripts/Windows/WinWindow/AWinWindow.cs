using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


public abstract class AWinWindow : BaseWindow
{
    [SerializeField] protected TextMeshProUGUI _winPlayerTxt;
    [SerializeField] protected TextMeshProUGUI _playerOneDamageTxt;
    [SerializeField] protected TextMeshProUGUI _playerTwoDamageTxt;
    
    [SerializeField] protected Button _resumeButton;

    [Inject] protected GameManager _gameManager;
    

    public virtual void Initialize(GameData gameData, GameEnum.RoundResult roundResult, int roundNum)
    {
        string winTxt = roundResult == GameEnum.RoundResult.Draw ? roundResult.ToString() : $"{gameData.RoundInfos[roundNum].WinnerPlayer.Name} win!";
        _winPlayerTxt.text = winTxt;
        
        _playerOneDamageTxt.text = $"{gameData.RoundInfos[roundNum].FirstPlayer.Name} damage: {gameData.RoundInfos[roundNum].FirstPlayer.DamageDone}";
        _playerTwoDamageTxt.text = $"{gameData.RoundInfos[roundNum].SecondPlayer.Name} damage: {gameData.RoundInfos[roundNum].SecondPlayer.DamageDone}";
        
        _resumeButton.onClick.AddListener(Hide);
        
    }
    
    
    public override void Hide()
    {
        _resumeButton.onClick.RemoveAllListeners();
        base.Hide();
    }
}
