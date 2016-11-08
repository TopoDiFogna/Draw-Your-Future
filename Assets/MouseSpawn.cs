using UnityEngine;
using System.Collections;

public class MouseSpawn : MonoBehaviour {

    public GameObject scratch;
    Vector3 mouse;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0)) 
        {
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(scratch, new Vector3(mouse.x, mouse.y, 3), Quaternion.identity);
        }
	}
}
