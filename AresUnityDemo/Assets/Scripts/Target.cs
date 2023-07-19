using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour 
{
    public int movePattern;
    private bool movingRight; 
    private int auxDirection = 0;
    private int maximumDirection = 300;
    private float moveSpeed; 
	public float frequency; 
    public	float magnitude; 
    private float timeCounter;
    public float radius;
    Vector3 pos;

    public ParticleSystem deathParticles1, deathParticles2, deathParticles3;

    private void HorizontalPattern() {
        var direction = movingRight ? 1 : -1;

		pos += direction * (transform.forward * Time.deltaTime * moveSpeed);
        transform.position = pos;
    }
    private void CircularPattern() {
        radius = 0.1f;
        frequency = 2f;
        timeCounter += Time.deltaTime*frequency;
        pos.x += Mathf.Cos(timeCounter) * radius;
        pos.y += Mathf.Sin(timeCounter)* radius;
        transform.position = new Vector3 (pos.x, pos.y, pos.z);
    }
    private void SinusWavePattern() {
        var direction = movingRight ? 1 : -1;

		pos += direction * (transform.forward * Time.deltaTime * moveSpeed);
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
	}
	void MoveRight() {
		pos += transform.forward * Time.deltaTime * moveSpeed;
        if (movePattern == 2)
            transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
        else
            transform.position = pos;
		
	}

	void MoveLeft() {
		pos -= transform.forward * Time.deltaTime * moveSpeed;
		if (movePattern == 2)
            transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
        else
            transform.position = pos;
	}

    void MoveControl() {   
        MoveDirectionControl();

        if (movePattern == 0)
            HorizontalPattern();
        else if (movePattern == 1)
            CircularPattern();
        else
            SinusWavePattern();
	}

    void MoveDirectionControl() {
        if (auxDirection >= maximumDirection) {
            movingRight = !movingRight;
            auxDirection = 0;
        }
        auxDirection += 1;
    }

    void Start() {
        // First we start all random variables

        movePattern = Random.Range(0 ,3);
        movingRight = (Random.value > 0.5f);
        moveSpeed = Random.Range(1f, 3f);
        frequency = 3f;
        magnitude = Random.Range(1f, 3f);
        
        // Get position 
        pos = transform.position;

        timeCounter = 0;
        radius = 0.3f;
    }

    void Update() {

        MoveControl();
    }

    void OnTriggerEnter(Collider other) {

        pos = transform.position;
        
        ParticleSystem particles1 = Instantiate(deathParticles1, pos, Quaternion.identity);
        ParticleSystem particles2 = Instantiate(deathParticles2, pos, Quaternion.identity);
        ParticleSystem particles3 = Instantiate(deathParticles3, pos, Quaternion.identity);

        Destroy(particles1, 5f);
        Destroy(particles2, 5f);
        Destroy(particles3, 5f);

        Destroy(gameObject);
    }
}
