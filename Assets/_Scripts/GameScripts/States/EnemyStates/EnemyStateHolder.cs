using PaleLuna.Patterns.State;

public class EnemyStateHolder : StateHolder<EnemyState>
{

    public EnemyStateHolder(Enemy context)
    {
        Registarion(new EnemyStateWalk(context, this));
        Registarion(new EnemyStateIdle(context, this));
        Registarion(new EnemyStateDeactive(context, this));

        ChangeState<EnemyStateIdle>();
    }
}
