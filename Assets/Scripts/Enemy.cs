using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public ParticleSystem blood;
    public HealthBar healthBar;
    
    private Transform player;
    private NavMeshAgent agent;
    
    private float smooth = 1f;
    private int Health = 100;
    private GameManager gm;
    private Vector3 InitPos;
    private Quaternion InitRot;
    
    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance.transform;
        gm = Manager.Instance.manager;
        InitPos = transform.position;
        InitRot = transform.rotation;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.IsGameEnded)
        {
            /*
            transform.position = Vector3.MoveTowards(transform.position, player.position,
                smooth * Time.deltaTime);
            transform.localRotation = Quaternion.LookRotation(player.position, Vector3.up);
        */
            agent.SetDestination(player.position);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Equals("Bullet"))
        {
            blood.Play();
            BulletHit();
        }
    }

    void BulletHit()
    {
        // decrease health
        if (Health > 0)
        {
            Health -= 35;
            healthBar.SetSlider(Health);
        }
        else
        {
            gm.GameWin();
            gameObject.SetActive(false);
        }
    }

    public void Reset()
    {
        transform.position = InitPos;
        transform.rotation = InitRot;
    }
}
