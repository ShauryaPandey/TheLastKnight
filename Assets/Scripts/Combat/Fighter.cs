using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using UnityEngine.AI;


namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {
        [SerializeField]
        Health target;
        [SerializeField]
        float WeaponRange = 2f;
        [SerializeField]
        float timeBetweenAttack = 1.0f;
        float timeSinceLastAttack = Mathf.Infinity;
        [SerializeField]
        GameObject enemy;
        [SerializeField]
        float weaponDamage = 5f;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) { return; }
            if (target.isDead()) {
                return;
            }
            if (!GetIsInRange())
            {
                
                GetComponent<Mover>().MoveTo(target.transform.position);

            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttack)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("StopAttack");
            GetComponent<Animator>().SetTrigger("Attack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) <= WeaponRange;
           
        }

        public void Attack(GameObject combatTarget) {
           
            target = combatTarget.GetComponent<Health>();
            print("Attack!");
            GetComponent<ActionScheduler>().StartAction(this);
        }

        public bool CanAttack(GameObject combatTarget) {
            if (combatTarget == null)
                return false;
            if (combatTarget.GetComponent<Health>().isDead()) {
                return false;
            }
            return true;
        }

        public void Cancel() {
            target = null;
            GetComponent<Mover>().Cancel();
            StopAttackBehavior();
        }

        private void StopAttackBehavior() {
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("StopAttack");
            
        }
        //Animation Event
        void Hit()
        {
            if (target == null)
                return;
            target.TakeDamage(weaponDamage);
        }
    }

}