using PaleLuna.Architecture.Controllers;
using PaleLuna.Architecture.GameComponent;
using Services;

public class TowerCombatState : TowerState, IUpdatable, IPausable
{
    private GameController _gameController;

    public TowerCombatState(Tower context)
        : base(context)
    {
        _gameController = ServiceManager.Instance.GlobalServices.Get<GameController>();
    }

    public override void StateStart()
    {
        UpdateRegistration();
        PausableRegistration();
        m_context.combatZone.SubscribeOnLastExit(OnNoEnemy);
    }

    public void EveryFrameRun()
    {
        Enemy currentEnemy = m_context.combatZone.GetEnemy();

        m_context.RotateHead(currentEnemy.transform);
        m_context.Fire(currentEnemy);
    }

    private void OnNoEnemy()
    {
        m_context.stateHolder.ChangeState<TowerIdleState>();
    }

    public override void StateStop()
    {
        UpdateUnregistation();
        PausableUnregistration();
        m_context.combatZone.UnsubscribeOnLastExit(OnNoEnemy);
    }

    public void OnPause()
    {
        UpdateUnregistation();
    }

    public void OnResume()
    {
        UpdateRegistration();
    }


    private void PausableRegistration() =>
        _gameController.pausablesHolder.Registration(this);
    private void PausableUnregistration() =>
        _gameController.pausablesHolder.Unregistration(this);

    private void UpdateRegistration() =>
        _gameController.updatablesHolder.Registration(this);
    private void UpdateUnregistation() =>
        _gameController.updatablesHolder.UnRegistration(this);
}
