using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaman : MonoBehaviour {

    public GameObject Spawner;
    public GameObject[] Skeletons;
    public DoorSpawnEvent dse;
    public GameObject Summon;
    bool activated;

	// Use this for initialization
	void Start () {
        activated = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            Activate(true);
            //GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(SpawnSkeletons(5f));
            dse.Close();
        }
    }

    IEnumerator Summ()
    {
        Summon.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Summon.SetActive(false);
    }

    private IEnumerator SpawnSkeletons(float delay)
    {
        foreach (GameObject g in Skeletons)
        {
            if (!activated)
                break;
            StartCoroutine(Summ());
            g.SetActive(true);
            g.transform.position = Spawner.transform.position;
            yield return new WaitForSeconds(delay);
        }
    }

    public void Activate(bool status)
    {
        activated = status;
        if (!status)
        {
            StopCoroutine("SpawnSkeletons");
            foreach (GameObject g in Skeletons)
            {
                g.SetActive(false);
            }
        }
    }

}
