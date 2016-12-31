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
            Debug.Log(camControl.m_minBounds.ToString());
            Debug.Log(coll.size.y.ToString());
            camControl.m_minBounds -= new Vector2(0, coll.size.y);
        }

    }
}
