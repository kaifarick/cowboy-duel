
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsWindow : BaseWindow
{

    [SerializeField] private Toggle _toggle;
    [SerializeField] private Button _closeButton;

    [Inject] private AudioManager  _audioManager;

    private void Start()
    {
        _toggle.onValueChanged.AddListener(arg0 => _audioManager.SwitchMusicState(arg0));
        _closeButton.onClick.AddListener(Hide);

        _toggle.isOn = _audioManager.MusicEnable;
    }
}
