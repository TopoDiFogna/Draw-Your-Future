using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour {

    bool active = false;
    public int default_layer = 0;
    public GameObject fishChild;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Scratch" && !active)
        {
            active = true;
            fishChild.layer = default_layer;
            fishChild.GetComponent<SpriteRenderer>().sortingOrder = 1;
            fishChild.GetComponent<SpiderController>().enabled = true;
        }
    }
}
