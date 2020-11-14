using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        
        [SerializeField]
        public float health = 100f;
        bool dead = false;

        public object CaptureState()
        {
            return health;
        }

        public bool isDead()
        {
            return dead;
        }

        public void RestoreState(object state)
        {
            float restoredHealth = (float)state;
            health = restoredHealth;
            if (health == 0) {
                DeathBehavior();
            }
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