using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Core
{
    public class PersistentObjectsSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectsPrefab;
        // Start is called before the first frame update
        public static bool hasSpawned = false;
        public void Awake()
        {
            if (hasSpawned) return;
            SpawnPersistentObject();
            hasSpawned = true;
        }
        void SpawnPersistentObject()
        {
            GameObject persistentObjects = Instantiate(persistentObjectsPrefab);
            DontDestroyOnLoad(persistentObjects);
        }
    }
}

