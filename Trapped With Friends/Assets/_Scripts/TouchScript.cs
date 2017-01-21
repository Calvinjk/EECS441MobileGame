using UnityEngine;
using System.Collections;

public class TouchScript : MonoBehaviour {
    //Initialize
    bool rotating = false;

	void Update () {

	    if (Input.touches[0].position.x < 300 && !rotating) { //Rotate left
            StartCoroutine(RotateCamera(Vector3.down * 90, 1));
        }

        if (Input.touches[0].position.x > 500 && !rotating) { //Rotate right
            StartCoroutine(RotateCamera(Vector3.up * 90, 1));
        }
    }

    void OnGUI() { //Everything in here is currently just to tell if fingers are touching the screen
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
    }

    IEnumerator RotateCamera(Vector3 byAngle, float inTime) {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngle);
        rotating = true;

        for (var t = 0f; t < 1; t += 0.05f / inTime) {
            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }

        var vec = transform.eulerAngles;
        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;
        transform.eulerAngles = vec;

        rotating = false;
    }
}
