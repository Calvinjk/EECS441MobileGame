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
        public GameObject beginButton; 

        public GameManager gameManagerScript;

        public List<string> playerNames = new List<string>();
        public int numPlayers = 0; 

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
                    numPlayers = 0; 
                    playerCount.GetComponent<Text> ().text = "Number  of  players: 0";
                    break;
                case 1:
                    addPlayersPanel.SetActive (true);
                    instruction.text = "please  enter  between  2  and  8  players";

                    // turn on the delete buttons
                    for (int i = 0; i < numPlayers; ++i) {
                        allPlayers[i].transform.GetChild(1).gameObject.SetActive(true);
                    }

                    if (numPlayers >= 2) {
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

                    // deactivate delete buttons
                    for (int i = 0; i < numPlayers; ++i) {
                        allPlayers[i].transform.GetChild(1).gameObject.SetActive(false);
                    }
                    break;
                case 3:
                    startPanel.SetActive(true);

                    gameManagerScript.curPlayer = UnityEngine.Random.Range(0, numPlayers - 1);

                    beginButton.transform.GetChild(1).gameObject.GetComponent<Text>().text = playerNames[gameManagerScript.curPlayer] + ",  please  press  when  ready!";
                    break;
                default:
                    Debug.LogWarning("Incorrect view selection");
                    break;
            }         
        }

        // Adds a new player to the list, updates on screen information to reflect this
        public void AddPlayerToList () {
            instruction.text = "please  enter  between  2  and  8  players";
            if (playerNameInput.text == "") {
            	instruction.text = "please  enter  a  player  name";
                return;
            }
            if (numPlayers >= 1) {
                readyButton.SetActive (true);
            }
            if (numPlayers >= 7) {
                addPlayerButton.SetActive (false);
            }
            
            playerNames.Add(playerNameInput.text);
            numPlayers += 1; 
            playerCount.GetComponent<Text>().text = "Number  of  players: " + numPlayers;

            allPlayers[numPlayers - 1].GetComponentInChildren<Text>().text = playerNameInput.text;
            allPlayers[numPlayers - 1].SetActive(true);

            playerNameInput.text = "";

        }

        // Deletes selected user
        public void DeletePlayer(GameObject player) {
            Debug.Log("deleting: " + player.GetComponentInChildren<Text>().text);
            player.SetActive(false);

            // Actually delete them out of the array passed to GameManager
            Debug.Log("Actually deleting: " + player.GetComponentInChildren<Text>().text);
            playerNames.Remove(player.GetComponentInChildren<Text>().text);

            //re ordering players. this sucks lol.
            for (int i = 0; i < numPlayers; ++i) {
                if (allPlayers[i].activeInHierarchy) {
                    Debug.Log(allPlayers[i].GetComponentInChildren<Text>().text);
                    continue;
                }

                //ok this player got deleted, so move them all up
                for (int j = i; j < numPlayers - 1; ++j) {
                    allPlayers[j].GetComponentInChildren<Text>().text = allPlayers[j + 1].GetComponentInChildren<Text>().text;
                }
                break; 
            }
            player.SetActive(true);
            numPlayers -= 1; 
            allPlayers[numPlayers].SetActive(false);

            playerCount.GetComponent<Text>().text = "Number  of  players: " + numPlayers;
            if (numPlayers < 2) {
                readyButton.SetActive(false);
            }      
        }

        // Write the current player list to the master GameManager and load the next scene
        public void ProceedToGame() {
            Debug.Log("PlayerList:");
            for (int i = 0; i < playerNames.Count; ++i) {
                Debug.Log(playerNames[i]);
            }
            Debug.Log("Starting...");

            gameManagerScript.players = playerNames;
            gameManagerScript.curTime = gameManagerScript.maxTime;
            SceneManager.LoadScene(1);
        }
    }
}