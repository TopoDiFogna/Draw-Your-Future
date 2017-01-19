using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismountFromOstrich : MonoBehaviour {

    public Transform dismount_position;
    public Transform ostrich_position;
    private Vector3 ostrich_vector;
    private Vector3 dismount_vector;

    private void Start()
    {
        dismount_vector = new Vector3(dismount_position.position.x, dismount_position.position.y, dismount_position.position.z);
        ostrich_vector = new Vector3(ostrich_position.position.x, ostrich_position.position.y, ostrich_position.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ostrich")
        {
            StartCoroutine(collision.gameObject.GetComponent<OstrichController>().DismountFromOstrich(dismount_vector, ostrich_vector));
        }
    }


}
