using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.aaronandco.puzzlepotato {
    public class AvoidancePath : Puzzle {

        public GameObject goalArea;
        public GameObject fallingBlock;
        public GameObject potatoBoatPrefab;
        public GameObject popupPrefab;
        public float spawnFrequency = 0.5f;
        public float minSpeed = 10f;
        public float maxSpeed = 30f;
        public float maxSize = 10f;
        public float minSize = 1f;
        public bool debugLogs = false;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        GameObject potato;
        
        public List<GameObject> blocks;
        GameObject startingAreaInstance;
        GameObject endingAreaInstance;
        public bool inProgress = false;
        public float curTime = 0;
        float topBound = 6f;
        float horizontalBound = 8f;
        public bool complete = false;
        public bool start = false; 
        public GameObject losePopup;

        void Awake() {
            Initialize();
            startingAreaInstance = Instantiate(goalArea, new Vector3(-horizontalBound, 0), Quaternion.identity);
            startingAreaInstance.name = "Starting Area";

            Camera.main.backgroundColor = new Color32(51, 153, 255, 1);

            // Create the "player"
            potato = Instantiate(potatoBoatPrefab);

            endingAreaInstance = Instantiate(goalArea, new Vector3(horizontalBound, 0, 0), Quaternion.identity);
            endingAreaInstance.name = "EndArea";
            endingAreaInstance.GetComponentInChildren<TextMesh>().text = "End";
        }

        void Update() {
            // Check if player is touching the screen
            if (Input.touchCount == 0 || Input.GetTouch(0).phase == TouchPhase.Ended) {
                potato.SetActive(false);
                SwapMode(true);
            }

            if (Input.touchCount == 1) {
                start = true;
                potato.SetActive(true);
                Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                movePotato(new Vector3(wp.x, wp.y, -1f));
            }

            if (inProgress && !complete) {
                start = true; 
                curTime -= Time.deltaTime;
                if (curTime < 0) {
                    curTime = spawnFrequency;
                    float blockSize = Random.Range(minSize, maxSize);
                    int hPos = Random.Range((int)-4, (int)5);
                    GameObject block = Instantiate(fallingBlock, new Vector3(hPos, topBound + (blockSize - 1), 0), Quaternion.identity) as GameObject;
                    block.transform.localScale = new Vector3(1, blockSize, 1);
                    block.name = "Log";

                    FallDown blockScript = (FallDown)block.GetComponent("FallDown");
                    blockScript.speed = Random.Range(minSpeed, maxSpeed);

                    blocks.Add(block);
                }
            }
        }

        void movePotato(Vector3 wp) {
            potato.transform.position = Vector3.Lerp(potato.transform.position, wp, .25f);
        }

        public void SwapMode(bool inGame) {
            if (inGame) {
                // Swap mode
                inProgress = false;

                // Delete all blocks on screen
                foreach (GameObject obj in blocks) {
                    Destroy(obj);
                }
                blocks.Clear();
            } else {
                // Swap mode
                inProgress = true;

                // Bring back the potato!
                potato.SetActive(true);

                // Get rid of thst stupid popup
                if (losePopup != null) { Destroy(losePopup); }

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
                    block.name = "Log";

                    FallDown blockScript = (FallDown)block.GetComponent("FallDown");
                    blockScript.speed = Random.Range(minSpeed, maxSpeed);

                    blocks.Add(block);
                }
            }
        }

        public void ThePotatoDidIt() {
            if (!complete) { GameCompleted(); }
            complete = true;
        }

        public override void StartGame() {

        }
    }
}