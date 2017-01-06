using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWithGoingBack : MonoBehaviour {

    public float min_bound_delta_y = 10.8f;
    public float max_bound_delta_y = 10.8f;
    bool activated = false;
    public GameObject backwardCameraToActivate;

    private void OnEnable()
    {
        activated = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Ostrich" || collision.tag == "Player") && !activated)
        {
            CameraController camControl = Camera.main.GetComponent<CameraController>();
            camControl.M_minBounds = camControl.M_minBounds - new Vector2(0, min_bound_delta_y);
            camControl.M_maxBounds = camControl.M_maxBounds - new Vector2(0, max_bound_delta_y);
            activated = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.tag == "Ostrich" || collision.tag == "Player") && activated)
        {
            backwardCameraToActivate.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void ResetForDeath()
    {
        activated = false;
        gameObject.SetActive(true);
        backwardCameraToActivate.GetComponent<CameraWithGoingBack>().ResetSingle();
    }

    public void ResetSingle()
    {
        activated = false;
        gameObject.SetActive(false);
    }
}
