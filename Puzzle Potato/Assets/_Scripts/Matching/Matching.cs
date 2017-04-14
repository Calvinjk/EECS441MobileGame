using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

namespace com.aaronandco.puzzlepotato {
    public class Matching : Puzzle {

        public List<GameObject> cardTypes; 

        public bool _______________;

        public List<Vector3> cardsPos;
        public List<Vector3> backsPos;
        public Dictionary<int, GameObject> cardsFront; 
        public Dictionary<int, GameObject> cardsBack; 
        public List<GameObject> faceUp; 
        public int whichCard;
        public int matches; 
        public bool pause; 

        void Awake() {
            Initialize();
            cardsPos = new List<Vector3>() {
                new Vector3(-4.5f, 1.75f, 1f),
                new Vector3(-1.5f, 1.75f, 1f),
                new Vector3(1.5f, 1.75f, 1f),
                new Vector3(4.5f, 1.75f, 1f),
                new Vector3(-4.5f, -1.75f, 1f),
                new Vector3(-1.5f, -1.75f, 1f),
                new Vector3(1.5f, -1.75f, 1f),
                new Vector3(4.5f, -1.75f, 1f)
            };

            backsPos = new List<Vector3>() {
                new Vector3(-4.5f, 1.75f, 0f),
                new Vector3(-1.5f, 1.75f, 0f),
                new Vector3(1.5f, 1.75f, 0f),
                new Vector3(4.5f, 1.75f, 0f),
                new Vector3(-4.5f, -1.75f, 0f),
                new Vector3(-1.5f, -1.75f, 0f),
                new Vector3(1.5f, -1.75f, 0f),
                new Vector3(4.5f, -1.75f, 0f)
            };

            cardsFront = new Dictionary<int, GameObject>();
            cardsBack = new Dictionary<int, GameObject>();
            faceUp = new List<GameObject>();
            whichCard = 0; 
            matches = 0; 
            pause = false; 
        }

        void Update() {
            // check if you won
            if (matches == 4 && !pause) {
                GameCompleted();
            }

            // check for a match
            if (faceUp.Count == 2 && !pause) {
                string type1 = faceUp[0].GetComponent<Text>().text;
                string type2 = faceUp[1].GetComponent<Text>().text;

                // you got a match!
                if (type1 == type2) {
                    StartCoroutine("GotMatch");
                }
                // you didnt, so flip the cards back over
                else {
                    Debug.Log("StartCoroutine");
                    StartCoroutine("ShowCard");
                    Debug.Log("EndCoroutine");
                }
            }

            // card was touched!
            //if (Input.GetMouseButtonDown(0) && !pause) {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended && !pause) {     
                //Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);   
                Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);   
                Vector2 touchPos = new Vector2(wp.x, wp.y);                                 
                Collider2D colInfo = Physics2D.OverlapPoint(touchPos);                      
                if (colInfo != null) {
                    if (colInfo.gameObject.activeSelf) {
                        pause = true;
                        colInfo.gameObject.SetActive(false);
                        faceUp.Add(colInfo.gameObject);
                        Debug.Log("Added card to faceUp: " + colInfo.gameObject.GetComponent<Text>().text);
                        pause = false; 
                    }
                }
            }
        }

        IEnumerator ShowCard() {
            pause = true;
            yield return new WaitForSeconds(.5f);
            faceUp[0].SetActive(true);
            faceUp[1].SetActive(true);
            faceUp.Clear();
            Debug.Log("cleared faceUp.");
            pause = false; 
        }

        IEnumerator GotMatch() {
            pause = true;
            yield return new WaitForSeconds(.5f);
            matches++;
            faceUp.Clear();
            Debug.Log("cleared faceUp.");
            pause = false;
        }

        public override void StartGame() {

            // putting cards down
            for (int j = 0; j < 2; ++j) {
                for (int i = 0; i < 4; ++i) {
                    while (cardsFront.ContainsKey(whichCard)) { whichCard = Random.Range(0, 8); }
                    cardsBack[whichCard] = Instantiate(cardTypes[4], backsPos[whichCard], Quaternion.identity);
                    cardsBack[whichCard].GetComponent<Text>().text = i.ToString();
                    cardsFront[whichCard] = Instantiate(cardTypes[i], cardsPos[whichCard], Quaternion.identity);
                }
            }
        }
    }
}
