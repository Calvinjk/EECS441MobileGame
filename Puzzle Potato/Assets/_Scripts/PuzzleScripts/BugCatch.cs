using UnityEngine;
using System.Collections;

namespace com.aaronandco.puzzlepotato {
    public class BugCatch : Puzzle {

        public GameObject bugPrefab;
        public float minBugSpeed = 2f;
        public float maxBugSpeed = 5f;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        public float topBound   =  5f;
        public float botBound   = -5f;
        public float rightBound =  7.5f;
        public float leftBound  = -7.5f;

        void Awake() {
            Initialize();
        }

        public override void StartGame() {
            GameObject bug = Instantiate(bugPrefab, new Vector3(Random.Range(leftBound, rightBound),Random.Range(botBound, topBound),1), Quaternion.identity) as GameObject;
        }
    }
}