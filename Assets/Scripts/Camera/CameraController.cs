using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject m_target;
    [Range(1f,50f)]
    public float m_move_speed = 10f;

    Camera cam;
    float camVertExtent;
    float camHorzExtent;
    float leftBound;
    float rightBound;
    float bottomBound;
    float topBound;

    float camX;
    float camY;

    public Vector2 m_minBounds = new Vector2(-9.3f,-5.4f);
    public Vector2 M_minBounds
    {
        get
        {
            return m_minBounds;
        }
        set
        {
            m_minBounds = value;
            leftBound = m_minBounds.x + camHorzExtent;
            bottomBound = m_minBounds.y + camVertExtent;
        }
    }
    public Vector2 m_maxBounds = new Vector2(336.3f,5.4f);
    public Vector2 M_maxBounds
    {
        get
        {
            return m_maxBounds;
        }
        set
        {
            m_maxBounds = value;
            rightBound = m_maxBounds.x - camHorzExtent;
            topBound = m_maxBounds.y - camVertExtent;
        }
    }




    Vector3 target_position;
    Transform tr;

	// Use this for initialization
	void Start () {
        tr = gameObject.GetComponent<Transform>();
        cam = gameObject.GetComponent<Camera>();
        camVertExtent = cam.orthographicSize;
        camHorzExtent = cam.aspect * camVertExtent;
        leftBound = m_minBounds.x + camHorzExtent;
        rightBound = m_maxBounds.x - camHorzExtent;
        bottomBound = m_minBounds.y + camVertExtent;
        topBound = m_maxBounds.y - camVertExtent;
    }

    private void Update()
    {
        target_position = m_target.GetComponent<Transform>().position;
        camX = Mathf.Clamp(target_position.x, leftBound, rightBound);
        camY = Mathf.Clamp(target_position.y, bottomBound, topBound);
    }

    // Update is called once per frame
    void FixedUpdate () {
        

        tr.position = Vector3.Lerp(tr.position, new Vector3(camX, camY, tr.position.z), m_move_speed * Time.deltaTime);	    
	}
}
