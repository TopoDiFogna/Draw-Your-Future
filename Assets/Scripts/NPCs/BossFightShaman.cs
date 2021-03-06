﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightShaman : MonoBehaviour
{
    public GameObject portal;
    GameController gc;
    public DoorSpawnEvent dse;
    GameObject player;
    public GameObject phase1, phase2, phase3;
    public GameObject wasp, waspSpawn;
    public bool FightStarted;
    public int phase;
    public bool wasp_spawned;
    public float fire_rate;
    public float spawn_rate;
    public GameObject MinSpawn, MaxSpawn;
    public GameObject Skeleton;
    float timer = 0f;
    public int SunNumber;
    SpriteRenderer sr;
    public GameObject Attack;
    public bool stopFight;
    public GameObject Death;
    public GameObject Summon;

    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        portal.SetActive(false);
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        SunNumber = 0;
        ObjectPoolingManager.Instance.CreatePool(Attack, 10, 10);
        ObjectPoolingManager.Instance.CreatePool(Skeleton, 10, 10);
        player = GameObject.FindGameObjectWithTag("Player");
        FightStarted = false;
        phase = 0;
        wasp_spawned = false;
        stopFight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gc.paused && !stopFight)
        {
            if (FightStarted)
            {
                if (phase == 0)
                {
                    if (!wasp_spawned)
                    {
                        //animation
                        wasp.SetActive(true);
                    }
                }
                else if (phase == 1)
                {
                    timer += Time.deltaTime;
                    if (timer > fire_rate)
                    {
                        timer = 0f;
                        Fire();
                    }
                }
                else if (phase == 2)
                {
                    timer += Time.deltaTime;
                    if (timer >= spawn_rate)
                    {
                        timer = 0;
                        StartCoroutine(Summ());
                        SpawnSkeleton();
                    }
                    if (SunNumber == 7)
                    {
                        Death.SetActive(true);
                        //particellare
                        foreach (GameObject g in GameObject.FindGameObjectsWithTag("SpawnedSkeleton"))
                        {
                            g.SetActive(false);
                        }
                        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Sun"))
                        {
                            g.transform.GetChild(0).gameObject.SetActive(false);
                        }
                        dse.Open();
                        portal.SetActive(true);
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    IEnumerator Summ()
    {
        Summon.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Summon.SetActive(false);
    }

    IEnumerator Hit()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(.2f);
        sr.color = Color.white;
    }

    public void AddSun()
    {
        SunNumber++;
    }

    private void SpawnSkeleton()
    {
        GameObject go = ObjectPoolingManager.Instance.GetObject("ChasingScheletro");
        go.GetComponent<SummonedSkeleton>().bossfight = true;
        go.transform.position = new Vector3(UnityEngine.Random.Range(MinSpawn.transform.position.x, MaxSpawn.transform.position.x), MinSpawn.transform.position.y, -1);
    }

    private void Fire()
    {
        Vector3 pos = player.transform.position;
        GameObject go = ObjectPoolingManager.Instance.GetObject("Attack");
        if (go != null)
        {
            go.SetActive(false);
            go.transform.position = transform.position;
            go.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            if (!FightStarted)
            {
                dse.Close();
                FightStarted = true;
                GetComponent<EdgeCollider2D>().enabled = false;
            }
        }
        if (coll.tag == "Swarm" && phase == 0)
        {
            StartCoroutine(Hit());
            phase++;
            phase1.SetActive(false);
            phase2.SetActive(true);
            timer = 0;
            ClearPaint();
        }
        if (coll.tag == "Light" && coll.GetComponent<Attack>().canhitshaman && phase == 1)
        {
            StartCoroutine(Hit());
            phase++;
            phase2.SetActive(false);
            phase3.SetActive(true);
            SpawnSkeleton();
            ClearPaint();
            coll.gameObject.SetActive(false);
        }
    }

    public void ClearPaint()
    {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Scratch"))
        {
            g.GetComponent<Paint>().DisablePaintRoutine();
        }
    }
}
