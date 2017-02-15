using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace Com.AaronAndCo.TrappedWithFriends {
    public class PlayerController : Photon.MonoBehaviour {
        //Initialize
        public float roatationSpeed = 20;
        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;
        public bool ____________________________;
        bool rotating = false;

        void Awake() {
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.isMine) {
                PlayerController.LocalPlayerInstance = this.gameObject;
            }
            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(this.gameObject);
        }

        // TODO -- Do not use OnGUI for mobile devices.  Find another way to do it.
        void OnGUI() {
            // Make sure only my player character processes my input
            if (photonView.isMine == false && PhotonNetwork.connected == true) { return; }

            // Used for development to see if fingers are touching the screem
            /*
            foreach(Touch touch in Input.touches) {
                string message = "";
                message += "ID: " + touch.fingerId + "\n";
                message += "Phase: " + touch.phase.ToString() + "\n";
                message += "TapCount: " + touch.tapCount + "\n";
                message += "Pos X: " + touch.position.x + "\n";
                message += "Pos Y: " + touch.position.y + "\n";

                int num = touch.fingerId;
                GUI.Label(new Rect(0 + 130 * num, 0, 120, 100), message);
            }
            */

            // Style the buttons
            GUIStyle rotationButton = new GUIStyle();
            rotationButton.fontSize = 50;
            rotationButton.alignment = TextAnchor.MiddleCenter;

            // Rotate player if GUI buttons are pressed
            if (GUI.RepeatButton(new Rect(0, 0, Screen.width / 4, Screen.height), "<", rotationButton) && !rotating) { // Rotate Left
                StartCoroutine(RotatePlayer(Vector3.down * 90, roatationSpeed));
            }
            if (GUI.RepeatButton(new Rect(Screen.width / 4 * 3, 0, Screen.width / 4, Screen.height), ">", rotationButton) && !rotating) { // Rotate Right
                StartCoroutine(RotatePlayer(Vector3.up * 90, roatationSpeed));
            }
        }

        // Coroutine for rotating the player given an angle to rotate and time to do it in
        IEnumerator RotatePlayer(Vector3 byAngle, float inTime) {
            var fromAngle = transform.rotation;
            var toAngle = Quaternion.Euler(transform.eulerAngles + byAngle);
            rotating = true;

            // Actual work for the coroutine is done here in moving the camera
            // NOTE -- Small inTime numbers may look bad due to snapping of camera to nearest 90 and low number of loops
            for (var t = 0f; t < 1; t += 1f / inTime) {
                transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
                yield return null;
            }

            // Once rotation is "done", make sure it snaps to the nearest 90 degrees
            var vec = transform.eulerAngles;
            vec.x = Mathf.Round(vec.x / 90) * 90;
            vec.y = Mathf.Round(vec.y / 90) * 90;
            vec.z = Mathf.Round(vec.z / 90) * 90;
            transform.eulerAngles = vec;

            rotating = false;
        }
    }
}