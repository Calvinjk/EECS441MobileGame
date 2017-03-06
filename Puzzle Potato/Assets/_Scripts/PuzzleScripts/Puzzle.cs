using UnityEngine;
using System.Collections;

/// This is the abstract parent class of any puzzle you will be making
/// How to make your own puzzle:
/// 1) Make sure your puzzle is surrounded in: namespace com.aaronandco.puzzlepotato {}
/// 2) Make sure your puzzle inherits from puzzle: public class YourPuzzle : Puzzle {} instead of public class YourPuzzle : MonoBehavior {}
/// 3) Make sure you call Initialize() in your class's Awake() or Start() function
/// 4) Implement public override void StartGame() {} as this will be the entry point for your puzzle
/// 5) Call GameCompleted() when your puzzle has been successfully solved
///  
/// In order to include your puzzle in the game itself:
/// 1) Create an empty GameObject in any scene and attach your script to it.  Name the GameObject after your puzzle
/// 2) Drag your newly created GameObject into the _Prefabs>PuzzleControllers folder to make it a prefab, you may now delete the original out of the scene
/// 3) Click on the MasterController Prefab located in the _Prefabs folder.  Add your puzzle to the MasterController's PuzzleOptions array.  
///     There are a few methods to do this:
///     a) Increasing the size by one and dragging your GameObject into the new empty spot
///     b) Increasing size by one, clicking on the empty spot or the circle next to it and selecting your GameObject
///     b) Simply dragging your GameObject over the PuzzleOptions wording.  This is automatically increase size by one and add your puzzle
/// 4) If you would like the MasterController to always choose your game (for development), go to the MasterController prefab and change the
///     Developer Puzzle Selection variable in the inspector to the number puzzle in the array you would like it to choose.  -1 will make it randomly select a puzzle


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