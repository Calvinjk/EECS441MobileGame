using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace com.aaronandco.puzzlepotato {
    public class Matching : Puzzle {

        public List<GameObject> cardTypes; 

        public bool _______________;

        private Dictionary<int, Vector3> cardsPos; 
        private Dictionary<int, GameObject> cardsFront; 
        private Dictionary<int, GameObject> cardsBack; 
        private int whichCard;
        private Vector3 vector; 

        void Awake() {
            Initialize();
            cardsPos = new Dictionary<int, Vector3>();
            cardsFront = new Dictionary<int, GameObject>();
            cardsBack = new Dictionary<int, GameObject>();

            cardsPos.Add(0, new Vector3(-5.1f, 1.75f, 1));
            cardsPos.Add(1, new Vector3(-1.6f, 1.75f, 1));
            cardsPos.Add(2, new Vector3(1.6f, 1.75f, 1));
            cardsPos.Add(3, new Vector3(5.1f, 1.75f, 1));
            cardsPos.Add(4, new Vector3(-5.1f, -1.75f, 1));
            cardsPos.Add(5, new Vector3(-1.6f, -1.75f, 1));
            cardsPos.Add(6, new Vector3(1.6f, -1.75f, 1));
            cardsPos.Add(7, new Vector3(5.1f, -1.75f, 1));
        }

        void Update() {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) {     // Make sure only one finger was used and it is coming off the screen
                Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);    // Get the world coordinates of the screen touch
                Vector2 touchPos = new Vector2(wp.x, wp.y);                                 // We have a 2D game, so turn the 3d coordinates into 2D (we dont care about z)
                Collider2D colInfo = Physics2D.OverlapPoint(touchPos);                      // Determine if this 2D point is within any colliders
                if (colInfo != null) {
                    Debug.Log("card touched");
                    return; 
                }
            }
        }

        public override void StartGame() {

            // putting cards down
            for (int i = 0; i < 4; ++i) {
                for (int j = 0; j < 2; ++j) {
                    whichCard = Random.Range(0, 8);
                    while (cardsFront.ContainsKey(whichCard)) {
                        whichCard = Random.Range(0, 8);                    
                    }
                    cardsBack.Add(whichCard, Instantiate(cardTypes[4], cardsPos[whichCard], Quaternion.identity));
                    cardsFront.Add(whichCard, Instantiate(cardTypes[i], cardsPos[whichCard], Quaternion.identity));
                    cardsBack[whichCard].SetActive(true);
                    cardsFront[whichCard].SetActive(true);
                }
            }

        }


    }
}
