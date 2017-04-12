using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.aaronandco.puzzlepotato {
    public class TiltMaze : Puzzle {

        public GameObject ballPrefab;
        public GameObject boundariesPrefab;
        public GameObject coinPrefab;
        public int minCoins = 5;
        public int maxCoins = 10;
        public bool debugLogs = true;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        float hBound = 7f;
        float vBound = 2.5f;
        int numCoins;
        public List<GameObject> coins;

        void Start() {
            //Initialize();

            // Set up the ball and map
            Instantiate(ballPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            Instantiate(boundariesPrefab);

            // Set up the coins
            numCoins = Random.Range(minCoins, maxCoins + 1);
            if (debugLogs) { Debug.Log("numCoins: " + numCoins); }
            for (int i = 0; i < numCoins; ++i) {
                Vector3 spawnPos = new Vector3(Random.Range(-hBound, hBound), Random.Range(-vBound, vBound), 0);
                if (debugLogs) { Debug.Log("Placing coin at: " + spawnPos); }
                coins.Add(Instantiate(coinPrefab, spawnPos, Quaternion.identity));
            }
        }

        public override void StartGame() {
            // TODO -- Move all awake and start shit here.
        }
    }
}