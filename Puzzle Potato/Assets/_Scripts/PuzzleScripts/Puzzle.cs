using UnityEngine;
using System.Collections;

namespace com.aaronandco.puzzlepotato {
    abstract public class Puzzle : MonoBehaviour {

        protected GameManager gameManagerScript;
        protected MasterController masterControllerScript;

        // Make sure to call this function first thing in StartGame() or Awake() or Start()
        // A reference to the GameManager and its variables is stored here along with the MasterController
        protected void Initialize() {
            gameManagerScript = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");
            masterControllerScript = (MasterController)GameObject.Find("MasterController").GetComponent("MasterController");
        }

        // Call this function to let the GameManager know that your game was solved.  No cleanup of your game should be necessary
        protected void GameCompleted() {
            masterControllerScript.CurrentGameCompleted();
        }

        // This will be called to start your game, so override this method with your game's logic
        // Assume you have an empty scene with a plain black background to work with.  GameManager will make sure this is the case
        virtual public void StartGame() {}
    }
}