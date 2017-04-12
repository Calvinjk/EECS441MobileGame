using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBall : MonoBehaviour {

    public float speed = .1f;

    public bool ____________________________;  // Separation between public and "private" variables in the inspector

    float vBound = 2.5f;
    float hBound = 7f;

    // Use this for initialization
    void Start () {
        // Change direction of gravity to induce rolling
        Physics.gravity = new Vector3(0f, 0f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
        // Move Ball
        //transform.position = new Vector3(transform.position.x + Input.acceleration.x * speed, transform.position.y + Input.acceleration.y * speed, 0f);
        GetComponent<Rigidbody>().AddForce(new Vector3(Input.acceleration.x * speed, Input.acceleration.y * speed, 0));

        // Check bounds
        //if (transform.position.y > vBound) { transform.position = new Vector3(transform.position.x, vBound, 0); }
        //if (transform.position.y < -vBound) { transform.position = new Vector3(transform.position.x, -vBound, 0); }
        //if (transform.position.x > hBound) { transform.position = new Vector3(hBound, transform.position.y, 0); }
        //if (transform.position.x < -hBound) { transform.position = new Vector3(-hBound, transform.position.y, 0); }

    }
}
