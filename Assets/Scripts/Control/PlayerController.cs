using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;
using RPG.Core;
using UnityEngine.AI;

namespace RPG.Control {
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        Mover moved;
        [SerializeField]
        Fighter fight;
        Health health;
       
        // Start is called before the first frame update
        void Start()
        {
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            if (health.isDead()) return;

            if (InteractWithCombat())
            {
                return;
            }
            if (InteractWithMovement()) {
                return;
            }
           
            print("Nothing to do");


        }

        private bool InteractWithCombat()
        {
            Ray ray = GetMouseRay();
            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach (RaycastHit hit in hits) {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                if (!fight.CanAttack(target.gameObject)) 
                    continue;
                
                if (Input.GetMouseButton(0)) {
                    fight.Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            Ray ray = GetMouseRay();
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                   moved.StartMoveAction(hit.point);
                   
                }
                return true;
            }
            return false;

        }
       
        

        private Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}
