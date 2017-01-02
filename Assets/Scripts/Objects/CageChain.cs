using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageChain : MonoBehaviour {

    private Rigidbody2D rb;
    public GameObject chain;
    private float original_mass;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        original_mass = rb.mass;
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
            StartCoroutine(RestoreStatus());
        }
    }

    IEnumerator RestoreStatus()
    {
        yield return new WaitForSeconds(10);
        rb.mass = 10;
        yield return new WaitForSeconds(10);
        rb.isKinematic = true;
        rb.velocity = new Vector3();
        rb.angularVelocity = 0;
        transform.rotation = Quaternion.identity;
        rb.mass = original_mass;
    }
}
