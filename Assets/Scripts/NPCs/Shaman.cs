using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaman : MonoBehaviour {

    public GameObject[] Skeletons;
    public DoorSpawnEvent dse;
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
            StartCoroutine(SpawnSkeletons(5f));
            dse.Close();
        }
    }

    private IEnumerator SpawnSkeletons(float delay)
    {
        foreach (GameObject g in Skeletons)
        {
            if (!activated)
                break;
            g.SetActive(true);
            yield return new WaitForSeconds(delay);
        }
    }

    public void Activate(bool status)
    {
        activated = status;
        if (!status)
        {
            foreach (GameObject g in Skeletons)
            {
                g.SetActive(false);
            }
        }
    }

}
