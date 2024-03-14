using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Shell : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private Collider _collider;

    private AmmunitionHolder _ammunitionHolder;

    private Coroutine _timer;

    private float _damage;

    private void OnValidate()
    {
        _rigidbody ??= GetComponent<Rigidbody>();
        _collider ??= GetComponent<Collider>();
    }

    private void Start()
    {
        _collider.isTrigger = true;
    }

    public void SetParentAmmunition(AmmunitionHolder ammunitionHolder) =>
        _ammunitionHolder = ammunitionHolder;

    public void ThrowThis(Vector3 force, float damage)
    {
        if (gameObject.activeSelf) return;

        gameObject.SetActive(true);

        _rigidbody.AddForce(force, ForceMode.Impulse);
        _damage = damage;

        _timer = StartCoroutine(Timer());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamagable>(out IDamagable damagable))
            damagable.SetDamage(_damage);

        Deactivate();
    }

    private void Deactivate()
    {
        _ammunitionHolder.ReturnToCombat(this);

        gameObject.SetActive(false);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(15);

        Deactivate();
        _timer = null;
    }
}
