using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

    private Vector3 spawnPosition;


    void Start()
    {
        spawnPosition = gameObject.GetComponentInChildren<Transform>().position;
    }
	
	void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            coll.gameObject.GetComponent<PlayerController>().CheckPointPosition = spawnPosition;
        }
    }
}
