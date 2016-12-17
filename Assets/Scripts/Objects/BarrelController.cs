using UnityEngine;
using System.Collections;

public class BarrelController : MonoBehaviour {

    public float amplitudeX;
    public float amplitudeY;
    public float omegaX;
    public float omegaY;

    private float index = 0;
    Vector3 start_position;
    Transform tr;


	// Use this for initialization
	private void Start () {
        tr = GetComponent<Transform>();
        start_position = tr.position;
	}

    private void Update()
    {
        index += Time.deltaTime;
        float x = amplitudeX * Mathf.Cos(omegaX * index);
        float y = amplitudeY * Mathf.Sin(omegaY * index);
        tr.position = start_position + new Vector3(x, y, 0);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        collision.transform.parent = tr;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.parent = null;
    }
}
