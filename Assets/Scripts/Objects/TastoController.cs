using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TastoController : MonoBehaviour {

    public int element;
    public LockedDoorManager ldm;
    bool pressed;
    public SpriteRenderer target;

	// Use this for initialization
	void Start () {
        pressed = false;
	}
	
    public void Reset()
    {
        pressed = false;
        target.color = Color.red;
    }
	// Update is called once per frame

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag=="Player" && !pressed)
        {
            pressed = true;
            target.color = Color.green;
            ldm.AddElement(element);
        }
    }
}
