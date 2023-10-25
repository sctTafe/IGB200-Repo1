using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGameObject : MonoBehaviour
{
    private float _xRotationSpeed = -55f;
    void Update()
    {
        float rotationAmount = _xRotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, rotationAmount,Space.World);
    }

}
