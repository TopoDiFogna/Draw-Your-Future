using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

    public GameObject spawnPosition;
    Transform tr;

    void Start()
    {
        tr = spawnPosition.transform;
    }

	// Use this for initialization
	
	void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            coll.gameObject.GetComponent<PlayerController>().checkPointPosition = tr.position;
        }
    }
}
