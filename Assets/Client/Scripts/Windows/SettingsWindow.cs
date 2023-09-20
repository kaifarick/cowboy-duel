
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsWindow : BaseWindow
{

    [SerializeField] private Toggle _toggle;
    [SerializeField] private Button _closeButton;

    [Inject] private SoundManager _soundManager;

    private void Start()
    {
        _toggle.onValueChanged.AddListener(arg0 => _soundManager.SwitchMusicState(arg0));
        _closeButton.onClick.AddListener(Hide);

        _toggle.isOn = _soundManager.MenuSoundState;
    }
}
