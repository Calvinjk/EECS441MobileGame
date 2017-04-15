using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;
using System.Collections.Generic;

namespace com.aaronandco.puzzlepotato {
    public class MasterController : MonoBehaviour {
        public List<GameObject> puzzleOptions;      // This should hold all of the GameObjects that run the chosen puzzles
        public GameObject currentPlayerText;        
        [Tooltip("-1 will randomly select puzzles, any other number will only select the puzzle that corresponds to that element number")]
        public int developerPuzzleSelection = -1;   // Refer to the tooltip
        public bool debugLogs = true;               // True if you want to see the debug logs (development)

        public GameObject popupPrefab;
        public GameObject playerName;
        public GameObject instrPrefab;
        public GameObject quitPopup;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        GameObject popUp;
        int whichPuzzle;
        Timer timerScript;
        GameManager gameManagerScript;
        Puzzle curPuzzleScript;
        bool popupTimed;

        void Awake() {
            timerScript = (Timer)GameObject.FindGameObjectWithTag("Timer").GetComponent("Timer");
            gameManagerScript = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");

            timerScript.StartTimer();

            gameManagerScript.ShowCurrentPlayer(currentPlayerText.GetComponent<Text>());

            // Set up the initial weights for the first time
            for (int i = 0; i < puzzleOptions.Count; ++i) {
                if (!gameManagerScript.puzzleWeights.ContainsKey(i)) { gameManagerScript.puzzleWeights[i] = 1; }
            }

            StartNewGame();
        }

        public void CurrentGameCompleted() {
            /// Choose new player

            // Sum up all the weights and keyList
            if (debugLogs) { Debug.Log("Starting to sum weights"); }
            int weightSum = -1;
            List<string> keyList = new List<string>();
            foreach (KeyValuePair<string, int> playerInfo in gameManagerScript.playerWeights) {
                if (debugLogs) { Debug.Log(playerInfo.Key); }
                weightSum += playerInfo.Value;
                keyList.Add(playerInfo.Key);
            }

            // Choose a number
            int num = Random.Range(0, weightSum);

            // Figure out which "weight zone" the chosen number is in
            if (debugLogs) { Debug.Log("Starting to figure out zones"); }
            int currentZone = -1;
            string chosenPlayerName = "";

            foreach (string key in keyList) {
                if (debugLogs) { Debug.Log(key); }
                // Figure out the current zone
                currentZone += gameManagerScript.playerWeights[key];

                // Determine if the chosen number lies within this zone
                if (num <= currentZone) {
                    chosenPlayerName = key;
                    break;
                }
            }

            /// We found the next player!  Deal with weights and set the curPlayer

            // Deal with new weights

            foreach (string key in keyList) {
                if (key != chosenPlayerName) {
                    gameManagerScript.playerWeights[key] += 1;
                }  else {
                    gameManagerScript.playerWeights[key] -= 1;
                }
            }

            // Set curPlayer
            for (int i = 0; i < gameManagerScript.players.Count; ++i) {
                if (gameManagerScript.players[i] == chosenPlayerName) {
                    gameManagerScript.curPlayer = i;
                }
            }

            // Show the supposedly public dictionary
            if (debugLogs) {
                Debug.Log("Showing the Player Dictionary");
                foreach (KeyValuePair<string, int> playerInfo in gameManagerScript.playerWeights) {
                    Debug.Log("Key: " + playerInfo.Key + " | Value: " + playerInfo.Value);
                }
            }

            //show the next player
            playerName.GetComponent<Text>().text = gameManagerScript.players[gameManagerScript.curPlayer];
            popUp = Instantiate(popupPrefab, GameObject.Find("Canvas").transform, false);

            // Cleanup and pick new game
            StartCoroutine("nextGame"); 
        }

