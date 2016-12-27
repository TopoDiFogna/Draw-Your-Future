using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoyoSpawner : MonoBehaviour {

    bool activated = false;
    public GameObject m_spawned_object;

    private void Start()
    {
        m_spawned_object = Instantiate(m_spawned_object);
        m_spawned_object.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch" && !activated)
        {
            activated = true;
            m_spawned_object.transform.position = transform.position;
            m_spawned_object.SetActive(true);
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch" && !activated)
        {
            activated = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch")
        {
            activated = false;
        }
    }
}
