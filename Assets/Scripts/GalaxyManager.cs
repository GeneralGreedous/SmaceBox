using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyManager : MonoBehaviour
{
    public static GalaxyManager Instance;


    [SerializeField] private List<GameObject> galaxySystemList;

    [SerializeField] private GameObject objectToSpawn;   // The prefab of the object you want to spawn.
    [SerializeField] private float spawnRadius = 5f;     // The radius within which the objects can be spawned.
    [SerializeField] private int numberOfObjects = 10;   // The number of objects to spawn.
    [SerializeField] private float minDistanceBetweenObjects = 0.5f; // The minimum distance between spawned objects.

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("GalaxyManager already on scene");
        }
        Instance = this;
    }
    private void BeforeStartNewGalaxy()
    {
        foreach (var item in galaxySystemList)
        {
            Destroy(item);
        }
        galaxySystemList = new List<GameObject>();
    }
    public void StartCreatingGalaxy()
    {
        BeforeStartNewGalaxy();
       
        for (int i = 0; i < numberOfObjects; i++)
        {
            SpawnRandomObject();
        }
    }

    private void SpawnRandomObject()
    {
        Vector3 randomPosition = GetRandomPoint();

        while (!IsPositionValid(randomPosition))
        {
            randomPosition = GetRandomPoint();
        }

        GameObject system = Instantiate(objectToSpawn, randomPosition, Quaternion.identity, transform);
        galaxySystemList.Add(system);
    }

    private Vector3 GetRandomPoint()
    {
        float randomAngle = Random.Range(0f, Mathf.PI * 2f);

        float randomDistance = Random.Range(0f, spawnRadius);

        Vector3 randomPosition = new Vector3(randomDistance * Mathf.Cos(randomAngle), randomDistance * Mathf.Sin(randomAngle), 0f);

        return randomPosition;
    }

    private bool IsPositionValid(Vector3 position)
    {
        // Check the distance between the new position and all existing spawned objects.
        GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag(objectToSpawn.tag);
        foreach (GameObject obj in spawnedObjects)
        {
            if (Vector3.Distance(obj.transform.position, position) < minDistanceBetweenObjects)
            {
                // If the distance is less than the minimum, the position is not valid.
                return false;
            }
        }

        // The position is valid if it's not too close to any other objects.
        return true;
    }

}
