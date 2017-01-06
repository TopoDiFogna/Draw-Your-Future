using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

    private Vector3 spawnPosition;


    void Start()
    {
        spawnPosition = gameObject.GetComponentInChildren<Transform>().position;
    }
	
	void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            CameraController camControl = Camera.main.GetComponent<CameraController>();
            PlayerController player = coll.gameObject.GetComponent<PlayerController>();
            player.CheckPointPosition = spawnPosition;
            player.min_cam_bounds = camControl.M_minBounds;
            player.max_cam_bounds = camControl.M_maxBounds;
        }
        if(coll.tag == "Ostrich")
        {
            CameraController camControl = Camera.main.GetComponent<CameraController>();
            OstrichController player = coll.gameObject.GetComponent<OstrichController>();
            player.checkPointPosition = spawnPosition;
            player.min_cam_bounds = camControl.M_minBounds;
            player.max_cam_bounds = camControl.M_maxBounds;
        }
    }
}
