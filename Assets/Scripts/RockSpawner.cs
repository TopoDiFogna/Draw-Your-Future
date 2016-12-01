using UnityEngine;
using System.Collections;

public class RockSpawner : MonoBehaviour {

    bool activated = false;

    public GameObject m_spawned_object;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("hitted");
        if (coll.gameObject.tag == "Scratch" && !activated)
        {
            activated = true;
            GameObject obj = ObjectPoolingManager.Instance.GetObject(m_spawned_object.name);
            obj.transform.position = transform.position;
        }
    }
}
