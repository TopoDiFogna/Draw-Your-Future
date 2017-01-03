using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateChild : MonoBehaviour {

    public GameObject m_object;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Scratch")
        {
            m_object.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Scratch")
        {
            m_object.gameObject.SetActive(false);
        }
    }
}
