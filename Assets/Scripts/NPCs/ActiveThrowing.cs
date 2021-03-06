﻿using UnityEngine;
using System.Collections;

public class ActiveThrowing : MonoBehaviour {

    public SpriteRenderer sr;
    public ThrowingEnemy script;
    public CircleCollider2D cc;
    bool activated;

    BoxCollider2D bc;

    void Start()
    {
        activated = false;
        bc = gameObject.GetComponent<BoxCollider2D>();
    }

	void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Scratch" && !activated)
        {
            activated = true;
            bc.enabled = false;
            script.enabled = true;
            cc.enabled = true;
            gameObject.GetComponent<ActiveThrowing>().enabled = false;
            gameObject.layer = 0;
            //sr.sortingOrder = 2;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Scratch")
        {
            activated = false;
            bc.enabled = true;
            script.enabled = false;
            cc.enabled = false;
            gameObject.GetComponent<ActiveThrowing>().enabled = true;
            gameObject.layer = 8;
            //sr.sortingOrder = 0;
        }
    }
}
