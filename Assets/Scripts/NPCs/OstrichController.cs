using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OstrichController : MonoBehaviour
{

    public int m_scratch_layer = 8;
    public int m_player_layer = 9;
    GameController gc;
    public GameObject m_paint;

    [Range(0, 100)]
    public float m_speed = 20f;
    public float m_underwater_speed = 10f;

    [Range(0, 200)]
    public float m_Jump_force = 10f;
    float normal_jump_force;
    public float diving_jump_force = 5f;

    int lay;
    Rigidbody2D rb;
    public Vector3 checkPointPosition;

    public bool jumping = false;
    public bool dead = false;
    public float timeToDismount = 0.5f;
    public int cycles_to_dismount = 50;

    [Range(0.5f, 5.0f)]
    public float timeToDie = 1.0f;

    private float m_horizontal = 0f;
    private float m_vertical = 0f;
    private float m_axis_jump = 0f;

    private bool facing_right = true;

    bool active = false;
    bool with_player = false;
    bool in_water = false;
    bool dismounting = false;
    bool hasJumped = false;
    BoxCollider2D boxcoll;
    CircleCollider2D circlecoll;
    EdgeCollider2D polycoll;
    Animator animator;
    Transform tr;
    GameObject player;

    public Vector2 min_cam_bounds;
    public Vector2 max_cam_bounds;

    public GameObject[] move_camera_bounds_to_reactivate;


    // Use this for initialization
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        tr = transform;
        boxcoll = GetComponent<BoxCollider2D>();
        circlecoll = GetComponent<CircleCollider2D>();
        polycoll = GetComponent<EdgeCollider2D>();
        animator = gameObject.GetComponent<Animator>();
        normal_jump_force = m_Jump_force;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gc.Pause && with_player && !dismounting)
        {
            m_horizontal = Input.GetAxisRaw("Horizontal");
            m_vertical = Input.GetAxisRaw("Vertical");
            m_axis_jump = Input.GetAxisRaw("Jump");

            if (m_axis_jump == 0)
            {
                hasJumped = false;
            }

            if (facing_right && m_horizontal < 0)
            {
                tr.localScale = new Vector3(tr.localScale.x * -1, 1, 1);
                facing_right = false;
            }
            else if (!facing_right && m_horizontal > 0)
            {
                tr.localScale = new Vector3(tr.localScale.x * -1, 1, 1);
                facing_right = true;
            }
            if (Input.GetKeyDown(KeyCode.F11))
            {
                DieWithFade();
            }
        }
    }

    private void FixedUpdate()
    {
        if (dead)
        {
            //donothing
        }
        else
        {
            if (with_player && !dismounting)
            {
                if (!in_water)
                {
                    rb.velocity = new Vector2(m_horizontal * m_speed, rb.velocity.y);
                    if(m_horizontal != 0)
                    {
                        animator.SetBool("Walking", true);
                    }else
                    {
                        animator.SetBool("Walking", false);
                    }
                    if (m_axis_jump > 0 && !jumping && !hasJumped)
                    {
                        rb.AddForce(Vector2.up * m_Jump_force, ForceMode2D.Impulse);
                        jumping = true;
                        hasJumped = true;
                        Debug.Log("set trigger jump");
                        animator.SetTrigger("Jump");
                    }
                }
                if (in_water)
                {
                    rb.velocity = new Vector2(m_horizontal * m_underwater_speed, m_vertical * m_underwater_speed);
                    if (m_horizontal != 0)
                    {
                        animator.SetBool("Walking", true);
                    }
                    else
                    {
                        animator.SetBool("Walking", false);
                    }
                }
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (jumping == true && coll.gameObject.tag == "Terrain" && with_player)
        {
            jumping = false;
            animator.SetTrigger("Landed");
        }
        if(coll.gameObject.tag == "Player" && active)
        {
            animator.SetBool("WithPlayer", true);
            player = coll.gameObject;
            player.SetActive(false);
            CameraController camControl = Camera.main.GetComponent<CameraController>();
            min_cam_bounds = camControl.M_minBounds;
            max_cam_bounds = camControl.M_maxBounds;
            camControl.m_target = gameObject;
            //TODO animazione del player che sale sullo struzzo e poi fa enable dei comandi
            with_player = true;
        }
    }


    private void OnCollisionExit2D(Collision2D coll)
    {
        if (jumping == false && coll.gameObject.tag == "Terrain" && with_player)
        {
            jumping = true;

        }
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (jumping == true && coll.gameObject.tag == "Terrain" && with_player)
        {
            jumping = false;
            //animator.SetTrigger("Landed");
        }

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch" && !active)
        {
            active = true;
            boxcoll.enabled = false;
            circlecoll.enabled = true;
            polycoll.enabled = true;
            rb.isKinematic = false;
            gameObject.layer = m_player_layer;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
            checkPointPosition = transform.position;
        }
        if (coll.gameObject.tag == "Scratch" && active)
        {
            gameObject.layer = m_scratch_layer;
        }
        if(coll.gameObject.tag == "Water")
        {
            in_water = true;
            jumping = true;
            animator.SetTrigger("Landed");
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch" && active)
        {
            gameObject.layer = m_scratch_layer;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch" && active)
        {
            gameObject.layer = m_player_layer;
        }
        if(coll.gameObject.tag == "Water")
        {
            in_water = false;
        }
    }

    public IEnumerator DismountFromOstrich(Vector3 position, Vector3 ostrich)
    {
        if (with_player)
        {
            dismounting = true;
            animator.SetBool("Walking", false);
            animator.SetTrigger("Landed");
            animator.SetBool("WithPlayer", false);
            with_player = false;
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            float time = 0;
            for(int i= 0; i<cycles_to_dismount; i++)
            {
                yield return new WaitForSeconds(timeToDismount / cycles_to_dismount);
                time += timeToDismount / cycles_to_dismount;
                transform.position = Vector3.Lerp(transform.position, ostrich, time);
            }
            rb.isKinematic = false;
            //TODO far partire animazione player di dismount e aspettare che finisca 
            player.transform.position = position;
            player.layer = m_player_layer;
            player.SetActive(true);
            GameObject.FindObjectOfType<CameraController>().m_target = player;

            dismounting = false;
        }
    }

    public void DieWithFade()
    {
        if (!dead)
        {
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            dead = true;
            //StopAnimation();
            CameraFade.StartAlphaFade(Color.black, false, timeToDie * 2f, 0f); // Fades out the screen to black   
            StartCoroutine(ResetScene());
            if (SceneManager.GetActiveScene().name == "Level3_Maya")
            {
                GameObject.FindGameObjectWithTag("Boulder").GetComponent<BoulderManager>().Set_Reset_Boulder(false);
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("SpawnedSkeleton"))
                {
                    g.SetActive(false);
                }
            }
        }
    }

    private IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(timeToDie);
        tr.localScale = new Vector3(1, 1, 1);
        facing_right = true;
        animator.SetBool("Walking", false);
        animator.SetTrigger("Landed");
        CameraController camControl = Camera.main.GetComponent<CameraController>();
        camControl.M_minBounds = min_cam_bounds;
        camControl.M_maxBounds = max_cam_bounds;
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Scratch"))
        {
            g.GetComponent<Paint>().DisablePaintRoutine();
        }
        foreach (GameObject go in move_camera_bounds_to_reactivate)
        {
            go.GetComponent<CameraWithGoingBack>().ResetForDeath();
        }
        yield return new WaitForSeconds(1f);
        transform.position = checkPointPosition;
        rb.isKinematic = false;
        CameraFade.instance.Die();
        dead = false;
    }
}
