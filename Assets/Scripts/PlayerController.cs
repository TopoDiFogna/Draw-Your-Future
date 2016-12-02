using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public int m_scratch_layer = 9;

    public GameObject m_paint;

    [Range(0, 100)]
    public float m_speed = 20f;

    [Range(0, 100)]
    public float m_climbing_speed = 15f;

    [Range(0, 200)]
    public float m_Jump_force = 10f;

    // Player Properties
    int lay;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;
    BoxCollider2D boxColl;
    CircleCollider2D circleColl;

    // Status variables
    public bool jumping = false;

    public bool IsNearLadder = false;

    bool facing_right = true;

    //Controls variables
    float m_horizontal = 0f;
    float m_vertical = 0f;

    //Variables for level control
    public bool IsAfterCheckPoint
    {
        set
        {
            isAfterCheckPoint = value;
        }
    }
    bool isAfterCheckPoint = false;
    public Vector3 StartingPosition
    {
        set
        {
            startingPosition = value;
        }
    }
    Vector3 startingPosition;
    public Vector3 CheckPointPosition
    {
        set
        {
            checkPointPosition = value;
        }
    }
    Vector3 checkPointPosition;


    //Death variables
    [Range(0.5f, 3.0f)]
    public float timeToDie = 1.0f;
    public float timeJump = 0.1f;
    public Camera blackCamera;

    
    // Use this for initialization
    void Start()
    {
        lay = gameObject.layer;
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        boxColl = gameObject.GetComponent<BoxCollider2D>();
        circleColl = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_horizontal = Input.GetAxisRaw("Horizontal");
        m_vertical = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", Mathf.Abs(m_horizontal));

        if(facing_right && m_horizontal < 0)
        {
            sr.flipX = true;
            facing_right = false;
        }
        else if(!facing_right && m_horizontal > 0)
        {
            sr.flipX = false;
            facing_right = true;
        }

        /*if (climbing)
        {
            if (m_vertical != 0)
            {
                tr.position += new Vector3(0, m_climbing_speed * m_vertical * Time.deltaTime, 0);
            }
        }
        else
        {
            // TODO 
        }*/

        if (Input.GetKeyDown(KeyCode.W) && !jumping && !IsNearLadder)
        {
            rb.AddForce(Vector2.up * m_Jump_force, ForceMode2D.Impulse);
            jumping = true;
            animator.SetBool("Jumping", true);
            StartCoroutine(StopJumpAnimation());
        }
    }

    IEnumerator StopJumpAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Jumping", false);
    }

    public void StopAnimation()
    {
        animator.SetFloat("Horizontal", 0);
    }

    void FixedUpdate()
    {
        if (!IsNearLadder)
        {
            rb.velocity = new Vector2(m_horizontal * m_speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(m_horizontal * m_speed, m_vertical * m_climbing_speed);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (jumping == true && coll.gameObject.tag == "Terrain")
        {
            jumping = false;
            animator.SetBool("Jumping", false);
        }
    }


    void onCollisionExit2D(Collision2D coll)
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch")
        {
            gameObject.layer = m_scratch_layer;
        }

        if (coll.gameObject.tag == "Climbable")
        {
            jumping = false;
            IsNearLadder = true;
            rb.isKinematic = true;
        }


    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch")
        {
            gameObject.layer = m_scratch_layer;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch")
        {
            gameObject.layer = lay;
        }

        if (coll.gameObject.tag == "Climbable")
        {
            IsNearLadder = false;
            rb.isKinematic = false;
        }
    }

    public void Die11()
    {
        StartCoroutine(Die1());
    }

    //Versione di morte con scomparsa dello schermo e ritorno immediato
    public IEnumerator Die1()
    {
        blackCamera.enabled = true;
        //Camera.main.enabled = false;
        yield return new WaitForSeconds(timeToDie);
        if (isAfterCheckPoint)
        {
            transform.position= checkPointPosition;
        }
        else
        {
            transform.position = startingPosition;
        }
        Camera.main.enabled = true;
        blackCamera.enabled = false;
    }


    //Versione di morte con ritorno graduale
    public void Die2()
    {
        rb.isKinematic = true;
        circleColl.enabled = false;
        boxColl.enabled = false;
        Vector3 startingPoint = new Vector3(transform.position.x, transform.position.y, 0);
        Vector3 endingPoint;
        float time = 0;

        if (isAfterCheckPoint)
        {
            endingPoint = checkPointPosition;
        }else
        {
            endingPoint = startingPosition;
        }

        while(time <= timeToDie)
        {
            transform.position = Vector3.Lerp(startingPoint, endingPoint, time / timeToDie);
            time += timeJump;
        }
        rb.isKinematic = false;
        circleColl.enabled = true;
        boxColl.enabled = true;
    }
}
