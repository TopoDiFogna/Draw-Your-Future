using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_porta : MonoBehaviour {

    public BoxCollider2D bc;
    public SpriteRenderer[] sr;
    public GameObject ymin, ymax;
    float xi,zi;
    public bool up;

	// Use this for initialization
	void Start () {
        //sr = GetComponent<SpriteRenderer>();
        //bc = GetComponent<BoxCollider2D>();
        if (transform.position.y >= ymax.transform.position.y)
            up = true;
        else
            up = false;
        xi = transform.position.x;
        zi = transform.position.z;
        if (up)
        {
            bc.enabled = false;
            foreach(SpriteRenderer s in sr)
                s.enabled = false;
        }
        else
        {
            bc.enabled = true;
            foreach (SpriteRenderer s in sr)
                s.enabled = true;
        }
	}
	
	// Update is called once per frame
	public void Switch()
    {
        if (up)
        {
            bc.enabled = true;
            foreach (SpriteRenderer s in sr)
                s.enabled = true;
            transform.position = new Vector3(xi, ymin.transform.position.y, zi);
            up = false;
        }
        else
        {
            bc.enabled = false;
            foreach (SpriteRenderer s in sr)
                s.enabled = false;
            transform.position = new Vector3(xi, ymax.transform.position.y, zi);
            up = true;
        }
        print(ymin+" "+ymax+" "+transform.position);
    }
}
