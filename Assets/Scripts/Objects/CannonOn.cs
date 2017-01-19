using UnityEngine;
using System.Collections;
using System;

public class CannonOn : MonoBehaviour {

    public GameObject CBSpawn;
    public GameObject CannonBall;
    public GameObject Spark;
    public GameObject Direction;
    public float BallPower;

    Vector3 CBSPosition;
    Vector2 BallDir;

    public bool activated = false;

    private AudioSource source;
    public AudioClip explosion;

    [Header("Cannon Movement (ignore if not used)")]
    public GameObject m_cannon;
    public bool m_should_cannon_move = false;
    public Transform m_final_cannon_position;
    public float m_movement_speed;
    private Vector3 finalPos;


    // Use this for initialization
    private void Start () {
        BallDir = (Direction.transform.position - CBSpawn.transform.position).normalized;
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        //CBSPosition = CBSpawn.transform.position;
        BallDir = (Direction.transform.position - CBSpawn.transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch" && !activated)
        {
            activated = true;
            StartCoroutine(CannonShoot());
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch" && !activated)
        {
            activated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch")
        {
            activated = false;
        }
    }

    private IEnumerator CannonShoot()
    {
        GameObject g = Instantiate(Spark);
        g.transform.position = transform.position;
        g.transform.localScale = new Vector3(1, 1, 1);
        yield return new WaitForSeconds(2);
        g.SetActive(false);
        source.PlayOneShot(explosion);
        GameObject cb = ObjectPoolingManager.Instance.GetObject(CannonBall.name.ToString());
        cb.transform.position = CBSpawn.transform.position;
        cb.GetComponent<Rigidbody2D>().AddForce(BallDir * BallPower, ForceMode2D.Impulse);
        if (m_should_cannon_move)
        {
            m_should_cannon_move = false;
            finalPos = m_final_cannon_position.position;
            Transform cannonPos = m_cannon.transform;
            while (Vector2.Distance(cannonPos.position, finalPos) > 2f)
            {
                cannonPos.position += new Vector3(Vector3.right.x * m_movement_speed * Time.deltaTime, 0, 0);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
