using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagment {
    public class Portal : MonoBehaviour
    {
        enum Destination {A,B,C};

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] Destination destination;
        [SerializeField] float fadeOutTime = 0.5f;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float fadeWaitTime = 0.5f;
        SavingWrapper saving;


        private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Player")) {
                
                StartCoroutine(Transition());
        }
    }

     

        private IEnumerator Transition() {
            Fader fader = FindObjectOfType<Fader>();
            saving = FindObjectOfType<SavingWrapper>();
            DontDestroyOnLoad(gameObject);
            yield return fader.FadeOut(fadeOutTime);
            saving.Save();
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            saving.Load();
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            saving.Save();
            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);
            Destroy(gameObject);
            
        }

        private void UpdatePlayer(Portal otherPortal)
        {
           GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        private Portal GetOtherPortal()
        {
         
            foreach (Portal portal in FindObjectsOfType<Portal>()) {
                if (portal == this) continue;
                if (portal.destination != this.destination) continue;
                return portal;
            }
            return null;
        }
    }
} 
