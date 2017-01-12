using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{

    public GameObject platform;
    public GameObject center;

    public void DeactivatePlatform()
    {
        if(platform != null)
        {
            platform.SetActive(false);
        }

    }

    public void ActivatePlatform()
    {
        if (platform != null)
        {
            platform.SetActive(true);
        }
    }
}