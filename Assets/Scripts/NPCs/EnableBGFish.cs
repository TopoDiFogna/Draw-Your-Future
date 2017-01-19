using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBGFish : MonoBehaviour {

    public GameObject child_fish;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Scratch")
        {
            child_fish.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
