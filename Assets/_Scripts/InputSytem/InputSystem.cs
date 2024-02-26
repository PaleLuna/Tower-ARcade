using PaleLuna.Architecture.Controllers;
using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Services;
using Services;
using UnityEngine;

public class InputSystem : IUpdatable, IService
{
    private IInput _inputHandler = new MobileInput();

    public InputSystem() 
    {
        ServiceLocator globalService = ServiceManager.Instance.GlobalServices;

        globalService.Get<GameController>().Registatrion(this);
    }

    public void EveryFrameRun()
    {
        CheckTap();
        CheckDoubleTap();
    }

    private void CheckTap()
    {
        if (_inputHandler.TryTap(out Vector2 pos))
            InputEvents.tapEvent.Invoke(pos);
    }

    private void CheckDoubleTap()
    {
        if (_inputHandler.TryDoubleTap(out Vector2 pos))
            InputEvents.doubleTapEvent.Invoke(pos);
    }

    
}
