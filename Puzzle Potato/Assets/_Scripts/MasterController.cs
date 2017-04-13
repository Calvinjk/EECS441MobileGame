using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;
using System.Collections.Generic;

namespace com.aaronandco.puzzlepotato {
    public class MasterController : MonoBehaviour{
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

            StartNewGame();
        }

        public void CurrentGameCompleted() {
            // Choose new player
            gameManagerScript.curPlayer = Random.Range(0, gameManagerScript.players.Count);

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
                whichPuzzle = Random.Range(0, puzzleOptions.Count);
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
                popUp.GetComponentInChildren<Text>().text = "Select correct answer(s).\nIf you select an incorrect answer,\nyou lose!";
            }
            else if (whichPuzzle == 8) {        // TiltMaze
                popUp.GetComponentInChildren<Text>().text = "Tilt the phone to collect the coins!";
            }

            popupTimed = false;
            StartCoroutine("WaitOnPopUp");

        }

        IEnumerator WaitOnPopUp()
        {
            yield return new WaitForSeconds(.5f);
            popupTimed = true;
        }

        IEnumerator StartGameForReal() {
            yield return new WaitForSeconds(.1f);
            curPuzzleScript.StartGame();
        }

        void Update()
        {
            // Assume last frame has been dealt with 
            bool screenTouched = false;

            // Cycle through touches and if there is a finger that just touched, check if theres a popup ready to delete
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    screenTouched = true;
                }
            }

            if (screenTouched && popupTimed)
            {
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