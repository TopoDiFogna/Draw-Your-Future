using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUnderWater : MonoBehaviour {

    BoxCollider2D coll;


	// Use this for initialization
	void Start () {
        coll = GetComponent<BoxCollider2D>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ostrich")
        {
            CameraController camControl = Camera.main.GetComponent<CameraController>();
            camControl.M_minBounds = camControl.M_minBounds - new Vector2(0, 5.4f);
            camControl.M_maxBounds = camControl.M_maxBounds - new Vector2(0, 5.4f);
        }

    }
}
