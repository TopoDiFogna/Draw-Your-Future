using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearTrap : MonoBehaviour {

    public float force=20;
    public Transform[] spawns;
    public GameObject spear;

    void Update()
    {

    }
	
    void OnEnable()
    {
        print("attivato");
        foreach(Transform tr in spawns)
        {
            GameObject g = Instantiate(spear, tr.position,Quaternion.identity);
            g.GetComponent<Rigidbody2D>().AddForce(Vector2.left * force, ForceMode2D.Impulse);
        }
        enabled = false;
    }
}
