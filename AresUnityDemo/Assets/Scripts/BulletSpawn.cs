using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GameObject objectPrefab; 

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown("space")) {
            SpawnBullet();
        }
    }

    public void SpawnBullet() {
        Vector3 spawnPosition = transform.position;
        Quaternion spawnRotation = transform.rotation;

        Vector3 localXDirection = transform.TransformDirection(Vector3.forward);

        //Instatiate Object
        GameObject bulletObject = Instantiate(objectPrefab, spawnPosition, spawnRotation);
        bulletObject.GetComponent<Bullet>().Setup(localXDirection);

    }
}
