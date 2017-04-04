using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace com.aaronandco.puzzlepotato {
    public class Matching : Puzzle {

        public List<GameObject> cards;
        public List<GameObject> cardTypes; 

        public bool _______________;

        public List<Vector3> cardsPos; 
        public Dictionary<int, GameObject> cardsFront; 
        public int whichCard;

        void Awake() {
            Initialize();
            cardsPos.Add(new Vector3(-195, 75, 1));
            cardsPos.Add(new Vector3(-65, 75, 1));
            cardsPos.Add(new Vector3(65, 75, 1));
            cardsPos.Add(new Vector3(195, 75, 1));
            cardsPos.Add(new Vector3(-195, -75, 1));
            cardsPos.Add(new Vector3(-65, -75, 1));
            cardsPos.Add(new Vector3(65, -75, 1));
            cardsPos.Add(new Vector3(195, -75, 1));
        }

        void Update() {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) {     // Make sure only one finger was used and it is coming off the screen
                Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);    // Get the world coordinates of the screen touch
                Vector2 touchPos = new Vector2(wp.x, wp.y);                                 // We have a 2D game, so turn the 3d coordinates into 2D (we dont care about z)
                Collider2D colInfo = Physics2D.OverlapPoint(touchPos);                      // Determine if this 2D point is within any colliders
                if (colInfo != null) { // If something was touched
                    return; 
                }
            }
        }

        public override void StartGame() {

            // putting cards down
            for (int i = 0; i < 4; ++i) {
                whichCard = Random.Range(0, cards.Count);
                // do { whichCard = Random.Range(0, cards.Count);
                // } while (!cardsFront.ContainsKey(whichCard));

                // cardsFront[whichCard] = Instantiate(cardTypes[i], cardsPos[whichCard], Quaternion.identity);
                // cardsFront[whichCard].SetActive(true);
            }
        }


    }
}
