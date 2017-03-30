using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace com.aaronandco.puzzlepotato {
    public class BugController : MonoBehaviour {
        public float speed = 1f;
        public Sprite splatIcon; 

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        public float topBound = 5f;
        public float botBound = -5f;
        public float rightBound = 7.5f;
        public float leftBound = -7.5f;
        float speedModifier = .01f;
        BugCatch bugCatchScript;

        void Start() {
            bugCatchScript = (BugCatch)GameObject.Find("BugCatch(Clone)").GetComponent("BugCatch");
        }

        void FixedUpdate() {
            Vector3 posAttempt = transform.position + transform.up * speed * speedModifier;

            if (isWithinBounds(posAttempt)) { transform.position = posAttempt; }
            else {
                Bounce(posAttempt);
            }

            // if (Input.GetMouseButtonDown(0)) { // If user is touching the screen
            if (Input.touchCount > 0) { // If user is touching the screen
                // Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);    // Get the world coordinates of the screen touch
                Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);    // Get the world coordinates of the screen touch
                Vector2 touchPos = new Vector2(wp.x, wp.y);                                 // We have a 2D game, so turn the 3d coordinates into 2D (we dont care about z)
                Collider2D colInfo = Physics2D.OverlapPoint(touchPos);
                if (colInfo != null) { // If something was touched
                    --bugCatchScript.numBugs;
                    colInfo.gameObject.SetActive(false);

                    GameObject splat = new GameObject("SPLAT");
                    splat.AddComponent<SpriteRenderer>().sprite = splatIcon;
                    splat.GetComponent<Transform>().position = new Vector3(wp.x, wp.y, 0); 
                    splat.GetComponent<Transform>().localScale = new Vector3(.7f, .7f, .7f);

                    // Check to see if player won
                    if (bugCatchScript.numBugs == 0) {
                        bugCatchScript.AllBugsSquished();
                    }
                }
            }
        }

        bool isWithinBounds(Vector3 pos) {
            return (pos.x > leftBound) && (pos.x < rightBound) && (pos.y > botBound) && (pos.y < topBound);
        }

        void Bounce(Vector3 pos) {
            if (pos.x < leftBound || pos.x > rightBound) {
                transform.up = Vector3.Reflect(pos - transform.position, Vector3.right);
            }
            if (pos.y < botBound || pos.y > topBound) {
                transform.up = Vector3.Reflect(pos - transform.position, Vector3.up);
            }
        }
    }
}