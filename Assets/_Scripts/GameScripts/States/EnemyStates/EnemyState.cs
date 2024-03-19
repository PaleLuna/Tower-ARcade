using PaleLuna.Patterns.State;

public class EnemyState : State
{
    protected readonly Enemy m_context;
    protected readonly EnemyStateHolder m_hodler;

    public EnemyState(Enemy context, EnemyStateHolder hodler)
    {
        this.m_context = context;
        this.m_hodler = hodler;

    }

}
