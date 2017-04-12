using UnityEngine;
using UnityEngine.UI;

using System.Collections;

namespace com.aaronandco.puzzlepotato {
    public class Timer : MonoBehaviour {

        public bool active = false;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        private Text timerText;
        private MasterController masterControllerScript;
        private GameManager gameManagerScript;

        void Awake() {
            timerText = GetComponent<Text>();
            masterControllerScript = (MasterController)GameObject.Find("MasterController").GetComponent("MasterController");
            gameManagerScript = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");

            // Set up the inital time
            gameManagerScript.curTime = gameManagerScript.maxTime;
        }

        // Update is called once per frame
        void Update() {
            if (active) { gameManagerScript.curTime -= Time.deltaTime; }
            if (gameManagerScript.curTime < 0) {
                gameManagerScript.curTime = 0;   // Do not allow the timer below zero
                SendTimeoutAlarm();    // This will only fire if timer is active.  Neat trick
                StopTimer();
            } 
            DisplayTime();
        }

        void DisplayTime() {
            int intLeft = (int)gameManagerScript.curTime;
            int minutes = intLeft / 60;
            int seconds =  intLeft % 60;
            // Need to "fix" seconds so that 5 seconds left shows as :05 and not :5
            string secondString = seconds.ToString();
            if (seconds < 10) { secondString = "0" + seconds.ToString(); }
            timerText.text = minutes.ToString() + ":" + secondString;
        }

        void SendTimeoutAlarm() {
            masterControllerScript.TimeOut();
        }

        public void StartTimer() {
            active = true;
        }

        public void StopTimer() {
            active = false;
        }
    }
}