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
        public InputField playerNameInput;
        public GameObject playerCount;
        public GameObject playerList;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        public List<string> playerNames = new List<string>();
        public GameManager gameManagerScript;

        // Make sure the players only see the title screen at first and find the GameManager script
        void Awake() {
            addPlayersPanel.SetActive(false);
            gameManagerScript = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");
        }

        // Toggles the view between title screen and player select
        public void ToggleTitleView() {
            if (addPlayersPanel.activeInHierarchy == true) {
                addPlayersPanel.SetActive(false);
                titlePanel.SetActive(true);
            } else {
                addPlayersPanel.SetActive(true);
                titlePanel.SetActive(false);
            }
        }

        // Adds a new player to the list, updates on screen information to reflect this
        public void AddPlayerToList() {
            playerNames.Add(playerNameInput.text);
            Text playerListText = playerList.GetComponent<Text>();

            playerCount.GetComponent<Text>().text = "Current Player Count: " + playerNames.Count;
            playerListText.text = "Current Players:";

            for (int i = 0; i < playerNames.Count; ++i) {
                playerListText.text = "\n" + playerListText.text + playerNames[i];
            }

            playerNameInput.text = "";
            playerListText.color = Color.white;
        }

        // Write the current player list to the master GameManager and load the next scene
        // TODO -- Checks for minimum players and such
        public void BeginGame() {
            gameManagerScript.players = playerNames;
            SceneManager.LoadScene(1);
        }
    }
}