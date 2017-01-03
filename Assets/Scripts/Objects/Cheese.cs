using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour {

    public GameObject m_cheese;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Scratch")
        {
            m_cheese.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Scratch")
        {
            m_cheese.gameObject.SetActive(false);
        }
    }
}
