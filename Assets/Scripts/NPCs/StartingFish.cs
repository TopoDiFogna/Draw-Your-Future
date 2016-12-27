using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingFish : MonoBehaviour {

    public float m_amplitude = 1;
    public float m_speed = 1;

    private SpriteRenderer sr;


	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        float x = Mathf.Sin(Time.time * m_speed);
        transform.localPosition = new Vector3(m_amplitude * x, transform.localPosition.y, transform.localPosition.z);
        if (x >= 0.95)
        {
            sr.flipX = false;
        }
        else if (x <= -0.95)
            sr.flipX = true;
    }
}
