using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Movement;
using System.Net;
using System;
using UnityEngine.AI;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField]
        float chaseDistance = 5f;
        Fighter fight;
        Mover move;
        NavMeshAgent agent;
        GameObject player;
        Health health;
        Vector3 guardPosition;
        float timeSinceLastSawPlayer;
        [SerializeField]
        float dwellTime;
        float timeDwelled;
        [SerializeField]
        float suspicionTime;
        [SerializeField]
        PatrolPath patrolPath;
        [SerializeField]
        float wayPointTolearance = 1f;

        int currentWayPointIndex = 0;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            fight = GetComponent<Fighter>();
            health = GetComponent<Health>();
            move = GetComponent<Mover>();
            guardPosition = transform.position;
            timeSinceLastSawPlayer = Mathf.Infinity;
            dwellTime = 2f;
            timeDwelled = Mathf.Infinity;
            suspicionTime = 3f;
            agent = GetComponent<NavMeshAgent>();
        }
        void Update()
        {
            if (health.isDead()) return;

            if (InAttackRangeOfPlayer() && fight.CanAttack(player))
            {
                agent.speed = 4;
                AttackBehavior();
                timeSinceLastSawPlayer = 0;

            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                agent.speed = 2;
                PatrolBehavior();
            }
            timeSinceLastSawPlayer += Time.deltaTime;
            


        }

        private void PatrolBehavior()
        {
            print(gameObject.name + "return to guard position");
            Vector3 nextPosition = GetCurrentWayPoint();
            if (patrolPath != null) {

                if (AtWayPoint())
                {
                    if (timeDwelled < dwellTime)
                    {
                        DwellBehavior();
                    }
                    else
                    {
                        CycleWayPoint();
                        nextPosition = GetCurrentWayPoint();
                        move.StartMoveAction(nextPosition);
                        timeDwelled = 0;
                    }

                    timeDwelled += Time.deltaTime;
                }
                else
                {
                    move.StartMoveAction(nextPosition);
                }
                
            }
            
        }

        private void DwellBehavior()
        {
            SuspicionBehavior();
        }

        private Vector3 GetCurrentWayPoint()
        {
            return patrolPath.transform.GetChild(currentWayPointIndex).position;
        }

        private void CycleWayPoint()
        {
            currentWayPointIndex = (currentWayPointIndex + 1) % patrolPath.transform.childCount;
        }

        private bool AtWayPoint()
        {
            float distanceToWayPoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return distanceToWayPoint < wayPointTolearance;

        }

        private void AttackBehavior()
        {
            print(gameObject.name + " Should chase");
            fight.Attack(player);
        }

        private void SuspicionBehavior()
        {
            GetComponent<ActionScheduler>().CancelAction();
        }

        private bool InAttackRangeOfPlayer()
        {
            return DistanceToPlayer() < chaseDistance;
        }
        private float DistanceToPlayer()
        {

            return Vector3.Distance(player.transform.position, transform.position);
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,chaseDistance);
        }

    }
}
