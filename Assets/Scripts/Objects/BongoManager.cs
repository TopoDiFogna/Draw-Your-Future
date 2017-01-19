using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BongoManager : MonoBehaviour {

    bool pressed;
    public BongoPuzzle bp;
    public int id;
    AudioSource auso;

	// Use this for initialization
	void Start () {
        pressed = false;
        auso = GetComponent<AudioSource>();
	}
	
	void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player" && !pressed)
        {
            pressed = true;
            auso.Play(0);
            bp.AddElement(id);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            pressed = false;
        }
    }
}
