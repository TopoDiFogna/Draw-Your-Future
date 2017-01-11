using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScratching : MonoBehaviour {

    public MouseSpawn ms;

    private void Awake()
    {
        ms.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ms.enabled = true;
        }
    }
}
