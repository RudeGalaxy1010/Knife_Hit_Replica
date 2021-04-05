using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LogEnemy : MonoBehaviour
{
    public float RotationSpeed = 10f;

    private void Update()
    {
        transform.Rotate(Vector3.back, RotationSpeed * Time.deltaTime);
    }
}
