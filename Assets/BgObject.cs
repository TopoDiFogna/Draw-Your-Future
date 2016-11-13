using UnityEngine;
using System.Collections;

public class BgObject : MonoBehaviour {

    public SpriteRenderer sr;
    public Collider2D cl;

	// Use this for initialization
	void Start () {
        cl.enabled = false;
        sr.enabled = false;
	}
	
    void OnTriggerEnter2D(Collider2D coll)
    {
        print("Triggered" );
        if (coll.gameObject.tag == "Scratch" && (cl.enabled == false && sr.enabled == false))
        {
            print("Enabling");
            cl.enabled = true;
            sr.enabled = true;
            StartCoroutine(TimeToLive(10));
        }
    }

    IEnumerator TimeToLive(int time)
    {
        yield return new WaitForSeconds(time);
        cl.enabled = false;
        sr.enabled = false;
    }
}
