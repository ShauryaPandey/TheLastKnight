using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;

using RPG.Core;
namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction , ISaveable
    {
        [System.Serializable]
        struct MoverData {
            public SerializableVector3 position;
            public SerializableVector3 rotation;
        }
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

        public object CaptureState()
        {
            MoverData data = new MoverData();
            data.position = new SerializableVector3(transform.position);
            data.rotation = new SerializableVector3(transform.eulerAngles);
            return data;
        }

        public void RestoreState(object state)
        {
            MoverData data = (MoverData)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = data.position.ToVector();
            transform.eulerAngles = data.rotation.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;

        }
    }
}