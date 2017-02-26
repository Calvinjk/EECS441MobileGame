using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;
using System.Collections.Generic;

namespace com.aaronandco.puzzlepotato {
    public class GameManager : MonoBehaviour {

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        public int curPlayer;
        bool inGame = false;
        // This is the main player list that will be in effect for the duration of the game
        public List<string> players = new List<string>();

        void Awake() {
            DontDestroyOnLoad(this);
        }
    }
}