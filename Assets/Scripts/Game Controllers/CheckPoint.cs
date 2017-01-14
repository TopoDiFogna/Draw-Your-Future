using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

    private Vector3 spawnPosition;
    public GameObject[] move_camera_bounds_to_reactivate;
    Animator animator;

    void Start()
    {
        spawnPosition = gameObject.GetComponentsInChildren<Transform>()[1].position;
        animator = GetComponent<Animator>();
    }
	
	void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            CameraController camControl = Camera.main.GetComponent<CameraController>();
            PlayerController player = coll.gameObject.GetComponent<PlayerController>();
            if (!player.Dead)
            {
                player.CheckPointPosition = spawnPosition;
                player.min_cam_bounds = camControl.M_minBounds;
                player.max_cam_bounds = camControl.M_maxBounds;
                player.move_camera_bounds_to_reactivate = move_camera_bounds_to_reactivate;
                animator.SetBool("Animate", true);
            }

        }
        if(coll.tag == "Ostrich")
        {
            CameraController camControl = Camera.main.GetComponent<CameraController>();
            OstrichController player = coll.gameObject.GetComponent<OstrichController>();
            if (!player.dead)
            {
                player.checkPointPosition = spawnPosition;
                player.min_cam_bounds = camControl.M_minBounds;
                player.max_cam_bounds = camControl.M_maxBounds;
                player.move_camera_bounds_to_reactivate = move_camera_bounds_to_reactivate;
                animator.SetBool("Animate", true);
            }
        }
    }
}
