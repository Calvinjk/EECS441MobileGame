using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBall : MonoBehaviour {

    public float speed = .1f;

    public bool ____________________________;  // Separation between public and "private" variables in the inspector

    int numCoins;

    void Start () {
        // Change direction of gravity to induce rolling -- TODO doesnt work.
        Physics.gravity = new Vector3(0f, 0f, 1f);
	}
	
	void FixedUpdate () {
        // Move Ball
        GetComponent<Rigidbody>().AddForce(new Vector3(Input.acceleration.x * speed, Input.acceleration.y * speed, 0));

    }
}
