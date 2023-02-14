using UnityEngine;

public class GizmoCircle : MonoBehaviour
{
    [SerializeField] Color color;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
