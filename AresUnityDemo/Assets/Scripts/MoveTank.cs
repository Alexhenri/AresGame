using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour 
{
    public float moveSpeed = 7.0f;
    public float rotationSpeed = 120.0f;
    public GameObject[] leftWheels;
    public GameObject[] rightWheels;
    public float wheelRotationSpeed = 200.0f;
    private Rigidbody rb;

    private float moveInput;
    private float rotationInput;

    void Start() {
        rb = GetComponent<Rigidbody>();

    }

    void Update() {
        moveInput = Input.GetAxis("Vertical");
        rotationInput = Input.GetAxis("Horizontal");

        RotateWheels(moveInput, rotationInput);
        
    }
    void FixedUpdate() {
        MoveTank(moveInput);
        RotateTank(rotationInput);

    }
    
    public void MoveTank(float input) {
        Vector3 moveDirection = transform.forward * input * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDirection);
    }
    public void RotateTank(float input) {
        float rotation = input * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
    public void RotateWheels(float moveInput, float rotationInput) {

        float wheelRotation = moveInput * rotationSpeed * Time.deltaTime;

        //Move the left wheels
        foreach(GameObject wheel in leftWheels) {
            if (wheel != null) {
                wheel.transform.Rotate(wheelRotation - rotationInput * wheelRotationSpeed * Time.deltaTime, 0.0f, 0.0f);
            }
        }

        //Move the left wheels
        foreach(GameObject wheel in rightWheels) {
            if (wheel != null) {
                wheel.transform.Rotate(wheelRotation + rotationInput * wheelRotationSpeed * Time.deltaTime, 0.0f, 0.0f);
            }
        }
    }
}
