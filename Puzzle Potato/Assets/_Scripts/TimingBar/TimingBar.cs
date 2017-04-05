using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.aaronandco.puzzlepotato {
    public class TimingBar : Puzzle {

        public float horizontalSpawnLocation = 2f;
        public float maxGoalSpawnLocation = 1.5f;
        public float minSize = 0.5f;
        public float maxSize = 1.5f;
        public float cooldownLength = 0.5f;

        public GameObject blackBarPrefab;
        public GameObject goalAreaPrefab;
        public GameObject blackArrowPrefab;
        public Sprite coolingDownArrow;
        public GameObject failBarPrefab;

        public bool debugLogs = true;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        GameObject goalArea;
        GameObject arrow;
        Sprite regularArrowSprite;
        float topBoundary;
        float botBoundary;
        float currentCooldown;
        bool touchCooldown = false;

        void Awake() {
            Initialize();
        }

        public override void StartGame() {
            // Set up the graphics
            Instantiate(blackBarPrefab, new Vector3(horizontalSpawnLocation, 0f, 0f), Quaternion.identity);
            goalArea = Instantiate(goalAreaPrefab, new Vector3(horizontalSpawnLocation, Random.Range(-maxGoalSpawnLocation, maxGoalSpawnLocation), -1), Quaternion.identity);
            goalArea.transform.localScale = new Vector3(1f, Random.Range(minSize, maxSize), 1f);

            arrow = Instantiate(blackArrowPrefab, new Vector3(-1f, 3f, 0f), Quaternion.identity);
            regularArrowSprite = arrow.GetComponent<SpriteRenderer>().sprite;

            topBoundary = goalArea.transform.position.y + (goalArea.transform.localScale.y / 2);
            botBoundary = goalArea.transform.position.y - (goalArea.transform.localScale.y / 2);
        }

        void FixedUpdate() {
            float arrowPos = arrow.transform.position.y;
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !touchCooldown) {
                if (arrowPos > botBoundary && arrowPos < topBoundary) {
                    if (debugLogs) { Debug.Log("TimingBar was completed"); }
                    GameCompleted();
                } else {    // Touched when arrow was outside of goal
                    // Only spawn a failBar inside of the boundaries
                    if (arrowPos > -(blackBarPrefab.transform.localScale.y / 2) && arrowPos < (blackBarPrefab.transform.localScale.y / 2)) {
                        Instantiate(failBarPrefab, new Vector3(0f, arrowPos, -1f), Quaternion.identity);
                    }
                    touchCooldown = true;
                    currentCooldown = cooldownLength;

                    // Swap out the sprite
                    arrow.GetComponent<SpriteRenderer>().sprite = coolingDownArrow;
                }
            }

            if (touchCooldown) {
                if (currentCooldown < 0) {
                    touchCooldown = false;
                    arrow.GetComponent<SpriteRenderer>().sprite = regularArrowSprite;
                }
                else { currentCooldown -= Time.deltaTime; }
            }
        }
    }
}