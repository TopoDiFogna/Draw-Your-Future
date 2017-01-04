using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEggSpawner : MonoBehaviour
{

    bool activated = false;
    public GameObject m_spawned_object;
    public GameObject endingGO;
    public float inactive_time = 2f;
    Transform tr;

    private GameObject instance;

    private void Start()
    {
        instance = GameObject.Instantiate(m_spawned_object);
        instance.GetComponent<BossEgg>().endingGO = endingGO.transform;
        tr = transform;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch")
        {
            coll.gameObject.GetComponent<Paint>().DisablePaintRoutine();
            if (!activated)
            {
                activated = true;
                instance.transform.position = tr.position;
                instance.SetActive(true);
                StartCoroutine("KeepInactive");
            }

        }

    }

    IEnumerator KeepInactive()
    {
        yield return new WaitForSeconds(inactive_time);
        activated = false;
    }

}
