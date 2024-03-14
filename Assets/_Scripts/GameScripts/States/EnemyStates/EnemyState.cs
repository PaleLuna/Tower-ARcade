using PaleLuna.Patterns.State;

public class EnemyState : State
{
    protected Enemy m_context;

    public EnemyState(Enemy context)
    {
        this.m_context = context;
    }

}
