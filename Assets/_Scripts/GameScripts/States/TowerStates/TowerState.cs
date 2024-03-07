using PaleLuna.Patterns.State;
using UnityEngine;

public abstract class TowerState : State
{
    protected Tower _context;

    public TowerState(Tower context)
    {
        this._context = context;
    }
}
