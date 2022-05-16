using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IKillable
{
    
    public float AttackCooldown = 1f;
    protected float timeOfLastAttack;

    public enum States { Idle, Alerted, }

    public States enemyState = States.Idle;

    protected NavMeshAgent agent;

    [SerializeField] protected List<Transform> waypoints = new List<Transform>();
    [SerializeField] protected float eyeSight = 10f;
    protected int currentWaypoint = 0;
    
    protected GameObject player;
    protected IKillable playerDmg;

    protected bool playerDetected = false;

    public float attackRange = 5f;
    protected float timeUntillStopsAlert = 4f;
    protected bool lostPlayer = false;
    protected float timeOfStart;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerDmg = (IKillable)player.GetComponentInParent(typeof(IKillable));
    }

    protected virtual void Start()
    {
        agent.destination = waypoints[0].position;
    }

    protected virtual void Update()
    {
        Detection();

        switch (enemyState)
        {
            case States.Idle: Idle(); break;
            case States.Alerted: Alerted(); break;
            default: break;
        }
    }

    protected virtual void Detection()
    {
        // Is the player even in range?
        if (!(DistanceHorizontalOnly(transform.position, player.transform.position) <= eyeSight))
        {
            playerDetected = false;
            return;
        }

        // Is the player in front for the enemy?
        if (!(Vector3.Angle(transform.forward, player.transform.position - transform.position) < 90))
        {
            playerDetected = false;
            return;
        }

        playerDetected = true;
    }

    protected virtual void Alerted()
    {
        // If we lose the player
        if (!playerDetected)
        {
            if (!lostPlayer)
            {
                lostPlayer = true;
                timeOfStart = Time.time;
                return;
            }

        }
        else
        {
            if (lostPlayer)
            {
                if (Time.time - timeOfStart > timeUntillStopsAlert)
                {
                    enemyState = States.Idle;
                    lostPlayer = false;
                    return;
                }
            }
        }

        // Chase player
        agent.destination = player.transform.position;

        // How close are we?? within range of attack?
        if (DistanceHorizontalOnly(transform.position, player.transform.position) <= attackRange)
        {

            if (Time.time - timeOfLastAttack > AttackCooldown)
            {
                timeOfLastAttack = Time.time;
                OnAttack();
            }
        }
    }

    protected virtual void OnAttack()
    {

    }

    protected virtual void Idle()
    {
        if (playerDetected) { enemyState = States.Alerted; }

        if (DistanceHorizontalOnly(agent.destination, transform.position) <= .12f)
        {
            currentWaypoint = Cycle(waypoints.Count, currentWaypoint, true);
            agent.destination = waypoints[currentWaypoint].position;
        }

    }

    protected virtual float DistanceHorizontalOnly(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x - b.x, 0, a.z - b.z).magnitude;
    }
    protected virtual int Cycle(int listLength, int currentVal, bool changePos)
    {
        if (changePos)
        {
            if (currentVal + 1 >= listLength)
                return 0;

            return currentVal + 1;
        }
        else
        {
            if (currentVal - 1 < 0)
                return listLength - 1;
            return currentVal - 1;
        }
    }


    public virtual void DealDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public virtual void Kill()
    {
        throw new System.NotImplementedException();
    }


}
