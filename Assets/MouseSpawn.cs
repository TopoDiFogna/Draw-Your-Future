using UnityEngine;
using System.Collections;

public class MouseSpawn : MonoBehaviour {

    public GameObject scratch;
    Vector3 mouse;

	// Use this for initialization
	void Start () {

        ObjectPoolingManager.Instance.CreatePool(scratch, 200, 200);

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0)) 
        {
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject go = ObjectPoolingManager.Instance.GetObject("Paint");
            if (go != null)
            {
                go.transform.position = new Vector3(mouse.x, mouse.y, 3);
                go.transform.rotation = Quaternion.identity;
            }
            //Instantiate(scratch, new Vector3(mouse.x, mouse.y, 3), Quaternion.identity);
        }
	}
}
