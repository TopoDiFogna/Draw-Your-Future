using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour {

    public GameObject child1;
    public GameObject child2;
    public float time_between_frames;
    private float time = 0;


	// Use this for initialization
	void Start () {
        child1.SetActive(true);
        child2.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time > time_between_frames)
        {
            child1.SetActive(!child1.activeSelf);
            child2.SetActive(!child2.activeSelf);
            time = 0;
        }
	}
}
