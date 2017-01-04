using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_porta : MonoBehaviour {

    public GameObject ymin, ymax;
    float xi,zi;
    public bool up;

	// Use this for initialization
	void Start () {
        if (transform.position.y >= ymax.transform.position.y)
            up = true;
        else
            up = false;
        xi = transform.position.x;
        zi = transform.position.z;
	}
	
	// Update is called once per frame
	public void Switch()
    {
        if (up)
        {
            transform.position = new Vector3(xi, ymin.transform.position.y, zi);
            up = false;
        }
        else
        {
            transform.position = new Vector3(xi, ymax.transform.position.y, zi);
            up = true;
        }
        print(ymin+" "+ymax+" "+transform.position);
    }
}
