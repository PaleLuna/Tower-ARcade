using PaleLuna.Architecture.Controllers;
using PaleLuna.Architecture.GameComponent;
using Services;
using UnityEngine;

public class TowerCombatState : TowerState, IUpdatable
{
    private GameController _gameController;
    public TowerCombatState(Tower context) : base(context)
    {
        _gameController = ServiceManager.Instance.GlobalServices.Get<GameController>();
    }

    public override void StateStart()
    {
        _gameController.updatablesHolder.Registration(this);
    }
    public void EveryFrameRun()
    {
        Debug.Log("In Combat Mode!");
    }

    public override void StateStop()
    {
        _gameController.updatablesHolder.UnRegistration(this);
    }
}
