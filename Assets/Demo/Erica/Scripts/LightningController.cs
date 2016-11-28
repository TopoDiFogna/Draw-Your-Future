using UnityEngine;
using System.Collections;

public class LightningController : MonoBehaviour {

    SpriteRenderer rend;
    Collider2D collider;
    bool visible = false;
    public float timeVisible;
    public float timeNotVisible;

	// Use this for initialization
	void Start () {
        rend = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!visible)
        {
            StartCoroutine("Lightning");
        }
	}

    IEnumerator Lightning()
    {
        visible = true;
        rend.enabled = true;
        collider.enabled = true;
        yield return new WaitForSeconds(timeVisible);
        rend.enabled = false;
        collider.enabled = false;
        yield return new WaitForSeconds(timeNotVisible);
        visible = false;
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        //TODO PLAYER DIES
    }
}
