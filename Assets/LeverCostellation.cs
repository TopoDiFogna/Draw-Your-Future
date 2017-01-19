using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverCostellation : MonoBehaviour {

    public Sprite LevR, LevL;
    bool activated, interactable;
    SpriteRenderer sr;
    public int element;
    public LockedDoorManager ldm;
    public SpriteRenderer target;

    // Use this for initialization
    void Start () {
        activated = false;
        interactable = true;
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Reset()
    {
        activated = false;
        interactable = true;
        target.color = Color.white;
        sr.sprite = LevL;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Scratch" && interactable)
        {
            interactable = false;
            if (!activated)
            {
                activated = true;
                sr.sprite = LevR;
                target.color = Color.yellow;
                ldm.AddElement(element);
            }
            else
            {
                activated = false;
                sr.sprite = LevL;
                target.color = Color.white;
                ldm.PopElement(element);
            }
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.tag=="Scratch" && interactable)
        {
            interactable = false;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Scratch" && !interactable)
        {
            interactable = true;
        }
    }
}
