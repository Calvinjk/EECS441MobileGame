using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace com.aaronandco.puzzlepotato {
    public class settingsManager : Puzzle {

        //the main PUZZLE POTATO screen
        public GameObject mainSettingsPanel;
    	//the main screen of the quiz maker
        public GameObject quizMakerMenu;
        //The start interface for adding a question
    	public GameObject addQuestionPanel;
    	//The next step in question creation, adding an incorrect answer pool
    	public GameObject incorrectAnswersPanel;
    	//A seperate menu for editing already made questions
    	public GameObject viewQuestionSetPanel;

        private GameObject tempObject;
		void Awake() 
        {
            SetView(1);
            // Stop screen from dimming or going into sleep mode
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        public void SetView (int panel)
        {


            switch(panel){


                case 0:
                    //go back to puzzle potato title screen
                    SceneManager.LoadScene(0);
                    break;
                case 1:
                    
                    mainSettingsPanel.SetActive(true);
                    //go to the main panel
                    break;
                case 2:
                    //addQuestionPanel.SetActive(true);
                    //go to the add question panel
                    break;
                case 3:
                    //incorrectAnswersPanel.SetActive(true);
                    //go to the incorrect answer panel
                    break;
                case 4: 
                    //viewQuestionSetPanel.SetActive(true);
                    //go to the view/edit questions menu
                    break;
                default:
                    //Debug.LogWarning("Incorrect view selection in QuizMakerController");
                    //code should never reach this point
                    break;
            }
        }
    }
}