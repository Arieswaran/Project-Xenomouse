using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    [SerializeField, Range(0.05f, 2f)] float colliderThickness = 0.5f;

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out Wall wall)) return;

        Vector3 colliderDistance = collision.contacts[0].point - transform.position;
        Vector3 normalizedColliderDistance = colliderDistance.normalized;
        normalizedColliderDistance.y = 0;
        transform.position = transform.position + normalizedColliderDistance * colliderThickness;
    }
}
