using UnityEngine;
using System.Collections;

namespace com.aaronandco.puzzlepotato {
    public class BugController : MonoBehaviour {
        public float speed = 1f;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        public float topBound = 5f;
        public float botBound = -5f;
        public float rightBound = 7.5f;
        public float leftBound = -7.5f;
        float speedModifier = .01f;

        void FixedUpdate() {
            transform.position += transform.up * speed * speedModifier;
        }

    }
}