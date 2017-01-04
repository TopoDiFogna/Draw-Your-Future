using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {

    bool activated = false;
    public GameObject[] objectsToActivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !activated)
        {
            activated = true;
            foreach(GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
            }
        }
    }

    public void ResetBoss()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(false);
            activated = false;
        }
    }
}
