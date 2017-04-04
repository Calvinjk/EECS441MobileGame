using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.aaronandco.puzzlepotato {
    public class PotatoBoatController : MonoBehaviour {

        public bool debugLogs = true;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        AvoidancePath avoidanceScript;

        void Awake() {
            // Set up connection to the master script
            avoidanceScript = (AvoidancePath)GameObject.Find("AvoidancePath(Clone)").GetComponent("AvoidancePath");
        }

        void OnTriggerStay2D(Collider2D col) {
            if (col.gameObject.name == "Starting Area" && !avoidanceScript.inProgress) {
                if (debugLogs) { Debug.Log("Touched Starting Area"); }
                avoidanceScript.SwapMode(false);
            } else if (col.gameObject.name == "EndArea" && avoidanceScript.inProgress ) {
                if (debugLogs) { Debug.Log("Touched Ending Area"); }
                avoidanceScript.ThePotatoDidIt();
            } else if (col.gameObject.name == "Log") {
                if (debugLogs) { Debug.Log("Touched a Log"); }
                avoidanceScript.SwapMode(true);
                gameObject.SetActive(false);
            }
        }
    }
}