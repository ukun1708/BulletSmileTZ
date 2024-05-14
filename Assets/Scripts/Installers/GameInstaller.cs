using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private VfxManager vfxManager;
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromInstance(gameManager);
        Container.Bind<VfxManager>().FromInstance(vfxManager);
    }
}