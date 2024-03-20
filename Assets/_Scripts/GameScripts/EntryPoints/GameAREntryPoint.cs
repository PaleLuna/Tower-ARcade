using Cysharp.Threading.Tasks;
using PaleLuna.Architecture.EntryPoint;


public class GameAREntryPoint : SceneEntryPoint
{
    protected override async UniTask Setup()
    {
        await base.Setup();
    }

    protected override void FillInitializers()
    {
        _initializers.Registration(new ValueCounterInit());
    }

    protected override void FillSceneLocator()
    {
        _sceneServiceLocator.Registarion(new InputSystem());
    }
}
