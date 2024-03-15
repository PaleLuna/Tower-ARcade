using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Services;
using Services;
using System.Collections.Generic;
using UnityEngine;

public class PathHolder : MonoBehaviour, IService, IStartable
{
    [SerializeField]
    private List<Path> _paths = new();

    private bool _isStart = false;
    public bool IsStarted => _isStart;

    private void OnValidate()
    {
        foreach (Path item in _paths)
            item.Refresh();
    }

    public void OnStart()
    {
        ServiceManager.Instance.SceneLocator.Registarion(this);

        _isStart = true;
    }

    public Path GetPath(int pathIndex)
    {
        if(pathIndex < _paths.Count)
            return _paths[pathIndex];
        return null;
    }

    
}
