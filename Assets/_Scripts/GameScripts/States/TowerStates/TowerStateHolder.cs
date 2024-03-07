using PaleLuna.Patterns.State;


public class TowerStateHolder : StateHolder<TowerState>
{
    public TowerStateHolder(Tower context)
    {
        Registarion(new TowerIdleState(context));
        Registarion(new TowerCombatState(context));

        ChangeState<TowerIdleState>();
    }
}
