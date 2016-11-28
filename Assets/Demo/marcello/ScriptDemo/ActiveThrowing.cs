using UnityEngine;
using System.Collections;

public class ActiveThrowing : MonoBehaviour {

    public SpriteRenderer sr;
    public ThrowingEnemy script;
    public CircleCollider2D cc;

    BoxCollider2D bc;

    void Start()
    {
        bc = gameObject.GetComponent<BoxCollider2D>();
    }

	void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Scratch")
        {
            bc.enabled = false;
            script.enabled = true;
            cc.enabled = true;
            gameObject.GetComponent<ActiveThrowing>().enabled = false;
            sr.sortingOrder = 2;
        }
    }
}
