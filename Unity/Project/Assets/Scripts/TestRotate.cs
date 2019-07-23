using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotate : MonoBehaviour {
    private Transform front;
    private Transform flush;
    private Transform handle;

    private float time = 0;
    private float speed = 5f;

    // Start is called before the first frame update
    void Start() {
        front = transform.Find("front");
        flush = transform.Find("flush");
        handle = transform.Find("handle");

        if (front == null || flush == null || handle == null) {
            throw new System.Exception("Missing child");
        }
    }

    // Update is called once per frame
    void Update() {


        if ((time += Time.deltaTime) > .1f) {
            time = 0;

            front.transform.Rotate(0, speed, 0, Space.Self);
            flush.transform.Rotate(0, speed, 0, Space.Self);
            handle.transform.Rotate(speed, 0, 0, Space.Self);
        }
    }

}
