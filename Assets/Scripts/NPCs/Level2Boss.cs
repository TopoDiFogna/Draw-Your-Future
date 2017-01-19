using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2Boss : MonoBehaviour {

    private Rigidbody2D rb;
    private Transform tr;
    private SpriteRenderer sr;
    public Transform[] landing_positions;
    private Vector3[] landing_vectors;
    private float[] time_multipliers;
    private float[] original_time_multipliers;
    public float time_to_jump = 1f;
    public float time_to_wait_in_landing = 0.5f;
    private int current_position = 0;

    private int lives = 3;
    public GameObject[] lives_images;
    private float hit_multiplier = 1f;

    bool is_jumping = false;
    public bool hit = false;
    public bool landed = false;
    bool facing_left = true;
    bool facing_right = false;

    GameController gc;

	// Use this for initialization
	void Awake () {
        tr = transform;
        rb = GetComponent<Rigidbody2D>();
        landing_vectors = new Vector3[landing_positions.Length];
        for(int i = 0; i < landing_positions.Length; i++)
        {
            Transform trans = landing_positions[i];
            landing_vectors[i] = new Vector3(trans.position.x, trans.position.y, -1);
        }
        time_multipliers = new float[landing_positions.Length];
        time_multipliers[0] = 1.5f; // 4->1
        time_multipliers[1] = 2.3f; // 1 -> 2
        time_multipliers[2] = 2f; // 2-> 3
        time_multipliers[3] = 1.5f; // 3-> 4
        original_time_multipliers = time_multipliers.Clone() as float[];
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
		if(!is_jumping && !hit)
        {
            current_position++;
            if(current_position >= landing_positions.Length)
            {
                current_position = 0;
            }
            is_jumping = true;
            StartCoroutine("Jump");
        }
	}

    private void OnEnable()
    {
        lives = 3;
        time_multipliers[0] = 1.5f; // 4->1
        time_multipliers[1] = 2.3f; // 1 -> 2
        time_multipliers[2] = 2f; // 2-> 3
        time_multipliers[3] = 1.5f; // 3-> 4
        tr.position = landing_vectors[0];
        hit_multiplier = 1;
        current_position = 0;
        facing_left = true;
        facing_right = false;
        tr.localScale = new Vector3(1, 1, 1);
    }

    IEnumerator Jump()
    {
        Vector3 startingPosition = tr.position;
        Vector3 endingPoint = landing_vectors[current_position];
        if((startingPosition.x-endingPoint.x > 0) && facing_right)
        {
            facing_left = true;
            facing_right = false;
            tr.localScale = new Vector3(1, 1, 1);
        }
        if((startingPosition.x - endingPoint.x<0)&& facing_left)
        {
            facing_left = false;
            facing_right = true;
            tr.localScale = new Vector3(-1, 1, 1);
        }
        float multiplier = time_multipliers[current_position];
        float v0x = (endingPoint.x - startingPosition.x) / multiplier * time_to_jump;
        float v0y = (endingPoint.y + 0.5f * rb.gravityScale * 10 * time_to_jump * time_to_jump * multiplier * multiplier - startingPosition.y) / multiplier * time_to_jump;
        rb.AddForce(new Vector2(v0x, v0y), ForceMode2D.Impulse);
        yield return new WaitForSeconds(time_to_jump * multiplier);
        rb.velocity = Vector2.zero;
        landed = true;
        yield return new WaitForSeconds(time_to_wait_in_landing);
        is_jumping = false;
        landed = false;
    }

    IEnumerator Hit()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(.2f);
        sr.color = Color.white;
        lives_images[3 - lives].SetActive(false);
        lives--;
        if (lives > 0)
        {
            //TODO animazione boss colpito
            hit_multiplier -= 0.25f;
            for (int i = 0; i < time_multipliers.Length; i++)
            {
                time_multipliers[i] = original_time_multipliers[i] * hit_multiplier;
            }
            //faccio ripartire appena finita animazione?
            yield return new WaitForSeconds(time_to_wait_in_landing - 0.2f);
            is_jumping = false;
            hit = false;
        }else
        {
            //TODO morte boss e fine livello
            gc.ShowWinningMenu();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().DieWithFade();
            GameObject.FindObjectOfType<BossTrigger>().ResetBoss();
        }
    }

    public void ResetVariables()
    {
        is_jumping = false;
        hit = false;
        landed = false;
    }

}
