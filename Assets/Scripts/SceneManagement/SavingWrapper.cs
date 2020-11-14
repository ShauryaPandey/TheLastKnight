using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using System;
using RPG.SceneManagment;

public class SavingWrapper : MonoBehaviour
{
    const string defaultSaveFile = "save";
    [SerializeField] float fadeinTime = 1f;
    private IEnumerator Start()
    {
        Fader fader = FindObjectOfType<Fader>();
        fader.FadeOutImmediate();
        yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
        yield return fader.FadeIn(fadeinTime);
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
       if (Input.GetKeyDown(KeyCode.S)) {
            Save();
                }
    }

    public void Save()
    {
        GetComponent<SavingSystem>().Save(defaultSaveFile);
    }

    public void Load()
    {
        GetComponent<SavingSystem>().Load(defaultSaveFile);
    }
}
