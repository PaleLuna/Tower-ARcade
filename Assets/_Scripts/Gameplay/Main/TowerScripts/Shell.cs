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

    public void ThrowThis(Vector3 diretction, float initSpeed, float damage)
    {
        if (gameObject.activeSelf) return;

        float force = _rigidbody.mass * initSpeed;

        gameObject.SetActive(true);

        _rigidbody.AddForce(diretction * force, ForceMode.Impulse);
        _damage = damage;

        StartCoroutine(Timer());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable))
            damagable.SetDamage(_damage);

        Deactivate();
    }

    private void Deactivate()
    {
        _ammunitionHolder.ReturnToCombat(this);

        _rigidbody.velocity = Vector3.zero;

        gameObject.SetActive(false);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(15);

        Deactivate();
    }
}
