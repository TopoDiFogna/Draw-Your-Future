using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBreak : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Terrain")
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
