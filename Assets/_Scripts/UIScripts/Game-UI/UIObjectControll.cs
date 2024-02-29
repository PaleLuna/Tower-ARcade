using UnityEngine;
using UnityEngine.UI;

public class UIObjectControll : MonoBehaviour
{
    [SerializeField]
    private GameObject _deleteBtn;
    [SerializeField]
    private GameObject _scaleSlider;

    private void Start()
    {
        UIEvents.objectSelectEvent.AddListener(OnObjectSelect);
        UIEvents.objectDeselectEvent.AddListener(OnObjectDeselect);
    }

    private void OnObjectSelect()
    {
        _deleteBtn.SetActive(true);
        _scaleSlider.SetActive(true);
    }

    private void OnObjectDeselect()
    {
        _deleteBtn.SetActive(false);
        _scaleSlider.SetActive(false);
    }

}
