using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Test : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField, Range(0.2F, 1.5F)]
    private float _radius;

    private void OnValidate()
    {
        _rb ??= GetComponent<Rigidbody>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(_rb.worldCenterOfMass, _radius);
    }
}
