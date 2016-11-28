using UnityEngine;
using System.Collections;

public class FlyingMovement : MonoBehaviour {

    public GameObject Target;
    public GameObject carry;
    public bool seek = false;
    public bool hasplayer = false;
    public float speed = 20;
    public Transform tr;

	// Use this for initialization
	void Start () {
        //tr = gameObject.GetComponentInParent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        tr.position += (Target.transform.position - tr.position).normalized * speed*Time.deltaTime;
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "FlyingBoundary" && Target.name==coll.gameObject.name && !seek && !hasplayer)
        {
            Target = coll.gameObject.GetComponent<FlyingBoundaries>().OtherSide;
        }
        else if(coll.tag == "Player")
        {
            seek = false;
            hasplayer = true;
            Target = carry;
            coll.gameObject.transform.parent = gameObject.transform;
            coll.gameObject.GetComponent<PlayerControl>().enabled = false;
            coll.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }
        else if(coll.tag == "FlyingEnd" && hasplayer)
        {
            hasplayer = false;
            GameObject p = gameObject.transform.GetChild(0).gameObject;
            p.GetComponent<PlayerControl>().enabled = true;
            p.GetComponent<PlayerControl>().Jumping = true;
            p.GetComponent<Rigidbody2D>().isKinematic = false;
            p.transform.parent = null;
        }
    }
}
