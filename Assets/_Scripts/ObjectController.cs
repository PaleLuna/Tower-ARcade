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
    private ScreenTouchDetector _screenTouchDetector;

    private DictinoryHolder<int, Pony> _ponyHolder = new();

    private Pony _currentSelectedPony;
    private int _currentPonyId = 0;

    private void Awake()
    {
        InitPonies();

        UIEvents.objectChangeEvent.AddListener(OnIdChange);
        UIEvents.objectDeleteEvent.AddListener(OnPonyDelete);
    }

    private void Update()
    {
        if (!_screenTouchDetector.IsScreenTouch()) return;
        print("Touch!");
        Pony pony = _screenTouchDetector.TryGetComponentFromGO<Pony>();
        if (pony)
        {
            _currentSelectedPony = pony;
            _currentPonyId = pony.id;
            UIEvents.objectSelectEvent.Invoke();

            print("Pony");
        }
        else if (_placableOnMarker.TryPlaceObjectOnMarker(_ponyHolder[_currentPonyId].transform))
        {
            print("Replace");

            print(!_ponyHolder[_currentPonyId].gameObject.activeSelf);

            if (!_ponyHolder[_currentPonyId].gameObject.activeSelf)
                _ponyHolder[_currentPonyId].gameObject.SetActive(true);

            _currentSelectedPony = _ponyHolder[_currentPonyId];
        }
        else
        {
            print("Nothing!");
            UIEvents.objectDeselectEvent.Invoke();
        }
    }

    private void InitPonies()
    {
        for(int i = 0; i < _ponyPrefabs.Count; i++)
        {
            Pony pony = Instantiate(_ponyPrefabs[i]);
            _ponyHolder.Registration(pony.id, pony);
            pony.gameObject.SetActive(false);
        }

        _ponyPrefabs = null;

        print(_ponyHolder[0].ponyName);
    }
    private void OnIdChange(int id)
    {
        _currentPonyId = id;
        print($"Current pony: {_ponyHolder[_currentPonyId].ponyName}");
    }
    private void OnPonyDelete()
    {
        _currentSelectedPony?.gameObject.SetActive(false);
        UIEvents.objectDeselectEvent.Invoke();
    }
}
