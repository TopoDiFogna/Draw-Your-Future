using UnityEngine;
using System.Collections;

public class Coconut : MonoBehaviour {

    bool active = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnEnable()
    {
        active = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Crab" && active)
        {
            active = false;
            collision.gameObject.SetActive(false);
        }
    }
}
