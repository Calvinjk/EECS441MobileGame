using UnityEngine;
using System.Collections;

namespace com.aaronandco.puzzlepotato {
    public class BugController : MonoBehaviour {
        public float speed = 2.1f;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        public float topBound = 5f;
        public float botBound = -5f;
        public float rightBound = 7.5f;
        public float leftBound = -7.5f;

        void FixedUpdate() {
            transform.position += transform.forward * speed;
        }

    }
}