using PaleLuna.Patterns.State;

public class EnemyStateHolder : StateHolder<EnemyState>
{
    public EnemyStateHolder(Enemy context)
    {
        Registarion(new EnemyStateWalk(context));
        Registarion(new EnemyStateIdle(context));
        Registarion(new EnemyStateDeath(context));

        ChangeState<EnemyStateIdle>();
    }
}
