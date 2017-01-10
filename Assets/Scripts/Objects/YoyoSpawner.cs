using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoyoSpawner : MonoBehaviour {

    public GameObject LR_Start = null;
    public LineRenderer lr = null;
    bool activated = false;
    public GameObject m_spawned_object;

    private void Start()
    {

        m_spawned_object = Instantiate(m_spawned_object);
        m_spawned_object.SetActive(false);
        if(m_spawned_object.GetComponent<Yoyo>() != null)
        {
            lr.SetPosition(0, LR_Start.transform.position);
            lr.SetPosition(1, LR_Start.transform.position);
            lr.enabled = false;
            m_spawned_object.GetComponent<Yoyo>().flamingo_animator = GameObject.FindGameObjectWithTag("Flamingo").GetComponent<Animator>();
            m_spawned_object.GetComponent<Yoyo>().lr = lr;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch" && !activated)
        {
            activated = true;
            m_spawned_object.transform.position = transform.position;
            m_spawned_object.SetActive(true);
            lr.enabled = true;
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
