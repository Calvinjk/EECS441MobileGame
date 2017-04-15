using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.aaronandco.puzzlepotato {
    public class Darts : Puzzle {

        public GameObject dartPrefab;
        public GameObject boardPrefab;
        public bool ____________________________;
        public bool waiting;
        public Vector2 dP;
        public float dT;

        // Use this for initialization
        void Awake() {
            Initialize();
        }

        // Update is called once per frame
        void Update() {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began && waiting)
            {
                dP = Input.GetTouch(0).position;
                dT = Time.time;
            }
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended && waiting)
            {
                waiting = false;
                dP = Input.GetTouch(0).position - dP;
                dT = Time.time - dT;
                Debug.Log("dP: " + dP + ",dt: " + dT);
            }
        }

        public override void StartGame() {
            GameObject dartInstance = Instantiate(dartPrefab, new Vector3(5f,0f,0f), Quaternion.identity) as GameObject;
            GameObject boardInstance = boardPrefab;
            Quaternion boardRotation = Quaternion.Euler(0f, 10f, 90f);
            //boardInstance.transform.localScale = new Vector3(3f, 0.1f, 1f);
            Instantiate(boardPrefab, new Vector3(-5f, 0f, 0f), boardRotation);
            waiting = true;
        }

        public void Hit()
        {
            GameCompleted();
        }
    }
}
