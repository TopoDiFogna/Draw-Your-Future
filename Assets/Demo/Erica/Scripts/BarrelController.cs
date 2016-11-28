using UnityEngine;
using System.Collections;

public class BarrelController : MonoBehaviour {

    public float amplitudeX;
    public float amplitudeY;
    public float omegaX;
    public float omegaY;
    float index = 0;
    Transform tr;


	// Use this for initialization
	void Start () {
        tr = GetComponent<Transform>();
	}

    public void Update()
    {
        index += Time.deltaTime;
        float x = amplitudeX * Mathf.Cos(omegaX * index);
        float y = amplitudeY * Mathf.Sin(omegaY * index);
        transform.position = new Vector3(x, y, 0);
    }










}
