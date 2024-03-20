using PaleLuna.Architecture.Controllers;
using PaleLuna.Architecture.GameComponent;
using Services;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class StatusBar : MonoBehaviour, IUpdatable
{
    [SerializeField]
    private Slider _sliderBar;

    private GameController _gameController;
    private Transform _cameraTransform;

    #region [ MonoBehaviour methods ]
        private void OnValidate() 
        {
            _sliderBar ??= GetComponent<Slider>();
        }

        private void Awake()
        {
            _gameController = ServiceManager.Instance.GlobalServices.Get<GameController>();
            _cameraTransform = ServiceManager.Instance.SceneLocator.Get<CameraScript>().transform;
        }
        private void OnEnable() 
        {
            _gameController.updatablesHolder.Registration(this);
        }
    
        private void OnDisable() 
        {
            _gameController.updatablesHolder.UnRegistration(this);
        }
    #endregion

    public void SetMax(int max) => _sliderBar.maxValue = max;
    public void SetCurrent(int value) => _sliderBar.value = value;

    public void EveryFrameRun()
    {
        transform.rotation = Quaternion.LookRotation(_cameraTransform.position - transform.position);
    }
}
