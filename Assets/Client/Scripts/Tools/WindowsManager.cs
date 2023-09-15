
using UnityEngine;
using UnityEngine.Serialization;

public class WindowsManager : MonoBehaviour
{
    [SerializeField] private SettingsWindow _settingsWindow;
    [SerializeField] private WinWindowPlayerVsComputer winWindowPlayerVsComputer;
    [SerializeField] private WinWindowPlayerVsPlayer _winWindowPlayerVsPlayer;
    [SerializeField] private WinWindowSurvival _winWindowSurvival;
    [SerializeField] private WinWindowChampionship _winWindowChampionship;

    public SettingsWindow GetSettingsWindow() => _settingsWindow;
    
    public AWinWindow GetWinWindow(AGameplay aGameplay)
    {
        if (aGameplay is PlayerVsComputerGameplay) return winWindowPlayerVsComputer;
        if (aGameplay is PlayerVsPlayerGameplay) return _winWindowPlayerVsPlayer;
        if (aGameplay is SurvivalGameplay) return _winWindowSurvival;
        if (aGameplay is ChampionshipGameplay) return _winWindowChampionship;

        return null;
    }
}
