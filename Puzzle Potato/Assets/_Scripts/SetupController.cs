using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;
using System.Collections.Generic;

namespace com.aaronandco.puzzlepotato {
    public class SetupController : MonoBehaviour {
        // All these must be added by hand in the inspector
        public GameObject addPlayersPanel;
        public GameObject titlePanel;
        public GameObject getReadyPanel;
        public InputField playerNameInput;
        public GameObject playerCount;
        public GameObject playerList;
        public Text beginButtonText;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        public List<string> playerNames = new List<string>();
        public GameManager gameManagerScript;

        // Make sure the players only see the title screen at first and find the GameManager script
        void Awake() {
            SetView(0);
            gameManagerScript = (GameManager)GameObject.FindGameObjectWithTag("GameManager").GetComponent("GameManager");
        }

        // Sets which view is active
        // 0 - Title
        // 1 - Add Players
        // 2 - Get Ready
        public void SetView(int panel) {
            switch (panel) {
                case 0:
                    titlePanel.SetActive(true);
                    addPlayersPanel.SetActive(false);
                    getReadyPanel.SetActive(false);

                    // If we are going back to the title screen, wipe players and reset text
                    playerNames.Clear();
					playerCount.GetComponent<Text>().text = "Number  of  players: 0";
					playerList.GetComponent<Text>().text = "Current  Players:  None";
                    playerList.GetComponent<Text>().color = Color.black;
                    break;
                case 1:
                    titlePanel.SetActive(false);
                    addPlayersPanel.SetActive(true);
                    getReadyPanel.SetActive(false);
                    break;
                case 2:
                    titlePanel.SetActive(false);
                    addPlayersPanel.SetActive(false);
                    getReadyPanel.SetActive(true);

                    gameManagerScript.curPlayer = Random.Range(0, playerNames.Count - 1);
                    beginButtonText.text = playerNames[gameManagerScript.curPlayer] + ",  please  press  the  button  when  ready!";
                    break;
                default:
                    Debug.LogWarning("Incorrect view selection");
                    break;
            }         
        }

        // Adds a new player to the list, updates on screen information to reflect this
        public void AddPlayerToList() {
            playerNames.Add(playerNameInput.text);
            Text playerListText = playerList.GetComponent<Text>();

            playerCount.GetComponent<Text>().text = "Number  of  players: " + playerNames.Count;
            playerListText.text = "Current  Players:\n";

            for (int i = 0; i < playerNames.Count; ++i) {
                playerListText.text = playerListText.text + "\t" +playerNames[i] + "\n";
            }

            playerNameInput.text = "";
            playerListText.color = Color.black;
        }

        // Write the current player list to the master GameManager and load the next scene
        // TODO -- Checks for minimum players and such
        public void ProceedToGame() {
            gameManagerScript.players = playerNames;
            gameManagerScript.curTime = gameManagerScript.maxTime;
            SceneManager.LoadScene(1);
        }
    }
}