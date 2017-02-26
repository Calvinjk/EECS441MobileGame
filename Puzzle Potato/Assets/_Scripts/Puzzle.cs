using UnityEngine;
using System.Collections;

namespace com.aaronandco.puzzlepotato {
    abstract public class Puzzle : MonoBehaviour {

        GameManager gameManagerScript;
        MasterController masterControllerScript;

        // Do not overwrite the Awake() method.  If you would like to use this functionality, use the Start() method instead
        // A reference to the GameManager and its variables is stored here in the gameManagerScript variable if you need it
        void Awake() {
            gameManagerScript = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");
            masterControllerScript = (MasterController)GameObject.Find("MasterController").GetComponent("MasterController");
        }

        // Call this function to let the GameManager know that your game was solved.  No cleanup of your game should be necessary
        void GameCompleted() {
            masterControllerScript.CurrentGameCompleted();
        }

        // This will be called to start your game, so override this method with your game's logic
        // Assume you have an empty scene with a plain black background to work with.  GameManager will make sure this is the case
        abstract public void StartGame();
    }
}