using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

namespace com.aaronandco.puzzlepotato {
    public class GameManager : MonoBehaviour {
        // The GameManager is used to pass variables from the game setup screen to the MasterController which runs the game in the next scene
        // The GameManager will hold all variables needed throughout the game

        public float maxTime = 180f;    // Starting time for the timer when the game starts

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        private static GameManager GMInstance;               // Used to make sure the GameManager doesnt duplicate itself
        public float curTime;                               // How much time is currently left
        public int curPlayer;                               // Which player should be playing
        public List<string> players = new List<string>();   // This is the main player list that will be in effect for the duration of the game
        public Dictionary<string, int> playerWeights = new Dictionary<string, int>();   // This dictionary will help with the new player-selection logic
        public Dictionary<int, int> puzzleWeights = new Dictionary<int, int>();         // This dictionary will help with the new puzzle-selection logic

        //public bool gameInProgress;

        public const int NUM_PUZZLES = 10;

        //settings menu edits this..
        public List<bool> user_puzzle_selections = new List<bool>();
        public List<bool> duration = new List<bool>();

        void Awake() {
            for(int i = 0; i < NUM_PUZZLES; i++){
                //populate user puzzle selection list
                user_puzzle_selections.Add(true);
            }
            //short time selection
            duration.Add(false);
            //medium time selection (DEFAULT)
            duration.Add(true);
            //Long time selection
            duration.Add(false);

            // Make sure the GameManager can persist between scene changes and not duplicate itself
            DontDestroyOnLoad(this);

            if (GMInstance == null) { GMInstance = this; }
            else { Destroy(this.gameObject); }
        }

        public void ShowCurrentPlayer(Text currentPlayerText) {
            currentPlayerText.text = "Current  Player:  " + players[curPlayer];
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); } // Back button on Android
        }
    }
}