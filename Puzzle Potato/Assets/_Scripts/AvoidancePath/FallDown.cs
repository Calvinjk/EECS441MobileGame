using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.aaronandco.puzzlepotato {
    public class FallDown : MonoBehaviour {

        public float speed = 10f;
        public float lifetime = 40f;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        float speedMultiplier = 0.01f;

        void Update() {
            transform.position = new Vector3(transform.position.x, transform.position.y - (speed * speedMultiplier), 0);

            lifetime -= Time.deltaTime;
            if (lifetime < 0) { Destroy(this.gameObject); }
        }
    }
}