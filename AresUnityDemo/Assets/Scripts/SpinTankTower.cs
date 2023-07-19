using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinTankTower : MonoBehaviour 
{
    public float mouseSensitivity = 2f;

    void Update() {
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up * inputX);
    }
}
