using UnityEngine;

public class GizmoCircle : MonoBehaviour
{
    [SerializeField] Color color;
    [SerializeField] float verticalOffset = 1f;
    [SerializeField] float radius = 0.5f;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position + (Vector3.up * verticalOffset), radius);
    }
}
