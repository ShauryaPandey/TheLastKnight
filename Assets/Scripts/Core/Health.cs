using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        
        [SerializeField]
        public float health = 100f;
        bool dead = false;

        public bool isDead()
        {
            return dead;
        }
        public void TakeDamage(float damage)
        {

            health = Mathf.Max(0, health - damage);
            print(health);
            if (health == 0 && !dead)
            {
                //  Destroy(GetComponent<CapsuleCollider>());
                // Destroy(GetComponent<NavMeshAgent>());
                
                print("Dead");
                DeathBehavior();
                GetComponent<ActionScheduler>().CancelAction();
                GetComponent<NavMeshAgent>().enabled = false;
            }
        }

        private void DeathBehavior()
        {
            GetComponent<Animator>().SetTrigger("Die");
            dead = true;
        }

        
    }

}