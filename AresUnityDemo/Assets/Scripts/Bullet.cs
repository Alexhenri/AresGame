using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed;
    Vector3 prevPos;
    private Vector3 shootDir;

    void Start() {
        moveSpeed = 100f;
    }

    void Update() {
        transform.position += shootDir * moveSpeed * Time.deltaTime;
    }

    public void Setup(Vector3 shootDir) {
        this.shootDir = shootDir;
        Destroy(gameObject, 5f);
    }


}
