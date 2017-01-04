using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBoard : MonoBehaviour {

    private Rigidbody2D rb;
    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("kfdòksf");
        if(collision.tag == "Scratch")
        {
            rb.isKinematic = false;
            sr.sortingOrder = 3;
        }
    }
}
