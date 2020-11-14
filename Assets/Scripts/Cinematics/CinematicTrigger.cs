using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Saving;
namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        bool cinematicSequenceTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag.Equals("Player") && !cinematicSequenceTriggered) {
                cinematicSequenceTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
            
        }

        public object CaptureState()
        {
            return cinematicSequenceTriggered;
        }

        public void RestoreState(object state)
        {
            cinematicSequenceTriggered = (bool)state;
        }
    }
}