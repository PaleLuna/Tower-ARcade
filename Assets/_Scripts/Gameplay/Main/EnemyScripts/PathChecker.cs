using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PathChecker : MonoBehaviour
{
    [SerializeField]
    private Collider _collider;
    [SerializeField]
    private Enemy parentEnemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PathPoint pathPoint))
        {
            if (pathPoint.isFinish)
                parentEnemy.FinishPointReached();
            else
                parentEnemy.PathPointReached(pathPoint);
        }
    }
}
