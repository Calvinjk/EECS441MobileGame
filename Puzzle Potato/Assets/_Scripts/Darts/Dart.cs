using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.aaronandco.puzzlepotato
{
    public class Dart : MonoBehaviour
    {
        public float gravity = -10f;
        public float forwardVel = 2f;
        public float sideModifier = 5f;
        public float forwardModifier = 0.005f;
        public bool ____________________________;
        Darts dartsScript;
        Vector3 speed;
        Vector3 acceleration;
        bool speedAccelSet;

        // Use this for initialization
        void Start()
        {
            dartsScript = (Darts)GameObject.Find("Darts(Clone)").GetComponent("Darts");
            speedAccelSet = false;
        }

        void OnTriggerEnter(Collider collider)
        {
            //Debug.Log("Hit!");
            dartsScript.Hit();
            speed = new Vector3(0f, 0f, 0f);
            acceleration = new Vector3(0f, 0f, 0f);
        }

        void FixedUpdate()
        {
            if (!dartsScript.waiting)
            {
                if (!speedAccelSet)
                {
                    speed = new Vector3(-forwardVel, dartsScript.dP.y * sideModifier, forwardModifier * dartsScript.dP.x / dartsScript.dT);
                    acceleration = new Vector3(0, 0, gravity);
                    //Debug.Log("speed: " + speed);
                    transform.rotation = Quaternion.Euler(0f, 0f, -Mathf.Rad2Deg*Mathf.Atan(dartsScript.dP.y * sideModifier) / forwardVel);
                    speedAccelSet = true;
                }

                speed = speed + acceleration * Time.deltaTime;
                transform.position = transform.position + speed * Time.deltaTime;
                transform.localScale = new Vector3(1f, 1f, 1f) * (1f + (transform.position.z) * -0.25f);
                if(transform.localScale.x < 0)
                {
                    //Debug.Log("1.position: " + transform.position);
                    transform.localScale = new Vector3(0f, 0f, 0f);
                }
                if(transform.position.x < -10 || transform.position.y > 10 || transform.position.y < -10 || transform.position.z > 5)
                {
                    //Debug.Log("2.position: " + transform.position);
                    speedAccelSet = false;
                    dartsScript.StartGame();
                    this.gameObject.SetActive(false);
                }
                //Debug.Log("speed: " + speed);
                //Debug.Log("position: " + transform.position);
                //Debug.Log("scale: " + transform.localScale);
            }
        }
    }
}
