using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySlowInBush : MonoBehaviour
{
    [SerializeField] LayerMask bushLayerMask;
    [SerializeField] float radius = 5f;

    public event Action<bool> OnBushStatusChanged;
    bool isInBush;

    private void OnDrawGizmos()
    {
        if (CheckIfInBush())
            Handles.color = Color.blue;
        else
            Handles.color = Color.grey;
        Handles.DrawWireDisc(transform.position, transform.up, radius);
    }

    private void Update()
    {
        bool isInBush = CheckIfInBush();
        if (this.isInBush != isInBush)
        {
            this.isInBush = isInBush;
            OnBushStatusChanged?.Invoke(this.isInBush);
        }
    }

    public bool CheckIfInBush()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, radius, bushLayerMask);

        if (colliderArray.Length < 1)
        {
            return false;
        }
        return true;
    }
}
