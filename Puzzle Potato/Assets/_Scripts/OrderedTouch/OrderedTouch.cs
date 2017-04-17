using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace com.aaronandco.puzzlepotato {
    public class OrderedTouch : Puzzle {
        public GameObject circlePrefab; // What the circles look like
        public bool debugLogs = true;   // True if you want to see the debug logs for this puzzle (development)
        public GameObject popupPrefab;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector
        
        public int minLocations = 3;    // Minimum number of locations to spawn
        public int maxLocations = 5;    // Maximum number of locations to spawn
        public float circleSize = 0.1f;   // How large of a circle to spawn
        public float minDist = 2f;    // How far apart circles must be
        public int maxAttempts = 10;    // Number of attempts to place a circle before it gives up and skips to the next one

        public int attemptNum = 1;      // Current touch number.  Used to make sure user has touched the "correct" circle
        public int numLocations;
        public float topBound;
        public float botBound;
        public float rightBound;
        public float leftBound;

        public List<GameObject> circles;
        public bool pause; 
        public GameObject losePopup;

        void Awake() {
            Initialize();

            // Calculate the offsets
            topBound = 3f;
            botBound = -topBound;
            rightBound = 6.5f;
            leftBound = -rightBound;
            pause = false;
        }

        void Update() {
            if (Input.GetMouseButtonDown(0) && !pause) {
            // if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended && !pause) {     // Make sure only one finger was used and it is coming off the screen
                Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);   
                // Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);    // Get the world coordinates of the screen touch
                Vector2 touchPos = new Vector2(wp.x, wp.y);                                 // We have a 2D game, so turn the 3d coordinates into 2D (we dont care about z)
                Collider2D colInfo = Physics2D.OverlapPoint(touchPos);                      // Determine if this 2D point is within any colliders
                if (colInfo != null) { // If something was touched
                    if (debugLogs) { Debug.Log("Touched circle #" + colInfo.gameObject.GetComponentInChildren<TextMesh>().text); }
                    if (colInfo.gameObject.GetComponentInChildren<TextMesh>().text == attemptNum.ToString()) {
                        // Correct player input!  Disable the circle.
                        if (debugLogs) { Debug.Log("Disabling circle #" + attemptNum); }
                        ++attemptNum;
                        StartCoroutine("PopBubble", colInfo);

                        // Check to see if player won
                        if (attemptNum > numLocations) {
                            if (debugLogs) { Debug.Log("Completed OrderedTouch, calling GameCompleted()"); }
                            GameCompleted();
                        }
                    }
                    else {
                        // Player touched an incorrectly-numbered circle.  Restart the game
                        Restart();
                    }
                }
            }
        }

        IEnumerator PopBubble(Collider2D bubble) {
            bubble.gameObject.GetComponent<ParticleSystem>().Emit(10);
            yield return new WaitForSeconds(.07f);
            bubble.gameObject.SetActive(false);
        }

        public override void StartGame() {
            // Each run through the for loop will spawn a circle randomly on the screen
            // TODO -- If a circle is skipped, wrong number will appear
            Debug.Log("Starting new game");
            if (losePopup != null) { Destroy(losePopup); }
            pause = false; 

            numLocations = Random.Range(minLocations, maxLocations + 1);    // Determine how many circles we are going to put down
            for (int i = 0; i < numLocations; ++i) {
                int attemptNum = 0;
                Vector2 spawnPos = new Vector2(Random.Range(rightBound, leftBound), Random.Range(botBound, topBound));  // Attempt to determine a spawn position
                while (minDistanceToCircle(spawnPos) < minDist) {   // If this attempt is too close to another circle, try again
                    if (debugLogs) {
                        Debug.Log("Attempting to place circle " + i + " at " + minDistanceToCircle(spawnPos) + " dist.  Attempt #" + attemptNum);
                    }
                    ++attemptNum;
                    if (attemptNum > maxAttempts) { // Break out of trying to find a pos (too many tries)
                        if (debugLogs) { Debug.Log("Giving up on circle " + i); }
                        --numLocations;
                        --i;
                        break;
                    } 
                    spawnPos = new Vector2(Random.Range(rightBound, leftBound), Random.Range(botBound, topBound));
                }
                if (attemptNum > maxAttempts) { continue; } // Skip to next circle
                GameObject placedCircle = circlePrefab;                                         // Create the GameObject we will place
                placedCircle.transform.localScale = new Vector3(circleSize, circleSize, circleSize);     // Scale to desired size
                placedCircle.GetComponentInChildren<TextMesh>().text = (i + 1).ToString();      // Change the text on the circle to the correct number
                placedCircle.GetComponent<ParticleSystem>().enableEmission = false;

                circles.Add((GameObject)Instantiate(placedCircle, spawnPos, Quaternion.identity));          // Actually put the circle in the scene
                
                if (debugLogs) { Debug.Log("Placing circle of size at (" + spawnPos.x + ", " + spawnPos.y + ")"); } 
            }
        }

        void Restart() {
            if (debugLogs) { Debug.Log("Restarting puzzle"); }
            StartCoroutine("RestartingGame");
            losePopup = Instantiate(popupPrefab, GameObject.Find("Canvas").transform, false);
            attemptNum = 1;     // Reset the number circle needed to touch
        }

        IEnumerator RestartingGame() {
            pause = true;
            yield return new WaitForSeconds(.5f);
            foreach (GameObject circle in circles) {    // Kill all circles
                Destroy(circle);
            }
            circles.Clear();    // Clear the circle list of the now-empty references
            StartGame();        // Now that everything is reset, start the puzzle again
        }

        // Finds the nearest circle to the given pos and returns the distance to it
        float minDistanceToCircle(Vector2 pos) {
            if (circles.Count == 0) { return minDist; } // Always put a circle down if none have been placed

            float answer = Mathf.Sqrt(Mathf.Pow(pos.x - circles[0].transform.position.x, 2) + Mathf.Pow(pos.y - circles[0].transform.position.y, 2));

            for (int i = 1; i < circles.Count; ++i) {
                float dist = Mathf.Sqrt(Mathf.Pow(pos.x - circles[i].transform.position.x, 2) + Mathf.Pow(pos.y - circles[i].transform.position.y, 2));
                if (dist < answer) { answer = dist; }
            }
            return answer;
        }
    }
}
