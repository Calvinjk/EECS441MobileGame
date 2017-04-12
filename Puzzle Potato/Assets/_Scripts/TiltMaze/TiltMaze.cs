using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.aaronandco.puzzlepotato {
    public class TiltMaze : Puzzle {

        public GameObject ballPrefab;
        public GameObject boundariesPrefab;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        void Start() {
            Instantiate(ballPrefab, new Vector3(-6.5f, 2f, 0f), Quaternion.identity);
            Instantiate(boundariesPrefab);
        }

        public override void StartGame() {
            // TODO -- Move all awake and start shit here.
        }
    }
}