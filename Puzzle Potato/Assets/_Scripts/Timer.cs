using UnityEngine;
using UnityEngine.UI;

using System.Collections;

namespace com.aaronandco.puzzlepotato {
    public class Timer : MonoBehaviour {

        public bool active = false;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        public float timeLeft = 0f;
        private Text timerText;
        private MasterController masterControllerScript;

        void Awake() {
            timerText = GetComponent<Text>();
            masterControllerScript = (MasterController)GameObject.Find("MasterController").GetComponent("MasterController");
        }

        // Update is called once per frame
        void Update() {
            if (active) { timeLeft -= Time.deltaTime; }
            if (timeLeft < 0) {
                timeLeft = 0;   // Do not allow the timer below zero
                SendAlarm();    // This will only fire if timer is active.  Neat trick
            } 
            DisplayTime();
        }

        void DisplayTime() {
            int intLeft = (int)timeLeft;
            int minutes = intLeft / 60;
            int seconds =  intLeft % 60;
            // Need to "fix" seconds so that 5 seconds left shows as :05 and not :5
            string secondString = seconds.ToString();
            if (seconds < 10) { secondString = "0" + seconds.ToString(); }
            timerText.text = minutes.ToString() + ":" + secondString;
        }

        void SendAlarm() {
            masterControllerScript.TimeOut();
        }

        public void StartTimer() {
            active = true;
        }

        public void StopTimer() {
            active = false;
        }

        public void SetTimer(float time) {
            timeLeft = time;
        }
    }
}