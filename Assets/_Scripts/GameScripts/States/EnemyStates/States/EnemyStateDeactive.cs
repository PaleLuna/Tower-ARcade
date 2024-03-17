using UnityEngine;

public class EnemyStateDeactive : EnemyState
{
    public EnemyStateDeactive(Enemy context, EnemyStateHolder holder)
        : base(context, holder) { }

    public override void StateStart()
    {
        Deactive();
    }

    private void Deactive()
    {
        m_context.gameObject.SetActive(false);
    }

    public override void StateStop()
    {
        m_context.gameObject.SetActive(true);
    }
}
