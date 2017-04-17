using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.aaronandco.puzzlepotato {
    public class PotatoBoatController : MonoBehaviour {

        public bool debugLogs = false; 

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        AvoidancePath avoidanceScript;

        void Awake() {
            // Set up connection to the master script
            avoidanceScript = (AvoidancePath)GameObject.Find("AvoidancePath(Clone)").GetComponent("AvoidancePath");
        }

        void OnTriggerStay2D(Collider2D col) {
            if (col.gameObject.name == "Starting Area" && !avoidanceScript.inProgress) {
                if (avoidanceScript.losePopup != null) { Destroy(avoidanceScript.losePopup); }
                if (debugLogs) { Debug.Log("Touched Starting Area"); }
                avoidanceScript.SwapMode(false);
            } else if (col.gameObject.name == "EndArea" && avoidanceScript.inProgress ) {
                if (debugLogs) { Debug.Log("Touched Ending Area"); }
                avoidanceScript.ThePotatoDidIt();
            } else if (col.gameObject.name == "Log") {
                if (debugLogs) { Debug.Log("Touched a Log"); }
                avoidanceScript.losePopup = Instantiate(avoidanceScript.popupPrefab, GameObject.Find("Canvas").transform, false);
                avoidanceScript.SwapMode(true);
                gameObject.SetActive(false);
            }
        }
    }
}