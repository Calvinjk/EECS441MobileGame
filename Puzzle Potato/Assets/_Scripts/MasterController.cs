using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;
using System.Collections.Generic;

namespace com.aaronandco.puzzlepotato {
    public class MasterController : MonoBehaviour{
        public List<GameObject> puzzleOptions;
        public GameObject currentPlayerText;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        Timer timerScript;
        GameManager gameManagerScript;

        void Awake() {
            timerScript = (Timer)GameObject.FindGameObjectWithTag("Timer").GetComponent("Timer");
            gameManagerScript = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");

            timerScript.StartTimer();

            gameManagerScript.ShowCurrentPlayer(currentPlayerText.GetComponent<Text>());

            StartNewGame();
        }

        public void CurrentGameCompleted() {
            // Choose new player
            gameManagerScript.curPlayer = Random.Range(0, gameManagerScript.players.Count - 1);

            // Cleanup and pick new game
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // TODO
        public void StartNewGame() {
            // Decide on what puzzle and start it
            GameObject curPuzzle = Instantiate(puzzleOptions[Random.Range(0, puzzleOptions.Count - 1)]);
            Puzzle curPuzzleScript = (Puzzle)curPuzzle.GetComponent("Puzzle");
            Debug.Log("Attempting to start game: " + curPuzzle.name);
            curPuzzleScript.StartGame();
        }

        // This gets called by the timer when it hits zero
        public void TimeOut() {

        }

        public void Quit() {
            gameManagerScript.players.Clear();
            SceneManager.LoadScene(0);
        }
    }
}