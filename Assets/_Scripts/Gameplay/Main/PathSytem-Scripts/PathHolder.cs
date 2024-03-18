using PaleLuna.Architecture.Services;
using Services;
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

    private void Start() {
        print("Registration");
        ServiceManager.Instance.SceneLocator.Registarion(this);
    }
    public Path GetPath(int pathIndex)
    {
        if(pathIndex < _paths.Count)
            return _paths[pathIndex];
        return null;
    }
}
