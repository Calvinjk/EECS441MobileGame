using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace com.aaronandco.puzzlepotato {
    public class SetupController : MonoBehaviour {
        // All these must be added by hand in the inspector

        public List<GameObject> allPlayers;
        public GameObject playerList;

        public bool ______________________________;

        public GameObject titlePanel;
        public GameObject addPlayersPanel;
        public GameObject startPanel;
        public Text instruction;
        public InputField playerNameInput;
        public GameObject playerCount;
        public GameObject readyButton;
        public GameObject addPlayerButton;
        public GameObject backButton;
        public GameObject playButton;
        public Text beginButtonText;
        public GameObject playerButton;
        public GameManager gameManagerScript;

        public List<string> playerNames = new List<string>();
        // public List<GameObject> allPlayers = new List<GameObject>();

        // Make sure the players only see the title screen at first and find the GameManager script
        void Awake() {
            SetView(0);
            gameManagerScript = (GameManager)GameObject.FindGameObjectWithTag("GameManager").GetComponent("GameManager");

        }

        // Sets which view is active
        // 0 - Title
        // 1 - Add Players
        // 2 - Confirm players/settings
        // 3 - START
        public void SetView (int panel)
        {
            titlePanel.SetActive (false);
            addPlayersPanel.SetActive (false);
            startPanel.SetActive (false);

            playButton.SetActive (false);
            backButton.SetActive (false);
            addPlayerButton.SetActive (false);
            readyButton.SetActive (false);

            switch (panel) {
                case 0:
                    titlePanel.SetActive (true);
        
                    // If we are going back to the title screen, wipe players and reset text
                    playerNames.Clear ();
                    playerCount.GetComponent<Text> ().text = "Number  of  players: 0";
                    // playerList.GetComponent<Text> ().text = "Current  Players:  \n \t None";
                    // playerList.GetComponent<Text> ().color = Color.black;
                    break;
                case 1:
                    addPlayersPanel.SetActive (true);
                    instruction.text = "please  enter  between  2  and  8  players";

                    if (playerNames.Count >= 2) {
                        readyButton.SetActive (true);
                    }
                    addPlayerButton.SetActive (true);
                    
                    break;
                case 2:
                    addPlayersPanel.SetActive(true);
                    instruction.text = "is  this  correct?";
                    instruction.color = Color.black; 

                    playButton.SetActive(true);
                    backButton.SetActive(true);
                    break;
                case 3:
                    startPanel.SetActive(true);

                    gameManagerScript.curPlayer = UnityEngine.Random.Range(0, playerNames.Count - 1);
                    beginButtonText.text = playerNames[gameManagerScript.curPlayer] + ",  please  press  the  button  when  ready!";
                    break;
                default:
                    Debug.LogWarning("Incorrect view selection");
                    break;
            }         
        }

        // Adds a new player to the list, updates on screen information to reflect this
        public void AddPlayerToList () {
            if (playerNameInput.text == "") {
                return;
            }
            if (playerNames.Count >= 1) {
                readyButton.SetActive (true);
            }
            if (playerNames.Count >= 7) {
                addPlayerButton.SetActive (false);
            }

            playerNames.Add(playerNameInput.text);
            Text playerListText = playerList.GetComponent<Text>();

            playerCount.GetComponent<Text>().text = "Number  of  players: " + playerNames.Count;
            playerListText.text = "Current  Players:\n";

            for (int i = 0; i < playerNames.Count; ++i) {
                playerListText.text = playerListText.text + "\t" +playerNames[i] + "\n";
            }

            playerNameInput.text = "";
            playerListText.color = Color.black;

            // playerNames.Add(playerNameInput.text);
            // playerCount.GetComponent<Text>().text = "Number  of  players: " + playerNames.Count;

            // for (int i = 0; i < playerNames.Count; ++i) {
            //     allPlayers[i].GetComponentInChildren<Text>().text = playerNameInput.text;
            //     allPlayers[i].SetActive(true);
            // }

            // playerNameInput.text = "";

        }

        // // Deletes selected user ()
        // public void DeletePlayer() {
        //     // int indexToDelete = Int32.Parse(transform.parent.GetComponent<Text>().text);
        //     // int indexToDelete = Int32.Parse(this.GetComponentInChildren<Text>().text);
        //     Debug.Log(indexToDelete);
        //     allPlayers.RemoveAt(indexToDelete);
        // }

        // Write the current player list to the master GameManager and load the next scene
        public void ProceedToGame() {
            Debug.Log("Starting...");

            gameManagerScript.players = playerNames;
            gameManagerScript.curTime = gameManagerScript.maxTime;
            SceneManager.LoadScene(1);
        }
    }
}