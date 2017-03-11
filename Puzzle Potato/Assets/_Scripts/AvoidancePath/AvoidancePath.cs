using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.aaronandco.puzzlepotato {
    public class AvoidancePath : Puzzle {

        public GameObject startingArea;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        public bool inProgress = false;

        void Awake() {
            Initialize();
        }

        void Start() {

        }

        void Update() {
            if (!inProgress) {
                if (Input.touchCount > 0) {
                    Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    Vector2 touchPos = new Vector2(wp.x, wp.y);
                    Collider2D colInfo = Physics2D.OverlapPoint(touchPos);
                    if (colInfo != null) { // StartingArea was touched
                        SwapMode();
                    }
                }
            } else { // inProgress == true

            }
        }

        void SwapMode() {
            /// TODO - 
            /// Spawn all the moving parts and whatnot
            /// Disable starting area
            /// Enable ending area
        }

        public override void StartGame() {
            Instantiate(startingArea);
        }
    }
}