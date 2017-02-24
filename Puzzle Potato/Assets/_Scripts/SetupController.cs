using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;
using System.Collections.Generic;

namespace com.aaronandco.puzzlepotato {
    public class SetupController : MonoBehaviour {
        public GameObject addPlayersPanel;
        public GameObject titlePanel;
        public InputField playerNameInput;
        public GameObject playerCount;
        public GameObject playerList;

        public bool ____________________________;  //Separation between public and "private" variables in the inspector

        public List<string> playerNames = new List<string>();
        public GameManager gameManagerScript;

        void Awake() {
            addPlayersPanel.SetActive(false);
            gameManagerScript = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");
        }

        public void ToggleTitleView() {
            if (addPlayersPanel.activeInHierarchy == true) {
                addPlayersPanel.SetActive(false);
                titlePanel.SetActive(true);
            } else {
                addPlayersPanel.SetActive(true);
                titlePanel.SetActive(false);
            }
        }

        public void AddPlayerToList() {
            playerNames.Add(playerNameInput.text);
            Text playerListText = playerList.GetComponent<Text>();

            playerCount.GetComponent<Text>().text = "Current Player Count: " + playerNames.Count;
            playerListText.text = "Current Players:\n";

            for (int i = 0; i < playerNames.Count; ++i) {
                playerListText.text = playerListText.text + playerNames[i] + "\n";
            }

            playerNameInput.text = "";
            playerListText.color = Color.white;
        }

        public void BeginGame() {
            gameManagerScript.players = playerNames;
            SceneManager.LoadScene(1);
        }
    }
}