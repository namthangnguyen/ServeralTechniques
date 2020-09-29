using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndShoot : MonoBehaviour {

    public GameObject crosshairs;
    private Vector2 target;

    void Start() {
        Cursor.visible = false;
    }

    void Update() {
        target = gameObject.GetComponent<Camera>().ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        crosshairs.transform.position = target;
    }
}
