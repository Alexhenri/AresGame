using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownCanon : MonoBehaviour
{
    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;

    public float maximumRotation = 60f;
    public float minimumRotation = -10f;
    
    void Update() {
        // Get Mouse Input  
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Up and Down
        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -maximumRotation, -minimumRotation);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;
    }

    public void MoveVertically(float inputY) {
        // Up and Down
        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -maximumRotation, -minimumRotation);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;
    }
}
