using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool cinematicSequenceTriggered = false;
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag.Equals("Player") && !cinematicSequenceTriggered) {
                cinematicSequenceTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
            
        }
    }
}