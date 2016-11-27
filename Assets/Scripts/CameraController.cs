using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject m_target;
    [Range(1f,50f)]
    public float m_move_speed = 10f;

    Vector3 target_position;
    Transform tr;

	// Use this for initialization
	void Start () {
        tr = gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        target_position = m_target.GetComponent<Transform>().position;

        tr.position = Vector3.Lerp(tr.position, new Vector3(target_position.x, tr.position.y, tr.position.z), m_move_speed * Time.deltaTime);	    
	}
}
