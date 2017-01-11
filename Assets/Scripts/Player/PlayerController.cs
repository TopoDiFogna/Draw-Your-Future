using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int m_scratch_layer = 8;
    GameController gc;
    public GameObject m_paint;

    [Range(0, 100)]
    public float m_speed = 20f;
    [Range(0, 100)]
    public float m_climbing_speed = 15f;

    [Range(0, 200)]
    public float m_Jump_force = 10f;
    float normal_jump_force;
    public float quicksand_jump_force = 5f;

    // Player Properties
    int lay;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;

    // Status variables
    public bool climbing = false;
    public bool jumping = false;
    public bool sliding = false;
    private bool dead = false;

    Ladder ladder;

    public bool Dead
    {
        get { return dead; }
    }

    private bool has_key = false;

    public bool hasKey
    {
        get { return has_key; }
        set { has_key = value; }
    }

    public bool IsNearLadder = false;

    public bool IsNearLever = false;

    private bool facing_right = true;

    //Controls variables
    private float m_horizontal = 0f;
    private float m_vertical = 0f;
    private float m_axis_jump = 0f;
    bool hasJumped = false;

    private Vector3 checkPointPosition = new Vector3((-26.84f + 19.5f), -2.106468f, -1);
    public Vector3 CheckPointPosition
    {
        get { return checkPointPosition; }
        set { checkPointPosition = value; }
    }

    public Vector2 min_cam_bounds;
    public Vector2 max_cam_bounds;
    public GameObject[] move_camera_bounds_to_reactivate;


    //Death variables
    [Range(0.5f, 5.0f)]
    public float timeToDie = 1.0f;


    // Use this for initialization
    private void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        lay = gameObject.layer;
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        normal_jump_force = m_Jump_force;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!gc.Pause)
        {
            //prendo gli input
            m_horizontal = Input.GetAxisRaw("Horizontal");
            m_vertical = Input.GetAxisRaw("Vertical");
            m_axis_jump = Input.GetAxisRaw("Jump");

            //controllo per non fare un doppio salto troppo in alto
            if(m_axis_jump == 0)
            {
                hasJumped = false;
            }

            //parte di animazione player
            if (!sliding && !dead)
            {
                animator.SetFloat("Horizontal", Mathf.Abs(m_horizontal));
            }

            if (facing_right && m_horizontal < 0)
            {
                sr.flipX = true;
                facing_right = false;
            }
            else if (!facing_right && m_horizontal > 0)
            {
                sr.flipX = false;
                facing_right = true;
            }

            //comandi
            /*
            if (m_vertical != 0 && !jumping && !dead && IsNearLadder)
            {
                    climbing = true;
            }*/
            
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) && !dead && IsNearLadder)
            {
                climbing = true;
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
                if(ladder != null)
                {
                    ladder.DeactivatePlatform();
                }

                //TODO aggiungere oggetto vuoto per centrare il player sulla scala
            }
            /*
            if (Input.GetKeyDown(KeyCode.S) && !jumping && !dead && IsNearLadder && !IsNearLever)
            {
                climbing = true;
                rb.isKinematic = true;
            }*/
            else if (Input.GetKeyDown(KeyCode.S) && !jumping && !dead && !IsNearLadder && IsNearLever)
            {
                //TODO non ho idea di cosa sia questo per cui lo lascio
            }

            /*
            if (!IsNearLadder && rb.isKinematic)
            {
                rb.isKinematic = false;
            }*/

            if (Input.GetKeyDown(KeyCode.F11))
            {
                DieWithFade();
            }
        }
    }

    /*private IEnumerator StopJumpAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Jumping", false);
    }*/

    public void StopAnimation()
    {
        animator.SetFloat("Horizontal", 0);
        animator.SetBool("Jumping", false);
    }

    private void FixedUpdate()
    {
        if (dead)
        {
            //donothing
            return;
        }
        if (sliding)
        {
            //donothing
            return;
        }
        if (!climbing)
        {
            rb.velocity = new Vector2(m_horizontal * m_speed, rb.velocity.y);

            if (m_axis_jump > 0 && !jumping && !hasJumped)
            {
                rb.AddForce(Vector2.up * m_Jump_force, ForceMode2D.Impulse);
                jumping = true;
                hasJumped = true;
                animator.SetBool("Jumping", true);
                /*
                else
                {
                    climbing = true;
                    rb.isKinematic = true;
                }*/
            }
        }
        else
        {
            if(m_axis_jump > 0)
            {
                rb.AddForce(Vector2.up * m_Jump_force, ForceMode2D.Impulse);
                jumping = true;
                hasJumped = true;
                climbing = false;
                animator.SetBool("Jumping", true);
                if(ladder != null)
                {
                    ladder.ActivatePlatform();
                }
                return;
            }
            rb.velocity = new Vector2(0, m_vertical * m_climbing_speed);
            /*if (m_horizontal != 0 || !IsNearLadder)
            {
                climbing = false;
                rb.isKinematic = false;
                rb.velocity = new Vector2(m_horizontal * m_speed, rb.velocity.y);
            }
            else
                rb.velocity = new Vector2(0, m_vertical * m_climbing_speed);*/
        }

    }









    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (jumping == true && coll.gameObject.tag == "Terrain")
        {
            if (climbing)
            {
                climbing = false;
                rb.gravityScale = 2;
                if (ladder != null)
                {
                    ladder.ActivatePlatform();
                }
                ladder = null;
            }
            jumping = false;
            animator.SetBool("Jumping", false);
        }
    }


    private void OnCollisionExit2D(Collision2D coll)
    {
        if (jumping == false && coll.gameObject.tag == "Terrain")
        {
            jumping = true;
            //animator.SetBool("Jumping", true);
        }
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (jumping == true && coll.gameObject.tag == "Terrain")
        {
            jumping = false;
            //animator.SetBool("Jumping", false);
        }

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch")
        {
            gameObject.layer = m_scratch_layer;
        }

        if (coll.gameObject.tag == "Climbable")
        {
            //TODO animazione scalata
            ladder = coll.gameObject.GetComponent<Ladder>();
            IsNearLadder = true;
        }
        if (coll.gameObject.tag == "Quicksand")
        {
            m_Jump_force = quicksand_jump_force;
            jumping = false;
        }

    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch")
        {
            gameObject.layer = m_scratch_layer;
        }
        if (coll.gameObject.tag == "Climbable")
        {
            IsNearLadder = true;
            /*if (climbing)
            {
                rb.isKinematic = true;
            }*/
        }
        if (coll.gameObject.tag == "Quicksand")
        {
            jumping = false;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch")
        {
            gameObject.layer = lay;
        }

        if (coll.gameObject.tag == "Climbable")
        {
            IsNearLadder = false;
            climbing = false;
            rb.gravityScale = 2;
            if (ladder != null)
            {
                ladder.ActivatePlatform();
            }
            ladder = null;
            //gameObject.layer = 9;
        }
        if (coll.gameObject.tag == "Quicksand")
        {
            jumping = true;
            m_Jump_force = normal_jump_force;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public void DieWithFade()
    {
        if (!dead)
        {
            rb.isKinematic = true;
            dead = true;
            StopAnimation();
            CameraFade.StartAlphaFade(Color.black, false, timeToDie * 2f, 0f); // Fades out the screen to black   
            StartCoroutine(ResetScene());
            if (SceneManager.GetActiveScene().name == "Level3_Maya")
            {
                //GameObject.Find("LockedDoor").GetComponent<LockedDoorManager>().Reset();
                foreach (GameObject d in GameObject.FindGameObjectsWithTag("DoorSpawner"))
                {
                    d.GetComponent<DoorSpawnEvent>().Open();
                }
                GameObject.FindGameObjectWithTag("Boulder").GetComponent<BoulderManager>().Set_Reset_Boulder(false);
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("SpawnedSkeleton"))
                {
                    g.SetActive(false);
                }
                BossFightShaman boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossFightShaman>();
                boss.stopFight = true;
                boss.phase = 0;
                //boss.FightStarted = false;
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("Sun"))
                {
                    g.GetComponent<sun_manager>().activ = false;
                    g.GetComponent<SpriteRenderer>().sortingOrder = 0;
                    g.transform.GetChild(0).gameObject.SetActive(false);
                }
                boss.phase1.SetActive(true);
                boss.phase2.SetActive(false);
                boss.phase3.SetActive(false);
                StartCoroutine(DelayColliderActivation(boss.GetComponent<EdgeCollider2D>(),boss));
                //boss.wasp_spawned = false;
                boss.wasp.transform.position = boss.waspSpawn.transform.position;
                boss.wasp.SetActive(false);
                boss.SunNumber = 0;
            }
            else if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "Jungle" )
            {
                gameObject.transform.parent = null;
            }
        }
    }

    private IEnumerator DelayColliderActivation(EdgeCollider2D c, BossFightShaman boss)
    {
        yield return new WaitForSeconds(timeToDie);
        c.enabled = true;
        boss.FightStarted = false;
        boss.wasp_spawned = false;
        boss.stopFight = false;
    }

    private IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(timeToDie);
        CameraController camControl = Camera.main.GetComponent<CameraController>();
        camControl.M_minBounds = min_cam_bounds;
        camControl.M_maxBounds = max_cam_bounds;
        transform.position = checkPointPosition;
        m_Jump_force = normal_jump_force;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<PolygonCollider2D>().enabled = true;
        foreach (GameObject go in move_camera_bounds_to_reactivate)
        {
            go.GetComponent<CameraWithGoingBack>().ResetForDeath();
        }
        yield return new WaitForSeconds(1f);
        rb.isKinematic = false;
        CameraFade.instance.Die();
        dead = false;
        if (SceneManager.GetActiveScene().name == "Level3_Maya")
        {
            GameObject.Find("LockedDoor").GetComponent<LockedDoorManager>().Reset();
        }
    }
}