using PaleLuna.Patterns.State;
using UnityEngine;

public abstract class TowerState : State
{
    protected Tower m_context;

    public TowerState(Tower context)
    {
        this.m_context = context;
    }
}
