using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearFlipper : MonoBehaviour {

    Transform tr;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        tr = transform;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (rb.velocity.x >= 0)
            tr.localScale = new Vector3(0.5f,0.5f,1);
        else
            tr.localScale = new Vector3(-0.5f, 0.5f, 1);
    }
}
