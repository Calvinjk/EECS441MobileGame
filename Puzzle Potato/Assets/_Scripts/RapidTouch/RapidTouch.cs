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
        public GameObject number;
        public bool debugLogs = true;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        int currentClickCount = 50;
        int curCircleNum;
        GameObject curCircle;
        GameObject clickCounter;
        bool completed = false;

        void Awake() {
            Initialize();
        }

        void Update() {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !completed) { // If user is touching the screen
                // Update counter
                --currentClickCount;
                clickCounter.GetComponent<Text>().text = currentClickCount.ToString();

                // New Circle
                int newCircleNum = Random.Range(0, circleOptions.Count);
                while (newCircleNum == curCircleNum) { newCircleNum = Random.Range(0, circleOptions.Count); } // Make sure same circle never shows up twice in a row
                if (debugLogs) { Debug.Log("Attempting to swap to circle #" + newCircleNum); }

                Destroy(curCircle);
                curCircle = Instantiate(circleOptions[newCircleNum], new Vector3(0, 0, -1), Quaternion.identity);
            }

            if (debugLogs) { Debug.Log("Current click count: " + currentClickCount); }
            if (currentClickCount <= 0 && !completed) {
                GameCompleted();
                completed = true;
            }
        }

        public override void StartGame() {
            // Determine number of clicks
            currentClickCount = Random.Range(minClicks, maxClicks + 1);

            //Spawn ClickCounter
            clickCounter = Instantiate(number, GameObject.Find("Canvas").transform, false);
            clickCounter.GetComponent<Text>().text = currentClickCount.ToString();

            // Spawn initial circle
            curCircleNum = Random.Range(0, circleOptions.Count);
            curCircle = Instantiate(circleOptions[curCircleNum], new Vector3(0, 0, -1), Quaternion.identity);
        }
    }
}