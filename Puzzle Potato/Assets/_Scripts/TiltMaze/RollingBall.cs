using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.aaronandco.puzzlepotato {
    public class RollingBall : MonoBehaviour {

        public float speed = .1f;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        int numCoins;

        void Start() {
            // Change direction of gravity to induce rolling
            Physics.gravity = new Vector3(0f, 0f, 20f);
        }

        void FixedUpdate() {
            // Move Ball
            GetComponent<Rigidbody>().AddForce(new Vector3(Input.acceleration.x * speed, Input.acceleration.y * speed, 0));
        }

        void OnTriggerEnter(Collider coll) {
            if (coll.gameObject.tag == "Coin") { // We hit a coin!
                TiltMaze tiltMazeScript = (TiltMaze)GameObject.Find("TiltMaze(Clone)").GetComponent("TiltMaze");
                tiltMazeScript.RemoveCoin(coll.gameObject);            
            }
        }
    }
}