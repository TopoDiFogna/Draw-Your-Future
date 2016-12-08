using UnityEngine;
using System.Collections;

public class SwingingObject : MonoBehaviour {

    public float speed = 1.0f;
    public float maxRotation = 30.0f;

	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, maxRotation * Mathf.Sin(Time.time * speed));
    }
}
