using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace com.aaronandco.puzzlepotato{
    public class OrderedTouch : Puzzle {
        public int maxLocations = 1;    // Maximum number of locations to spawn
        public float circleSize = 2f;   // How large of a circle to spawn
        public float minDist = 2.5f;    // How far apart circles must be
        public float hOffset = 5f;      
        public float vOffset = 5f;
        public GameObject circlePrefab;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        public float topBound;
        public float botBound;
        public float rightBound;
        public float leftBound;
        public List<GameObject> circles;

        void Awake() {
            topBound = 5f - 5f / vOffset;
            botBound = -topBound;
            rightBound = 7.5f - 7.5f / hOffset;
            leftBound = -rightBound;
        }

        public override void StartGame() {
            Debug.Log("Started Ordered Touch");
            // Each run through the for loop will spawn a circle randomly on the screen
            // TODO -- only try 5 times?
            for (int i = 0; i < maxLocations; ++i) {
                Vector2 spawnPos = new Vector2(Random.Range(rightBound, leftBound), Random.Range(botBound, topBound));
                while (minDistanceToCircle(spawnPos) < minDist) {
                    spawnPos = new Vector2(Random.Range(rightBound, leftBound), Random.Range(botBound, topBound));
                }

                circles.Add((GameObject)Instantiate(circlePrefab, spawnPos, Quaternion.identity));
                Debug.Log("Placing circle at (" + spawnPos.x + ", " + spawnPos.y + ")");
            }
        }

        float minDistanceToCircle(Vector2 pos) {
            if (circles.Count == 0) { return minDist; } // Always put a circle down if none have been placed

            float answer = Mathf.Sqrt(Mathf.Pow(pos.x - circles[0].transform.position.x, 2) + Mathf.Pow(pos.y - circles[0].transform.position.y, 2));

            for (int i = 1; i < circles.Count; ++i) {
                float dist = Mathf.Sqrt(Mathf.Pow(pos.x - circles[i].transform.position.x, 2) + Mathf.Pow(pos.y - circles[i].transform.position.y, 2));
                if (dist < answer) { dist = answer; }
            }
            return answer;
        }
    }
}
