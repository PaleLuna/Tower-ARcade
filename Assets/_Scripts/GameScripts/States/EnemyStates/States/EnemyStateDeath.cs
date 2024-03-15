using UnityEngine;

public class EnemyStateDeath : EnemyState
{
    public EnemyStateDeath(Enemy context, EnemyStateHolder holder)
        : base(context, holder) { }

    public override void StateStart()
    {
        Dead();
    }

    private void Dead()
    {
        GameEvents.enemyDeathEvent.Invoke(m_context);
        m_context.gameObject.SetActive(false);
    }
}
