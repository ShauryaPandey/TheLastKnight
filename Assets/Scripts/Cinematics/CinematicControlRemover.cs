using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Control;
using RPG.Core;

namespace RPG.Cinematics { 
public class CinematicControlRemover : MonoBehaviour
{
        PlayableDirector playableDirector;
        GameObject player;
    void Start()
    {
           playableDirector =  GetComponent<PlayableDirector>();
            playableDirector.played += DisableControl;
            playableDirector.stopped += EnableControl;
    }

        private void EnableControl(PlayableDirector obj)
        {
            Debug.Log("Enabled Control");
            player.GetComponent<PlayerController>().enabled = true;

        }

        private void DisableControl(PlayableDirector obj)
        {
            Debug.Log("Disabled Control");
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<ActionScheduler>().CancelAction();
            player.GetComponent<PlayerController>().enabled = false;
        }
    }
}
