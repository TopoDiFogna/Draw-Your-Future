using UnityEngine;
using System.Collections;

public class FlyingBGActive : MonoBehaviour {

    public GameObject[] ToActivate;
    bool active = false;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Scratch" && !active)
        {
            active = true;
            GetComponent<SpriteRenderer>().enabled = false;
            foreach(GameObject g in ToActivate)
            {
                g.SetActive(true);
            }
        }
    }

}
