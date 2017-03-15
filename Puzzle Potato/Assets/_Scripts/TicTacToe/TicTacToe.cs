using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.aaronandco.puzzlepotato {
    public class TicTacToe : Puzzle {

        public GameObject board;
        public Sprite xSprite;
        public Sprite oSprite;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        void Start() {
            StartGame(); // Delete this later, this is for testing
        }

        public override void StartGame() {
            // Create the UI for the game
        }
    }
}