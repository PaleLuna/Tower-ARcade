using PaleLuna.DataHolder.Dictinory;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeReference]
    private List<Pony> _ponyPrefabs;

    

    [SerializeField]
    private PlacableOnMarker _placableOnMarker;
    [SerializeField]
    private RayScanner _screenTouchDetector;

    private DictinoryHolder<int, Pony> _ponyHolder = new();

    private Pony _currentSelectedPony;

    private void Awake()
    {
        InitPonies();

        UIEvents.objectChangeEvent.AddListener(OnIdChange);
        UIEvents.objectDeleteEvent.AddListener(OnPonyDelete);

        _screenTouchDetector.SubscribeOnGODetect(OnGODetect);
        _screenTouchDetector.SubscribeOnNothingDetect(ReplacePony);
        _screenTouchDetector.SubscribeOnPlaneDetect(ReplacePony);
    }

    private void InitPonies()
    {
        for (int i = 0; i < _ponyPrefabs.Count; i++)
        {
            Pony pony = Instantiate(_ponyPrefabs[i]);
            _ponyHolder.Registration(pony.id, pony);
            pony.gameObject.SetActive(false);
        }

        _ponyPrefabs = null;

        _currentSelectedPony = _ponyHolder[0];
    }

    private void OnGODetect(GameObject gameObject)
    {
        Pony tempPony = gameObject.GetComponent<Pony>();
        if (tempPony)
            OnPonySelect(tempPony);

    }
    private void ReplacePony()
    {
        print("Replace");

        if (!_currentSelectedPony) return;

        if (!_placableOnMarker.TryPlaceObjectOnMarker(_currentSelectedPony.transform))
        {
            OnDeselectPony();
            return;
        }

        if (!_currentSelectedPony.gameObject.activeSelf)
            _currentSelectedPony.gameObject.SetActive(true);
    }
    private void ReplacePony(Vector3 point)
    {
        print("Replace");

        if (!_currentSelectedPony) return;

        if (!_placableOnMarker.TryPlaceObjectOnMarker(_currentSelectedPony.transform))
        {
            OnDeselectPony();
            return;
        }

        if (!_currentSelectedPony.gameObject.activeSelf)
            _currentSelectedPony.gameObject.SetActive(true);
    }

    private void OnPonySelect(Pony pony)
    {
        _currentSelectedPony = pony;
        UIEvents.objectSelectEvent.Invoke();

        print("Pony");
    }
    private void OnDeselectPony()
    {
        _currentSelectedPony = null;
        UIEvents.objectDeselectEvent.Invoke();
    }

    private void OnIdChange(int id)
    {
        OnPonySelect(_ponyHolder[id]);
    }
    private void OnPonyDelete()
    {
        _currentSelectedPony?.gameObject.SetActive(false);
        OnDeselectPony();
    }
}
