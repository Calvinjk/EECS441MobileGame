using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace com.aaronandco.puzzlepotato {
    public class GameManager : MonoBehaviour {


        public bool ____________________________;  //Separation between public and "private" variables in the inspector

        public List<string> players = new List<string>();

        void Awake() {
            DontDestroyOnLoad(this);
        }
    }
}