using UnityEngine;
using System.Collections;

public class MortalObject : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            print("ECCHECAZZO");
            //coll.GetComponent<PlayerController>().Die11();
            coll.GetComponent<PlayerController>().Die2();
            gameObject.SetActive(false);
        }
    }
}
