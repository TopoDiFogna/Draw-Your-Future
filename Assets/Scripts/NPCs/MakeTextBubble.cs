using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTextBubble : MonoBehaviour {

    private Transform canvas;

	// Use this for initialization
	void Start () {
        canvas = transform.FindChild("ChatBubble");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            canvas.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            canvas.gameObject.SetActive(false);
    }
}
