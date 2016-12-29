using UnityEngine;
using System.Collections;

public class JumpingPlat : MonoBehaviour {

    public float force = 10;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            animator.SetTrigger("Jump");
            coll.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force, ForceMode2D.Impulse);

        }
    }
}
