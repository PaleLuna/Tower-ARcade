using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private const int TIME_TO_DESTROY = 15;

    [SerializeField] private Rigidbody _rigidbody;

    private float _damage = 0;

    private Coroutine _timer;

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public void ThrowBullet(Vector3 direction)
    {
        _rigidbody.AddForce(direction * 15F, ForceMode.Impulse);

        _timer = StartCoroutine(Timer());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!TryGetDamagable(other, out IDamagable damagable)) return;

        damagable.SetDamage(_damage);

        DestroyGameObject();
    }

    private bool TryGetDamagable(Collider collider, out IDamagable damagable) => (damagable = collider.GetComponent<IDamagable>()) != null;

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(TIME_TO_DESTROY);

        _timer = null;
        DestroyGameObject();
    }

    private void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }
}
