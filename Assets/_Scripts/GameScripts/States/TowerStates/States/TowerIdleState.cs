using UnityEngine;

public class TowerIdleState : TowerState
{
    public TowerIdleState(Tower context) : base(context){}

    public override void StateStart()
    {
        Debug.Log($"New State! {this}");
        m_context.combatZone.SubscribeOnFirstEnter(OnEnemyEnter);
    }

    private void OnEnemyEnter()
    {
        m_context.stateHolder.ChangeState<TowerCombatState>();
    }

    public override void StateStop()
    {
        m_context.combatZone.UnsubscribeOnFirstEnter(OnEnemyEnter);
    }
}
