using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Services;
using Services;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraScript : MonoBehaviour, IService, IStartable
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Transform _camParentTransform;

    private Vector3 _originScale;

    public Camera cam => _camera;

    private bool _isStart = false;
    public bool IsStarted => _isStart;

    public void OnStart()
    {
        if(_isStart) return;

        ServiceManager.Instance.SceneLocator.Registarion(this);
        _originScale = _camParentTransform.localScale;

        _isStart = true;

    }

    public void OnChangeScale(float value){
        _camParentTransform.localScale = _originScale * value;
    }

}
