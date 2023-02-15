using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] int rotationAmount = 30;

    private void Update()
    {
        Vector3 targetRotation = transform.eulerAngles + new Vector3(0, rotationAmount, 0);
        transform.rotation = Quaternion.Euler(Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime));
    }
}
