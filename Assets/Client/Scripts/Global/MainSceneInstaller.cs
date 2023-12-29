using TMPro;
using UnityEngine;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    [SerializeField] private WindowsManager _windowsManager;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private CameraManager cameraManager;
    

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<WindowsManager>().FromInstance(_windowsManager).AsSingle();
        Container.BindInterfacesAndSelfTo<GameManager>().FromInstance(_gameManager).AsSingle();
        Container.BindInterfacesAndSelfTo<GamePresenter>().AsSingle();
        Container.BindInterfacesAndSelfTo<AudioManager>().FromInstance(_audioManager).AsSingle();
        Container.BindInterfacesAndSelfTo<CameraManager>().FromInstance(cameraManager).AsSingle();
        
    }
}