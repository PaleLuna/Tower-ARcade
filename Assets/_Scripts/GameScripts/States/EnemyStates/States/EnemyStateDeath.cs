public class EnemyStateDeath : EnemyState
{
    public EnemyStateDeath(Enemy context)
        : base(context) { }

    public override void StateStart()
    {
        m_context.gameObject.SetActive(false);
    }
}
