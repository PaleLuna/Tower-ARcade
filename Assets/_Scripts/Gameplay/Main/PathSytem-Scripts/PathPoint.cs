using UnityEngine;

public class PathPoint : MonoBehaviour
{
    [SerializeField]
    public PathPoint nextPoint;
    [SerializeField]
    private bool _isFinish = false;

    public bool isFinish => _isFinish;


    private void OnDrawGizmosSelected()
    {
        if (!nextPoint) return;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, nextPoint.transform.position);
    }
}
