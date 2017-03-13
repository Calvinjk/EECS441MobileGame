using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;
using System.Collections.Generic;

namespace com.aaronandco.puzzlepotato {
	public class LoserController : MonoBehaviour {

		public GameObject loserName;

		GameManager gameManagerScript;

		// Use this for initialization
		void Start () {
			gameManagerScript = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");
			loserName.GetComponent<Text>().text = gameManagerScript.players[gameManagerScript.curPlayer];
		}

		public void ContinueGame() {
			gameManagerScript.curTime = gameManagerScript.curTime;
			SceneManager.LoadScene (1);
		}

		public void NewGame() {
			gameManagerScript.players.Clear();
			SceneManager.LoadScene(0);
		}
	}
}
