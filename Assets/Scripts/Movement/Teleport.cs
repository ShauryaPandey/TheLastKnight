using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Teleport : MonoBehaviour
    {
        NavMeshAgent agent;
        
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

       
    
       public void TeleportTo(Vector3 destination) {
            agent.Warp(destination);
        }
    }
}
