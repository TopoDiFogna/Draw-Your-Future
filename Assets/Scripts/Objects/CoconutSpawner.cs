using UnityEngine;
using System.Collections;

public class CoconutSpawner : MonoBehaviour {

    bool activated = false;
    public GameObject m_spawned_object;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch" && !activated)
        {
            activated = true;
            GameObject obj = ObjectPoolingManager.Instance.GetObject(m_spawned_object.name);
            obj.transform.position = transform.position;
        }
    }
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch" && !activated)
        {
            activated = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch")
        {
            activated = false;
        }
    }
}
