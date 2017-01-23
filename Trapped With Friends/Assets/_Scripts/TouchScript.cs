using UnityEngine;
using System.Collections;

public class TouchScript : MonoBehaviour {
    //Initialize
    public float cameraRoatationSpeed = 20;
    public bool ____________________________;
    bool rotating = false;

	void Update () {

    }

    void OnGUI() {
        // Used for development to see if fingers are touching the screem
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

        // Rotate camera if GUI buttons are pressed
        if (GUI.Button(new Rect(0, 0, Screen.width / 4, Screen.height), "") && !rotating) {
            StartCoroutine(RotateCamera(Vector3.down * 90, cameraRoatationSpeed));
        }
        if (GUI.Button(new Rect(Screen.width / 4 * 3, 0, Screen.width / 4, Screen.height), "") && !rotating) {
            StartCoroutine(RotateCamera(Vector3.up * 90, cameraRoatationSpeed));
        }
    }

    // Coroutine for rotating the camera given an angle to rotate and time to do it in
    IEnumerator RotateCamera(Vector3 byAngle, float inTime) {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngle);
        rotating = true;

        // Actual work for the coroutine is done here in moving the camera
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
