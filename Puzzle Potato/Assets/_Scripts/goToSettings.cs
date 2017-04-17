using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace com.aaronandco.puzzlepotato {
    public class goToSettings : MonoBehaviour {
    	//declare scenes for the maker

        //the main PUZZLE POTATO screen
        public GameObject TitlePanel;

		void Awake() 
        {
            // Stop screen from dimming or going into sleep mode
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        public void SetView (int panel)
        {

            if(panel == 1){

                TitlePanel.SetActive(false);
                SceneManager.LoadScene(3);
            } 
            else if(panel == 0){

                SceneManager.LoadScene(0);
            }
        }
    }
}