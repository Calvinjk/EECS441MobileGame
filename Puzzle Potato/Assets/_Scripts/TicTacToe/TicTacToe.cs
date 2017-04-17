using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.aaronandco.puzzlepotato {
    public class TicTacToe : Puzzle {

        public GameObject boardPrefab;
        public GameObject popupPrefab;
        public Sprite xSprite;
        public Sprite oSprite;
        public bool debugLogs = true;

        public bool ____________________________;  // Separation between public and "private" variables in the inspector

        GameObject board;
        GameObject losePopup;
        /// 2d array that shows the gamestate of the board. 
        /// -1 mean unoccupied, 0 means controlled by O, 1 means controlled by X
        public int[,] boardStatus = new int[3, 3] { 
            { -1, -1, -1 }, 
            { -1, -1, -1 }, 
            { -1, -1, -1 }
        };
        public bool aiMove = false;
        public bool pause = false;

        void Start() {
            Initialize();
        }

        public override void StartGame() {
            // Create the UI for the game and reset all variables
            if (board != null) { Destroy(board); }
            if (losePopup != null) { Destroy(losePopup); }
            aiMove = false;
            pause = false;
            board = Instantiate(boardPrefab) as GameObject;
            boardStatus = new int[3,3] {
                { -1, -1, -1 },
                { -1, -1, -1 },
                { -1, -1, -1 }
            };
        }

        void FixedUpdate() {

            if (debugLogs) { Debug.Log("UPDATING TTTT"); }

            if (!pause) {
                // if (Input.GetMouseButtonDown(0)) { // If user is touching the screen
                if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) {     // Make sure only one finger was used and it is coming off the screen
                    if (debugLogs) { Debug.Log("Finger attempted to touch something"); }
                    // Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);    // Get the world coordinates of the screen touch
                    Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);    // Get the world coordinates of the screen touch
                    Vector2 touchPos = new Vector2(wp.x, wp.y);                                 // We have a 2D game, so turn the 3D coordinates into 2D (we dont care about z)
                    Collider2D colInfo = Physics2D.OverlapPoint(touchPos);                      // Determine if this 2D point is within any colliders
                    if (colInfo != null) { // If something was touched
                        string gridSpaceName = colInfo.gameObject.name;
                        if (debugLogs) { Debug.Log("Touched: " + gridSpaceName); }
                        if (boardStatus[(int)gridSpaceName[0] - 48, (int)gridSpaceName[1] - 48] == -1) { // Need to subtract 48 because of ASCII values
                            boardStatus[(int)gridSpaceName[0] - 48, (int)gridSpaceName[1] - 48] = 1;
                            colInfo.gameObject.GetComponent<SpriteRenderer>().sprite = xSprite;
                            colInfo.transform.localScale = new Vector3(.1f, .1f, .1f);
                            // Check for a full board
                            int placed = 0;
                            for (int i = 0; i < 3; ++i) {
                                for (int j = 0; j < 3; ++j) {
                                    if (boardStatus[i, j] != -1) {
                                        ++placed;
                                    }
                                }
                            }
                            if (placed != 9) { aiMove = true; }
                        }
                        CheckWin(1);
                    }
                }

                if (aiMove) {
                    StartCoroutine("AIMove");
                }             
            } 
        }

        IEnumerator AIMove() {
            pause = true;
            yield return new WaitForSeconds(.7f);
            int row = Random.Range(0, 3);
            int col = Random.Range(0, 3);
            // Make a more efficent system later with "remembering" -- TODO
            while (boardStatus[row, col] != -1) {
                row = Random.Range(0, 3);
                col = Random.Range(0, 3);
                if (debugLogs) { Debug.Log("Trying to place"); }
            }
            if (debugLogs) { Debug.Log("Placed at [" + row + ", " + col + "]"); }

            GameObject target = GameObject.Find(row.ToString() + col.ToString());
            boardStatus[row, col] = 0;
            target.GetComponent<SpriteRenderer>().sprite = oSprite;
            target.transform.localScale = new Vector3(.1f, .1f, .1f);
            aiMove = false;
            CheckWin(0);
            pause = false; 
        }

        void CheckWin(int player) {
            // Check rows
            for (int i = 0; i < 3; ++i) {
                if (boardStatus[i, 0] == player && boardStatus[i, 1] == player && boardStatus[i, 2] == player) {
                    if (player == 1) { GameCompleted(); return; } else { GameFailed(); return; }
                }
            }

            // Check columns
            for (int i = 0; i < 3; ++i) {
                if (boardStatus[0, i] == player && boardStatus[1, i] == player && boardStatus[2, i] == player) {
                    if (player == 1) { GameCompleted(); return; } else { GameFailed(); return; }
                }
            }

            // Check diagonals
            if (boardStatus[0, 0] == player && boardStatus[1, 1] == player && boardStatus[2, 2] == player ||
                boardStatus[0, 2] == player && boardStatus[1, 1] == player && boardStatus[2, 0] == player) {
                if (player == 1) { GameCompleted(); return; } else { GameFailed(); return; }
            }

            // Check Tie
            int placed = 0;
            for (int i = 0; i < 3; ++i) {
                for (int j = 0; j < 3; ++j) {
                    if (boardStatus[i, j] != -1) {
                        ++placed;
                    }
                }
            }
            if (placed == 9) { GameFailed(); return; }
        }

        void GameFailed() {
            // Notify player and print a popup
            if (losePopup != null) { Destroy(losePopup); }
            losePopup = Instantiate(popupPrefab, GameObject.Find("Canvas").transform, false);
            pause = true;
        }
    }
}