using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;
using System.Collections.Generic;

namespace com.aaronandco.puzzlepotato {
    public class RapidTouch : Puzzle {

        public List<GameObject> circleOptions;
        public int maxClicks = 50;
        public int minClicks = 25;
        public float showNumberTime = 2f;
        public float checkAnswerIdleTime = 2f;
        public GameObject number;
        public GameObject youLosePanelPrefab;
        public bool debugLogs = true;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        int startingClickCount = 50;
        int currentClickCount = 50;
        int curCircleNum;
        float curTime;
        float curIdleTime;
        GameObject curCircle;
        GameObject clickCounter;
        GameObject losePopup;
        bool completed = false;

        void Awake() {
            Initialize();
            curTime = showNumberTime;
            curIdleTime = checkAnswerIdleTime;
        }

        void Update() {
            // Assume player is idle unless screen touched
            curIdleTime -= Time.deltaTime;

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !completed) { // If user is touching the screen
                // Update counter
                --currentClickCount;
                clickCounter.GetComponent<Text>().text = currentClickCount.ToString();

                // Reset check answer timer
                curIdleTime = checkAnswerIdleTime;

                // New Circle
                int newCircleNum = Random.Range(0, circleOptions.Count);
                while (newCircleNum == curCircleNum) { newCircleNum = Random.Range(0, circleOptions.Count); } // Make sure same circle never shows up twice in a row
                if (debugLogs) { Debug.Log("Attempting to swap to circle #" + newCircleNum + " from circle #" + curCircleNum); }

                Destroy(curCircle);
                curCircle = Instantiate(circleOptions[newCircleNum], new Vector3(0, 0, -1), Quaternion.identity);
                curCircleNum = newCircleNum;
            }

            //if (debugLogs) { Debug.Log("Current click count: " + currentClickCount); }
            if (currentClickCount < -1 && !completed) {
                Restart(true);
                completed = true;
            }

            // Determine if the number should still be shown or not
            if (curTime < 0) {
                clickCounter.SetActive(false);
            } else if (currentClickCount != startingClickCount) { // Don't start the number disappearing countdown until the player has touched the screen
                curTime -= Time.deltaTime;
            }

            // Determine if player has won or not.  Can't lose on the first touch
            if (curIdleTime < 0 && currentClickCount != startingClickCount && !completed) {
                if (currentClickCount == 0) {
                    GameCompleted();
                    completed = true;
                } else {
                    Restart(false);
                }
            }
        }

        public void Restart(bool wentOver) {
            // Make sure no more clicks will be registered.  "Pause"
            completed = true;

            // Make an informative popup
            if (losePopup != null) { Destroy(losePopup); }
            losePopup = Instantiate(youLosePanelPrefab, GameObject.Find("Canvas").transform, false);

            // What should it say?
            if (!wentOver) {
                losePopup.GetComponentInChildren<Text>().text = "Try again!\nYou   clicked " + (startingClickCount - currentClickCount).ToString() + "\ntimes instead of " + (startingClickCount);
            } else {
                losePopup.GetComponentInChildren<Text>().text = "Try again!\nToo many clicks!";
            }

            // Stall a couple seconds for the popup
            StartCoroutine("RestartGame");
        }

        IEnumerator RestartGame() {
            yield return new WaitForSeconds(2f);

            // Restart and reset variables --TODO
            curTime = showNumberTime;
            curIdleTime = checkAnswerIdleTime;

            Destroy(curCircle);
            Destroy(losePopup);
            Destroy(clickCounter);

            completed = false;
            StartGame();
        }

        public override void StartGame() {
            if (debugLogs) { Debug.Log("StartGame() executing"); }

            // Determine number of clicks
            startingClickCount = Random.Range(minClicks, maxClicks + 1);
            currentClickCount = startingClickCount;

            //Spawn ClickCounter
            clickCounter = Instantiate(number, GameObject.Find("Canvas").transform, false);
            clickCounter.GetComponent<Text>().text = currentClickCount.ToString();

            // Spawn initial circle
            curCircleNum = Random.Range(0, circleOptions.Count);
            curCircle = Instantiate(circleOptions[curCircleNum], new Vector3(0, 0, -1), Quaternion.identity);
        }
    }
}