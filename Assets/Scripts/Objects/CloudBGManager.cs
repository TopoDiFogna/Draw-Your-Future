using UnityEngine;
using System.Collections;
using System;

public class CloudBGManager : MonoBehaviour {

    public GameObject Light;
    public float mintime = 1.5f;
    public float maxtime = 3f;
    bool stop = true;
    SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(Lightning());
	}

    private IEnumerator Lightning()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(1.5f,3f));
            if (!stop)
            {
                sr.enabled = true;
                Light.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Scratch")
        {
            gameObject.layer = 0;
            stop = false;
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Scratch" && stop)
        {
            stop = false;
        }
    }
}