        IEnumerator nextGame() {
            yield return new WaitForSeconds(1.3f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void StartNewGame() {
            // Decide on what puzzle and start it
            GameObject curPuzzle;
            if (developerPuzzleSelection == -1) {
                // Sum up the weights
                int weightSum = -1;
                for (int i = 0; i < puzzleOptions.Count; ++i) {
                    weightSum += gameManagerScript.puzzleWeights[i];
                }

                // Choose a number
                int num = Random.Range(0, weightSum);

                // Figure out which "weight zone" the chosen number is in
                int currentZone = -1;
                int chosenPuzzleNum = -1;

                for (int i = 0; i < puzzleOptions.Count; ++i) {
                    // Figure out the current zone
                    currentZone += gameManagerScript.puzzleWeights[i];

                    // Determine if the chosen number lies within this zone
                    if (num <= currentZone) {
                        chosenPuzzleNum = i;
                        break;
                    }
                }

                /// We found the next puzzle!  Deal with weights and set the curPlayer
                
                for (int i = 0; i < puzzleOptions.Count; ++i) {
                    if (i != chosenPuzzleNum) {
                        gameManagerScript.puzzleWeights[i] += 1;
                    } else {
                        gameManagerScript.puzzleWeights[i] -= 1;
                    }
                }

                // Set whichPuzzle
                whichPuzzle = chosenPuzzleNum;

                // Show the supposedly public dictionary
                if (debugLogs) {
                    Debug.Log("Showing the Puzzle Dictionary");
                    foreach (KeyValuePair<int, int> puzzleInfo in gameManagerScript.puzzleWeights) {
                        Debug.Log("Key: " + puzzleInfo.Key + " | Value: " + puzzleInfo.Value);
                    }
                }

                curPuzzle = Instantiate(puzzleOptions[whichPuzzle]); 
            }
            else { 
                whichPuzzle = developerPuzzleSelection;
                curPuzzle = Instantiate(puzzleOptions[developerPuzzleSelection]); 
            }

            curPuzzleScript = (Puzzle)curPuzzle.GetComponent("Puzzle");
            if (debugLogs) { Debug.Log("Attempting to start game: " + curPuzzle.name); }

            popUp = Instantiate(instrPrefab, GameObject.Find("Canvas").transform, false);

            if (whichPuzzle == 0) {             // Ordered Touch
                popUp.GetComponentInChildren<Text>().text = "Pop the bubbles!";
            }
            else if (whichPuzzle == 1) {        // Bug Catch
                popUp.GetComponentInChildren<Text>().text = "Squish the bugs!";
            }
            else if (whichPuzzle == 2) {        // Avoidance Path
                popUp.GetComponentInChildren<Text>().text = "Cross the river!";
            }
            else if (whichPuzzle == 3) {        // TTT
                popUp.GetComponentInChildren<Text>().text = "Play tic-tac-toe!";
            } 
            else if (whichPuzzle == 4) {        // Rapid Touch
                popUp.GetComponentInChildren<Text>().text = "Remember how many times to tap!";
            }
            else if (whichPuzzle == 5) {        // Matching
                popUp.GetComponentInChildren<Text>().text = "Find all the matches!";
            }
            else if (whichPuzzle == 6) {        // TimingBar
                popUp.GetComponentInChildren<Text>().text = "Tap when the arrow is in the green!";
            }
            else if (whichPuzzle == 7) {        // Quiz
                popUp.GetComponentInChildren<Text>().text = "Select correct answer(s)!";
            }
            else if (whichPuzzle == 8) {        // TiltMaze
                popUp.GetComponentInChildren<Text>().text = "Tilt the phone to collect the coins!";
            }

            popupTimed = false;
            StartCoroutine("WaitOnPopUp");
        }

        IEnumerator WaitOnPopUp() {
            yield return new WaitForSeconds(.5f);
            popupTimed = true;
        }

        IEnumerator StartGameForReal() {
            yield return new WaitForSeconds(.1f);
            curPuzzleScript.StartGame();
        }

        void Update() {
            // Assume last frame has been dealt with 
            bool screenTouched = false;
            // for hannah testing purposes bc she doesn't like to build it for real
            screenTouched = true;

            // Cycle through touches and if there is a finger that just touched, check if theres a popup ready to delete
            foreach (Touch touch in Input.touches) {
                if (touch.phase == TouchPhase.Began) {
                    screenTouched = true;
                }
            }

            if (screenTouched && popupTimed) {
                Destroy(popUp);
                popupTimed = false;
                StartCoroutine("StartGameForReal");
            }
        }

        // This gets called by the timer when it hits zero
        public void TimeOut() {
            SceneManager.LoadScene(2);
        }

        public void DisplayQuitPopup(bool openPopup) {
            if (openPopup) { // Quit was pressed
                quitPopup.SetActive(true);
                Time.timeScale = 0; // Pause
                if (popUp) {
                    popUp.SetActive(false);
                }
            } else { // No was pressed
                quitPopup.SetActive(false);
                Time.timeScale = 1; // Resume
                if (popUp) {
                    popUp.SetActive(true);
                }
            }
        }        

        public void Quit() { // Yes was pressed
            // Resume time!
            Time.timeScale = 1;

            gameManagerScript.players.Clear();
            SceneManager.LoadScene(0);
        }
    }
}