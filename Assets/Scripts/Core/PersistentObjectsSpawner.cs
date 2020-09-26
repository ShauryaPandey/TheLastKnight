using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjectsSpawner : MonoBehaviour
{
    [SerializeField] GameObject persistentObjectPrefab;
    static bool hasSpawned = false;
    void Awake()
    {
        if (hasSpawned)
            return;
        SpawnPersistentObjects();
        hasSpawned = true;
        
    }

    private void SpawnPersistentObjects()
    {
        DontDestroyOnLoad(Instantiate<GameObject>(persistentObjectPrefab));
    }

    void Update()
    {
        
    }
}
