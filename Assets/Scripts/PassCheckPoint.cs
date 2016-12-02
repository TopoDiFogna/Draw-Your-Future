using UnityEngine;
using System.Collections;

public class PassCheckPoint : MonoBehaviour {

    public PlayerController script;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //script.IsAfterCheckPoint = true;
        }
    }
}
