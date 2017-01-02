using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sun_manager : MonoBehaviour {

    public BossFightShaman shaman;
    public bool activ;

	// Use this for initialization
	void Start () {
        activ = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Scratch" && !activ)
        {
            shaman.AddSun();
            activ = true;
            GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
    }

}
