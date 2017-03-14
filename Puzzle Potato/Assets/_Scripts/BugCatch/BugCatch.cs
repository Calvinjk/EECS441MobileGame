using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace com.aaronandco.puzzlepotato {
    public class BugCatch : Puzzle {

        public List<GameObject> bugPrefabs;
        public int minBugs          = 10;
        public int maxBugs          = 30;
        public float minBugSpeed    = 10f;
        public float maxBugSpeed    = 30f;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        public float topBound   =  5f;
        public float botBound   = -5f;
        public float rightBound =  7.5f;
        public float leftBound  = -7.5f;
        public int numBugs;

        void Awake() {
            Initialize();
            numBugs = Random.Range(minBugs, maxBugs + 1);
        }

        public override void StartGame() {
            for (int i = 0; i < numBugs; ++i) {
                int bugType = Random.Range(0, bugPrefabs.Count);
                Quaternion spawnRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
                GameObject bugInstance = Instantiate(bugPrefabs[bugType], new Vector3(Random.Range(leftBound, rightBound), Random.Range(botBound, topBound), 1), spawnRotation) as GameObject;

                BugController bug = (BugController)bugInstance.GetComponent("BugController");
                bug.speed = Random.Range(minBugSpeed, maxBugSpeed);
            }
        }

        public void AllBugsSquished() {
            GameCompleted();
        }
    }
}