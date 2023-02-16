using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheeseSpawner : MonoBehaviour
{
    [SerializeField] int amountOfCheeseToSpawn = 4;
    [SerializeField] List<Transform> cheeseSpawnTransformList;
    [SerializeField] List<Transform> cheesePrefabList;
    List<Vector3> spawnPositionsList;

    void Start()
    {
        spawnPositionsList = new();
        foreach (Transform transform in cheeseSpawnTransformList)
        {
            spawnPositionsList.Add(transform.position);
        }

        List<Vector3> shuffledSpawnPositions = ShufflePositionsList(spawnPositionsList);
        SpawnCheese(shuffledSpawnPositions);
    }

    List<Vector3> ShufflePositionsList(List<Vector3> spawnPositionsList)
    {
        return spawnPositionsList.OrderBy(x => Random.value).ToList();
    }

    Transform GetRandomCheese()
    {
        int randomIndex = Random.Range(0, cheesePrefabList.Count);
        return cheesePrefabList[randomIndex];
    }


    void SpawnCheese(List<Vector3> shuffledSpawnPositions)
    {
        int spawnIndex = 0;

        for (int i = 0; i < amountOfCheeseToSpawn; i++)
        {
            if (spawnIndex >= shuffledSpawnPositions.Count)
            {
                Debug.Log("Not enough spawn points");
                break;
            }
            Instantiate(GetRandomCheese(), shuffledSpawnPositions[spawnIndex], Quaternion.identity);
            spawnIndex++;
        }
    }
}
