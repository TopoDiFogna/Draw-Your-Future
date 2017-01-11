using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageChain : MonoBehaviour {

    public GameObject m_crate;
    public GameObject m_chain;

	// Use this for initialization
	void Start () {
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Scratch")
        {
            HingeJoint2D[] joints = m_chain.GetComponents<HingeJoint2D>();
            foreach (HingeJoint2D j in joints)
            {
                if (j.connectedBody == null)
                {
                    j.connectedBody = m_crate.GetComponent<Rigidbody2D>();
                }
            }
        }
    }


}
