using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.aaronandco.puzzlepotato {

    public class PeriodicMotion : MonoBehaviour {

        public float maxVerticalValue = 3f;
        public float speed = 10f;
        public bool debugLogs = true;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        bool goingDown = true;
    	
    	void FixedUpdate () {
            if (goingDown) {
                if (transform.position.y < -maxVerticalValue) {
                    goingDown = false;
                } else {
                    transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
                }
            } else { // Going Up
                if (transform.position.y > maxVerticalValue) {
                    goingDown = true;
                } else {
                    transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
                }
            }
    	}
    }
}
