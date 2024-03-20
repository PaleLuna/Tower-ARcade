using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class ARPlaneController : MonoBehaviour
{
    [SerializeField]
    private ARPlaneManager _aRPlaneManager;

    private void OnValidate() 
    {
        _aRPlaneManager ??= GetComponent<ARPlaneManager>();    
    }

    private void Start() 
    {
        GameEvents.gameRestart.AddListener(OnGameRestart);
        GameEvents.levelConfirmEvent.AddListener(OnLevelConfirm);
    }

    private void OnGameRestart()
    {
        _aRPlaneManager.enabled = true;
    }
    private void OnLevelConfirm()
    {
        _aRPlaneManager.enabled = false;
    }
}
