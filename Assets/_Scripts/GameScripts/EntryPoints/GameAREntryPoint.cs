using Cysharp.Threading.Tasks;
using PaleLuna.Architecture.EntryPoint;


public class GameAREntryPoint : SceneEntryPoint
{
    protected override async UniTask Setup()
    {
        await base.Setup();
    }

    protected override void FillSceneLocator()
    {
        _sceneServiceLocator.Registarion(new InputSystem());
        _sceneServiceLocator.Registarion(new Wallet());
    }
}
