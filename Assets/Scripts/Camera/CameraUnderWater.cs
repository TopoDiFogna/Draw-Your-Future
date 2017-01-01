using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUnderWater : MonoBehaviour {

    BoxCollider2D coll;
    public GameObject wallToActivate;
    bool activated = false;


	// Use this for initialization
	void Start () {
        coll = GetComponent<BoxCollider2D>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ostrich" && !activated)
        {
            CameraController camControl = Camera.main.GetComponent<CameraController>();
            camControl.M_minBounds = camControl.M_minBounds - new Vector2(0, 10.8f);
            camControl.M_maxBounds = camControl.M_maxBounds - new Vector2(0, 10.8f);
            wallToActivate.SetActive(true);
            activated = true;
        }

    }
}
