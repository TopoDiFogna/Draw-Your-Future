using UnityEngine;
using System.Collections;

public class MouseSpawn : MonoBehaviour {

    public GameObject m_scratch;
    Vector3 mouse_position;

	// Use this for initialization
	void Start () {
        ObjectPoolingManager.Instance.CreatePool(m_scratch, 200, 200);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0)) 
        {
            mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject go = ObjectPoolingManager.Instance.GetObject("Paint");
            if (go != null)
            {
                go.transform.position = new Vector3(mouse_position.x, mouse_position.y, 0);
                go.transform.rotation = Quaternion.identity;
            }
        }
	}
}
