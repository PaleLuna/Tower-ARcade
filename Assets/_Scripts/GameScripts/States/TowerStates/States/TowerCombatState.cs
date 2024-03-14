using PaleLuna.Architecture.Controllers;
using PaleLuna.Architecture.GameComponent;
using Services;

public class TowerCombatState : TowerState, IUpdatable
{
    private GameController _gameController;

    public TowerCombatState(Tower context)
        : base(context)
    {
        _gameController = ServiceManager.Instance.GlobalServices.Get<GameController>();
    }

    public override void StateStart()
    {
        _gameController.updatablesHolder.Registration(this);
        m_context.combatZone.SubscribeOnLastExit(OnNoEnemy);
    }

    public void EveryFrameRun()
    {
        Enemy currentEnemy = m_context.combatZone.GetEnemy();

        m_context.RotateHead(currentEnemy.transform);
        m_context.Fire(currentEnemy.transform);
    }

    private void OnNoEnemy()
    {
        m_context.stateHolder.ChangeState<TowerIdleState>();
    }

    public override void StateStop()
    {
        _gameController.updatablesHolder.UnRegistration(this);
        m_context.combatZone.UnsubscribeOnLastExit(OnNoEnemy);
    }
}
