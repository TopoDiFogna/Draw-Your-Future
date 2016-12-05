using UnityEngine;
using System.Collections;

public class RockSpawner : MonoBehaviour
{

    bool activated = false;

    public GameObject m_spawned_object;
    public int objects_to_spawn = 5;

    private int spawned_objects = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("hitted");
        if (coll.gameObject.tag == "Scratch" && !activated && (spawned_objects < objects_to_spawn))
        {
            activated = true;
            GameObject obj = ObjectPoolingManager.Instance.GetObject(m_spawned_object.name);
            obj.transform.position = transform.position;
            spawned_objects++;
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
