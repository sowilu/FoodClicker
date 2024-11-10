using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 direction = Vector3.forward;

    void Update()
    {
        transform.Rotate(direction * speed * Time.deltaTime);
    }
}
