using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2Boss : MonoBehaviour {

    private Rigidbody2D rb;
    private Transform tr;
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
    }

    IEnumerator Jump()
    {
        Vector3 startingPosition = tr.position;
        Vector3 endingPoint = landing_vectors[current_position];
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
            yield return new WaitForSeconds(time_to_wait_in_landing);
            is_jumping = false;
            hit = false;
        }else
        {
            //TODO morte boss e fine livello
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
