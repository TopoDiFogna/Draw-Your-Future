using UnityEngine;
using System.Collections;

public class FishController : MonoBehaviour {

    Rigidbody2D rb;
    Transform tr;
    Vector3 startingPosition;
    public Transform endingGameObject;
    Vector3 endingPoint;
    [Range(0.0f, 10.0f)]
    public float m_timeToJump;
    [Range(0.5f, 10.0f)]
    public float m_timeToWait;
    //public Vector2 m_jumpDirection;
    [Range(0.0f, 20.0f)]
    //public float m_jumpForce;
    FishController script;
    SpriteRenderer rend;

    void Awake()
    {
        tr = transform;
        startingPosition = new Vector3(tr.position.x, tr.position.y, 0);
        endingPoint = new Vector3(endingGameObject.position.x, endingGameObject.position.y, 0);
        script = GetComponent<FishController>();
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
	void Update () {

	}

    void OnEnable()
    {
        rend.enabled = true;
        tr.position = startingPosition;
        StartCoroutine("FishJump");
    }

    IEnumerator FishJump()
    {

        float v0x = (endingPoint.x- startingPosition.x) / m_timeToJump;
        float v0y = (endingPoint.y + 0.5f*rb.gravityScale*10 * m_timeToJump*m_timeToJump - startingPosition.y) / m_timeToJump;
        rb.AddForce(new Vector2(v0x, v0y), ForceMode2D.Impulse);
        //rb.AddForce(m_jumpDirection * m_jumpForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(m_timeToJump);
        script.enabled = false;
        rend.enabled = false;
        yield return new WaitForSeconds(m_timeToWait);
        tr.position = startingPosition;
        script.enabled = true;


    }
}
