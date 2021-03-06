﻿using UnityEngine;
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
    Transform tr;

    // Status variables
    public bool climbing = false;
    public bool jumping = false;
    public bool sliding = false;
    private bool dead = false;
    public bool canBeGrabbed = true;

    public bool CanBeGrabbed
    {
        get { return canBeGrabbed; }
        set { canBeGrabbed = value; }
    }

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

    private int child_count;
    private Transform[] children;

    // Use this for initialization
    private void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        lay = gameObject.layer;
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        normal_jump_force = m_Jump_force;
        tr = transform;
        child_count = tr.childCount;
        children = new Transform[child_count];
        for(int i = 0; i<child_count; i++)
        {
            children[i] = tr.GetChild(i);
        }
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
            if (!sliding && !dead && !climbing)
            {
                animator.SetFloat("Horizontal", Mathf.Abs(m_horizontal));
            }

            if (facing_right && m_horizontal < 0)
            {
                tr.localScale = new Vector3(-1, 1, 1);
                facing_right = false;
            }
            else if (!facing_right && m_horizontal > 0)
            {
                sr.flipX = false;
                tr.localScale = new Vector3(1, 1, 1);
                facing_right = true;
            }

            //comandi
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) && !dead && IsNearLadder)
            {
                climbing = true;
                jumping = false;
                hasJumped = false;
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
                if(ladder != null)
                {
                    ladder.DeactivatePlatform();
                    if(ladder.center != null)
                    {
                        tr.position = new Vector3(ladder.center.transform.position.x, tr.position.y, tr.position.z);
                    }

                }
                animator.SetBool("Climbing", true);
                animator.SetBool("Jumping", false);
            }
            else if (Input.GetKeyDown(KeyCode.S) && !jumping && !dead && !IsNearLadder && IsNearLever)
            {
                //TODO non ho idea di cosa sia questo per cui lo lascio
            }
            if (Input.GetKeyDown(KeyCode.F11))
            {
                DieWithFade();
            }
        }
    }

    public void StopAnimation()
    {
        animator.SetFloat("Horizontal", 0);
        animator.SetBool("Jumping", false);
        animator.SetBool("Climbing", false);
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
                Debug.Log("ho saltato");
            }
        }
        else
        {
            if(m_axis_jump > 0)
            {
                rb.gravityScale = 2;
                rb.AddForce(Vector2.up * m_Jump_force, ForceMode2D.Impulse);
                jumping = true;
                hasJumped = true;
                climbing = false;
                animator.SetFloat("Vertical", 0f);
                animator.SetBool("Jumping", true);
                animator.SetBool("Climbing", false);
                if(ladder != null)
                {
                    ladder.ActivatePlatform();
                }
                return;
            }
            rb.velocity = new Vector2(0, m_vertical * m_climbing_speed);
            animator.SetFloat("Vertical", Mathf.Abs(m_vertical));
        }

    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (jumping == true && coll.gameObject.tag == "Terrain")
        {
            jumping = false;
            animator.SetBool("Jumping", false);
        }
        else if(climbing && coll.gameObject.tag == "Terrain")
        {
            jumping = false;
            hasJumped = false;
            rb.gravityScale = 2;
            climbing = false;
            animator.SetBool("Climbing", false);
            if (ladder != null)
            {
                ladder.ActivatePlatform();
            }
        }
    }


    private void OnCollisionExit2D(Collision2D coll)
    {
        if (jumping == false && coll.gameObject.tag == "Terrain" && !climbing)
        {
            jumping = true;
        }
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (jumping == true && coll.gameObject.tag == "Terrain")
        {
            jumping = false;
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
        if (coll.tag == "SafeZone")
        {
            canBeGrabbed = false;
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
        }
        if (coll.gameObject.tag == "Quicksand")
        {
            jumping = false;
        }
        if (coll.tag == "SafeZone")
        {
            canBeGrabbed = false;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch")
        {
            bool found = false;
            foreach(Transform child in children)
            {
                RaycastHit2D[] ray1 = Physics2D.RaycastAll(child.position, new Vector2(1,0), 0.1f);
                Debug.DrawRay(child.position, new Vector3(1, 0, 0)*0.1f, Color.red, 4f);
                RaycastHit2D[] ray2 = Physics2D.RaycastAll(child.position, new Vector2(-1, 0), 0.1f);
                Debug.DrawRay(child.position, new Vector3(-1, 0, 0)*0.1f, Color.red, 4f);
                foreach (RaycastHit2D ray in ray1)
                {
                    if(ray.collider.gameObject.tag == "Scratch")
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    foreach (RaycastHit2D ray in ray2)
                    {
                        if (ray.collider.gameObject.tag == "Scratch")
                        {
                            found = true;
                            break;
                        }
                    }
                }
            }
            if (!found)
            {
                gameObject.layer = lay;
            }

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
            jumping = true;
            hasJumped = true;
            animator.SetBool("Climbing", false);
        }
        if (coll.gameObject.tag == "Quicksand")
        {
            jumping = true;
            m_Jump_force = normal_jump_force;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        if (coll.tag == "SafeZone")
        {
            canBeGrabbed = true;
        }
    }

    public void DieWithFade()
    {
        if (!dead)
        {
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            dead = true;
            StopAnimation();
            animator.SetTrigger("Death");
            //fadedelay aumentato a 0.5 per far andare animazione morte
            CameraFade.StartAlphaFade(Color.black, false, timeToDie * 2f, 0.5f); // Fades out the screen to black   
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
        yield return new WaitForSeconds(timeToDie+2f);
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
        m_Jump_force = normal_jump_force;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        foreach (GameObject go in move_camera_bounds_to_reactivate)
        {
            go.GetComponent<CameraWithGoingBack>().ResetForDeath();
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Scratch"))
        {
            g.GetComponent<Paint>().DisablePaintRoutine();
        }
        yield return new WaitForSeconds(1f);
        facing_right = true;
        transform.position = checkPointPosition;
        rb.isKinematic = false;
        tr.localScale = new Vector3(1, 1, 1);
        CameraFade.instance.Die();
        dead = false;
        if (SceneManager.GetActiveScene().name == "Level3_Maya")
        {
            GameObject.Find("LockedDoor").GetComponent<LockedDoorManager>().Reset();
        }
    }
}