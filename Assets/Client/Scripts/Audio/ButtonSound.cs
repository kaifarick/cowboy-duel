using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private Button _button;

    [Inject] private AudioManager _audioManager;

    private void Awake()
    {
        _button.onClick.AddListener((() => _audioManager.PlaySound(_audioClip)));
    }
}
