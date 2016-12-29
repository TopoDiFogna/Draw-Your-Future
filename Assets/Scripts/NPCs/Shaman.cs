using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaman : MonoBehaviour {

    public GameObject[] Skeletons; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            StartCoroutine(SpawnSkeletons(5f));
        }
    }

    private IEnumerator SpawnSkeletons(float delay)
    {
        foreach (GameObject g in Skeletons)
        {
            g.SetActive(true);
            yield return new WaitForSeconds(delay);
        }
    }
}
