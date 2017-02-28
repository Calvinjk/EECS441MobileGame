using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;
using System.Collections.Generic;

namespace com.aaronandco.puzzlepotato {
    public class GameManager : MonoBehaviour {
        // The GameManager is used to pass variables from the game setup screen to the MasterController which runs the game in the next scene
        // The GameManager will hold all variables needed throughout the game

        public float maxTime = 180f;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        public int curPlayer;
        public List<string> players = new List<string>();   // This is the main player list that will be in effect for the duration of the game

        void Awake() {
            DontDestroyOnLoad(this);
        }
    }
}