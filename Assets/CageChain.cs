using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageChain : MonoBehaviour {

    private Rigidbody2D rb;
    public GameObject chain;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Scratch")
        {
            HingeJoint2D[] joints = chain.GetComponents<HingeJoint2D>();
            foreach(HingeJoint2D j in joints)
            {
                if(j.connectedBody == null)
                    j.connectedBody = rb;
            }
            rb.isKinematic = false;

        }
    }
}
