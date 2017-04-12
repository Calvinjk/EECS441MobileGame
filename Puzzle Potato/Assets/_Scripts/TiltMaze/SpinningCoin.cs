using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.aaronandco.puzzlepotato {
    public class SpinningCoin : MonoBehaviour {

        public float rotationSpeed = 1f;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        // Update is called once per frame
        void FixedUpdate() {
            transform.Rotate(-Vector3.up * (rotationSpeed * Time.deltaTime));
        }
    }
}