using UnityEngine;
using PaleLuna.Patterns.State;


public class TowerStateHolder : StateHolder<TowerState>
{
    public TowerStateHolder(Tower context)
    {
        Registarion(new TowerIdleState(context));
        Registarion(new TowerCombatState(context));

        ChangeState<TowerIdleState>();
    }

    public void Clear()
    {
        currentState.StateStop();
        DeleteAllStates();
    }

    ~TowerStateHolder()
    {
        Debug.Log("Destroy");
    }
}
