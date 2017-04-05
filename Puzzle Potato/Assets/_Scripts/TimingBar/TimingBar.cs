using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.aaronandco.puzzlepotato {
    public class TimingBar : Puzzle {

        public float horizontalSpawnLocation = 2f;
        public float maxGoalSpawnLocation = 1.5f;
        public float minSize = 0.5f;
        public float maxSize = 1.5f;

        public GameObject blackBarPrefab;
        public GameObject goalAreaPrefab;
        public GameObject blackArrowPrefab;
        public GameObject failBarPrefab;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        GameObject goalArea;
        GameObject arrow;

        public override void StartGame() {
            // Set up the graphics
            Instantiate(blackBarPrefab, new Vector3(horizontalSpawnLocation, 0, 0), Quaternion.identity);
            goalArea = Instantiate(goalAreaPrefab, new Vector3(horizontalSpawnLocation, Random.Range(-maxGoalSpawnLocation, maxGoalSpawnLocation), -1), Quaternion.identity);
            goalArea.transform.localScale = new Vector3(1, Random.Range(minSize, maxSize), 1);

            arrow = Instantiate(blackArrowPrefab, new Vector3(0, 3, 0), Quaternion.identity);

            // Testing -- TODO
            // Spawn boundaries on goal checking
        }

        void FixedUpdate() {

        }
    }
}