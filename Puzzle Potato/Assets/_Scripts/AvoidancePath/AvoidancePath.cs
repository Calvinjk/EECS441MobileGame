﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.aaronandco.puzzlepotato {
    public class AvoidancePath : Puzzle {

        public GameObject goalArea;
        public GameObject fallingBlock;
        public Sprite cutePotato;
        public float spawnFrequency = 0.5f;
        public float minSpeed = 10f;
        public float maxSpeed = 30f;
        public float maxSize = 10f;
        public float minSize = 1f;
        public bool debugLogs = true;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        GameObject potato;
        
        public List<GameObject> blocks;
        GameObject startingAreaInstance;
        GameObject endingAreaInstance;
        public bool inProgress = false;
        public float curTime = 0;
        float topBound = 5f;
        float horizontalBound = 8f;
        bool complete = false;

        void Awake() {
            Initialize();
            startingAreaInstance = Instantiate(goalArea, new Vector3(-horizontalBound, 0), Quaternion.identity);
            startingAreaInstance.name = "Starting Area";

            Camera.main.backgroundColor = new Color32(51, 153, 255, 1);

            potato = new GameObject("POTATO");
            potato.AddComponent<SpriteRenderer>().sprite = cutePotato;
            potato.GetComponent<Transform>().position = new Vector3(-7f, 0, -1f);
            potato.GetComponent<Transform>().localScale = new Vector3(.25f, .25f, .25f);

            endingAreaInstance = Instantiate(goalArea, new Vector3(horizontalBound, 0, 0), Quaternion.identity);
            endingAreaInstance.name = "EndArea";
            endingAreaInstance.GetComponentInChildren<TextMesh>().text = "End";
        }

        void Update() {

            if (!complete) {
                if (!inProgress) {
                    if (Input.touchCount == 1) {
                        Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                        Vector2 touchPos = new Vector2(wp.x, wp.y);
                        Collider2D colInfo = Physics2D.OverlapPoint(touchPos);
                        moveSprite(wp);

                        if (colInfo != null && colInfo.name == "Starting Area") { // StartingArea was touched
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

                        FallDown blockScript = (FallDown)block.GetComponent("FallDown");
                        blockScript.speed = Random.Range(minSpeed, maxSpeed);

                        blocks.Add(block);
                    }
                    if (Input.touchCount == 0 || Input.GetTouch(0).phase == TouchPhase.Ended) {
                        if (debugLogs) { Debug.Log("Player lifted finger off screen"); }
                        SwapMode();
                    }

                    // Check if player won or touched a falling block
                    Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    Vector2 touchPos = new Vector2(wp.x, wp.y);
                    Collider2D colInfo = Physics2D.OverlapPoint(touchPos);
                    moveSprite(wp);
                    if (colInfo != null) {
                        if (colInfo.gameObject.name == "EndArea") {
                            GameCompleted();
                            complete = true;
                        } else if (colInfo.gameObject.name != "Starting Area") { // Falling block was touched
                            if (debugLogs) { Debug.Log("Player touched a block"); }
                            SwapMode();
                        }
                    }
                }
            }
        }

        void moveSprite(Vector3 wp) {
            potato.GetComponent<Transform>().position = new Vector3(wp.x, wp.y, -1f); 
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
                //startingAreaInstance = Instantiate(goalArea, new Vector3(-horizontalBound, 0, 0), Quaternion.identity);
                //startingAreaInstance.name = "Starting Area";
                //Destroy(endingAreaInstance);
            } else {
                // Swap mode
                inProgress = true;

                //Destroy(startingAreaInstance);
                //endingAreaInstance = Instantiate(goalArea, new Vector3(horizontalBound, 0, 0), Quaternion.identity);
                //endingAreaInstance.name = "EndArea";
                //endingAreaInstance.GetComponentInChildren<TextMesh>().text = "End";

                //Spawn a bunch of starting blocks
                List<int> positions = new List<int>();
                for (int i = 0; i < 6; ++i) {                   
                    float blockSize = Random.Range(minSize, maxSize);
                    int hPos = Random.Range((int)-4, (int)5);
                    for (int j = 0; j < positions.Count; ++j) {
                        if (positions[j] == hPos) {
                            hPos = Random.Range((int)-4, (int)5);
                            j = 0;
                        }
                    }
                    positions.Add(hPos);

                    float vPos = Random.Range(-2f, 5f);

                    GameObject block = Instantiate(fallingBlock, new Vector3(hPos, vPos, 0), Quaternion.identity) as GameObject;
                    block.transform.localScale = new Vector3(1, blockSize, 1);

                    FallDown blockScript = (FallDown)block.GetComponent("FallDown");
                    blockScript.speed = Random.Range(minSpeed, maxSpeed);

                    blocks.Add(block);
                }
            }
        }

        public override void StartGame() {

        }
    }
}