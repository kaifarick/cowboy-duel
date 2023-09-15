using TMPro;
using UnityEngine;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    [SerializeField] private WindowsManager _windowsManager;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private SoundManager _soundManager;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<WindowsManager>().FromInstance(_windowsManager).AsSingle();
        Container.BindInterfacesAndSelfTo<GameManager>().FromInstance(_gameManager).AsSingle();
        Container.BindInterfacesAndSelfTo<SoundManager>().FromInstance(_soundManager).AsSingle();
    }
}