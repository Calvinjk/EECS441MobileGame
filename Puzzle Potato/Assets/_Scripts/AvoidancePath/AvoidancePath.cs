using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.aaronandco.puzzlepotato {
    public class AvoidancePath : Puzzle {

        public GameObject startingArea;
        public GameObject fallingBlock;
        public float spawnFrequency = 0.5f;
        public float maxSize = 10f;
        public float minSize = 1f;
        public bool debugLogs = true;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        public List<GameObject> blocks;
        GameObject startingAreaInstance;
        public bool inProgress = false;
        public float curTime = 0;
        float topBound = 5f;

        void Awake() {
            Initialize();
        }

        void Update() {
            if (!inProgress) {
                if (Input.touchCount > 0) {
                    Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    Vector2 touchPos = new Vector2(wp.x, wp.y);
                    Collider2D colInfo = Physics2D.OverlapPoint(touchPos);
                    if (colInfo != null) { // StartingArea was touched
                        if (debugLogs) { Debug.Log("Player's finger is in starting area"); }
                        SwapMode();
                    }
                }
            } else { // inProgress == true
                curTime -= Time.deltaTime;
                if (curTime < 0) {
                    curTime = spawnFrequency;
                    float blockSize = Random.Range(minSize, maxSize);
                    int hPos = Random.Range((int)-4, (int)5);
                    GameObject block = Instantiate(fallingBlock, new Vector3(hPos, topBound + (blockSize / 2), 0), Quaternion.identity) as GameObject;
                    block.transform.localScale = new Vector3(1, blockSize, 1);

                    blocks.Add(block);
                }
                if (Input.touchCount == 0 || Input.GetTouch(0).phase == TouchPhase.Ended) {
                    if (debugLogs) { Debug.Log("Player lifted finger off screen"); }
                    SwapMode();
                }
            }
        }

        void SwapMode() {
            if (inProgress) {
                // Swap mode
                inProgress = false;

                // Delete all blocks on screen
                foreach (GameObject obj in blocks) {
                    Destroy(obj);
                }
                blocks.Clear();

                // Re-spawn the starting area
                startingAreaInstance = Instantiate(startingArea);
            } else {
                // Swap mode
                inProgress = true;
                Destroy(startingAreaInstance);

                // TODO
                // spawn in a bunch of blocks to start (one each horizontally ?)
                // Disable starting area
            }
        }

        public override void StartGame() {
            startingAreaInstance = Instantiate(startingArea);
        }
    }
}