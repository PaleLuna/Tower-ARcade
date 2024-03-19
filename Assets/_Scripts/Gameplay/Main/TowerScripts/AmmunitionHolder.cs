using PaleLuna.DataHolder;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionHolder : MonoBehaviour
{
    [SerializeField]
    private Shell _shellPrefab;

    private Queue<Shell> _combatShells;

    private void Start()
    {
        Shell[] shells= transform.GetComponentsInChildren<Shell>(includeInactive: true);
        foreach (Shell shell in shells) shell.SetParentAmmunition(this);

        _combatShells = new Queue<Shell>(shells);
    }

    public Shell GetShell()
    {
        if(_combatShells.Count > 0)
            return _combatShells.Dequeue();

        return Instantiate(_shellPrefab, transform.position, Quaternion.identity, transform);
    }

    public void ReturnToCombat(Shell shell)
    {
        _combatShells.Enqueue(shell);
    }
}
