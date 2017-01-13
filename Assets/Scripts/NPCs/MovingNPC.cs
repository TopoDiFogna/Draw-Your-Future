using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingNPC : MonoBehaviour {

    public float m_amplitude = 1;
    public float m_speed = 1;

    private SpriteRenderer sr;
    private bool flippedX;
    private Vector3 startingPos;

    private float random;
    private bool can_flip = true;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        flippedX = sr.flipX;
        startingPos = transform.localPosition;
        random = Random.Range(0,Mathf.PI);
	}
	
	// Update is called once per frame
	void Update () {
        float x = Mathf.Sin(Time.time * m_speed + random);
        transform.localPosition = startingPos + new Vector3(m_amplitude * x, 0, 0);
        if (x >= 0.95 && can_flip)
        {
            transform.localScale = new Vector3( -1, transform.localScale.y, transform.localScale.z);
            can_flip = false;
            
        }
        else if (x <= -0.95 && can_flip) {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            can_flip = false;
        }
        else if (x<0.2 && x>-0.2)
        {
            can_flip = true;
        }
    }
}
