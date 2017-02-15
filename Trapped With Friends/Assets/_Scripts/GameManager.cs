using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;


namespace Com.AaronAndCo.TrappedWithFriends {
    public class GameManager : MonoBehaviour {

        #region Public Variables

        [Tooltip("The prefab to use for representing the player")]
        public GameObject playerPrefab;

        [Tooltip("Positions players may spawn at")]
        public List<GameObject> spawnPositions;

        #endregion

        #region Photon Messages

        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public void OnLeftRoom() {
            SceneManager.LoadScene(0);
        }

        #endregion


        #region Public Methods

        public void LeaveRoom() {
            PhotonNetwork.LeaveRoom();
        }


        #endregion

        #region MonoBehavior Callbacks

        void Start() {
            if (playerPrefab == null) {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            } else {
                Debug.Log("We are Instantiating LocalPlayer from " + SceneManager.GetActiveScene().name);

                // Randomly determine spawnpos
                int spawnNum = UnityEngine.Random.Range(0, spawnPositions.Count);

                if (PlayerController.LocalPlayerInstance == null) {
                    Debug.Log("We are Instantiating LocalPlayer from " + SceneManager.GetActiveScene().name);
                    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                    PhotonNetwork.Instantiate(this.playerPrefab.name, spawnPositions[spawnNum].transform.position, spawnPositions[spawnNum].transform.rotation, 0);
                } else {
                    Debug.Log("Ignoring scene load for " + SceneManager.GetActiveScene().name);
                }
            }
            
        }

        #endregion
    }
}