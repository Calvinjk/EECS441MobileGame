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

        public float curTime;                               // How much time is currently left
        public int curPlayer;                               // Which player should be playing
        public List<string> players = new List<string>();   // This is the main player list that will be in effect for the duration of the game
        public Dictionary<string, int> playerWeights = new Dictionary<string, int>();   // This dictionary will help with the new player-selection logic

        void Awake() {
            DontDestroyOnLoad(this);    // Make sure the GameManager can persist between scene changes
        }

        public void ShowCurrentPlayer(Text currentPlayerText) {
            currentPlayerText.text = "Current  Player:  " + players[curPlayer];
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); } // Back button on Android
        }
    }
}