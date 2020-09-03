using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using RPG.Core;
namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        // Start is called before the first frame update
        [SerializeField]
        Transform TargetTransform;
        NavMeshAgent nav;
        Ray lastRay;
        void Start()
        {
            nav = GetComponent<NavMeshAgent>();

        }

        // Update is called once per frame
        void Update()
        {
            // if (Input.GetMouseButtonDown(0)) {
            //if (Input.GetMouseButton(0)) {  
            //MoveToCursor();
            //}
            UpdateAnimator();

        }


        public void Cancel() {
            nav.isStopped = true;
        }

       
        public void StartMoveAction(Vector3 destination) {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }
        public void MoveTo(Vector3 destination)
        {
            nav.destination = destination;
            nav.isStopped = false;
        }
       
        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }

        
    }
}