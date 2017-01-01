using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUnderWater : MonoBehaviour {

    BoxCollider2D coll;
    public float min_bound_delta_y = 10.8f;
    public float max_bound_delta_y = 10.8f;
    bool activated = false;


	// Use this for initialization
	void Start () {
        coll = GetComponent<BoxCollider2D>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.tag == "Ostrich" || collision.tag == "Player") && !activated)
        {
            CameraController camControl = Camera.main.GetComponent<CameraController>();
            camControl.M_minBounds = camControl.M_minBounds - new Vector2(0, min_bound_delta_y);
            camControl.M_maxBounds = camControl.M_maxBounds - new Vector2(0, max_bound_delta_y);
            activated = true;
        }

    }
}
