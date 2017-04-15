using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.aaronandco.puzzlepotato{
    public class ReplayOT : MonoBehaviour {
        public float aliveTime = 10f;

        public void Update() {
            aliveTime -= Time.deltaTime;

            if (aliveTime < 0) {
                OrderedTouch OTscript = (OrderedTouch)GameObject.Find("OrderedTouch(Clone)").GetComponent("OrderedTouch");
                OTscript.StartGame();
            }
        }
    }
}