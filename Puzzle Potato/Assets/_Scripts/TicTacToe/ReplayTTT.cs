using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.aaronandco.puzzlepotato{
    public class ReplayTTT : MonoBehaviour {
        public float aliveTime = 10f;

        public void Update() {
            aliveTime -= Time.deltaTime;

            if (aliveTime < 0) {
                TicTacToe TTTscript = (TicTacToe)GameObject.Find("TicTacToe(Clone)").GetComponent("TicTacToe");
                TTTscript.StartGame();
            }
        }
    }
}