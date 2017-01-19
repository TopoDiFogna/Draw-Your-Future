using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSpear : MonoBehaviour {

    public float speed;
    public GameObject spawn;
    Transform tr;
    GameController gc;

	// Use this for initialization
	void Start () {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        tr = transform;
        tr.position = spawn.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if(!gc.paused)
            tr.position = new Vector3(tr.position.x - speed * Time.deltaTime, tr.position.y, tr.position.z);
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Finish")
        {
            tr.position = spawn.transform.position;
        }
    }
}
