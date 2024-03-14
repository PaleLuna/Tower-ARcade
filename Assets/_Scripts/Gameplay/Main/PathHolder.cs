using PaleLuna.Architecture.Services;
using System.Collections.Generic;
using UnityEngine;

public class PathHolder : MonoBehaviour, IService
{
    [SerializeField]
    private List<Path> _paths = new();

    private void OnValidate()
    {
        foreach (Path item in _paths)
            item.Refresh();
    }

    public Path GetPath(int pathIndex)
    {
        if(pathIndex < _paths.Count)
            return _paths[pathIndex];
        return null;
    }
}
