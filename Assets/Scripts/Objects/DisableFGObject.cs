using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableFGObject : MonoBehaviour {

    private int defaultLayer;

	// Use this for initialization
	void Start () {
        defaultLayer = gameObject.layer;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Scratch")
            gameObject.layer = 10;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Scratch")
            gameObject.layer = 10;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Scratch")
            gameObject.layer = defaultLayer;
    }
}
