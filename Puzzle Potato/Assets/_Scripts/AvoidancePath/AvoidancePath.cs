using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.aaronandco.puzzlepotato {
    public class AvoidancePath : Puzzle {

        public GameObject startingArea;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        void Awake() {
            Initialize();
        }

        public override void StartGame() {
            Instantiate(startingArea);
        }
    }
}