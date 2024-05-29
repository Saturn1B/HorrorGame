using System.Collections.Generic;
using UnityEngine;

public class QuestSpawner : MonoBehaviour
{
    public List<Transform> questsSpawnPoints;
    public List<GameObject> questPrefabs;

    public List <Transform> botlleSpawnPoints;
    public GameObject bottlePrefab;
    private int bottleQuantity;

    void Start()
    {
        SpawnQuests();
    }

    void SpawnQuests()
    {
        if (questsSpawnPoints.Count < questPrefabs.Count)
        {
            Debug.LogError("Il n'y a pas assez de points de spawn pour toutes les qu�tes !");
            return;
        }

        List<int> availableSpawnIndices = new List<int>();
        for (int i = 0; i < questsSpawnPoints.Count; i++)
        {
            availableSpawnIndices.Add(i);
        }

        foreach (var questPrefab in questPrefabs)
        {
            int randomIndex = Random.Range(0, availableSpawnIndices.Count);
            int spawnIndex = availableSpawnIndices[randomIndex];

            Instantiate(questPrefab, questsSpawnPoints[spawnIndex].position, questsSpawnPoints[spawnIndex].rotation);

            availableSpawnIndices.RemoveAt(randomIndex);
        }
    }

    private void SpawnBottles()
    {

    }


/*private void Bottles()
    {
        if (botlleSpawnPoints.Count < questPrefabs.Count)
        {
            Debug.LogError("Il n'y a pas assez de points de spawn pour toutes les qu�tes !");
            return;
        }

        List<int> availableSpawnIndices = new List<int>();
        for (int i = 0; i < botlleSpawnPoints.Count; i++)
        {
            availableSpawnIndices.Add(i);
        }

        foreach (var questPrefab in questPrefabs)
        {
            int randomIndex = Random.Range(0, availableSpawnIndices.Count);
            int spawnIndex = availableSpawnIndices[randomIndex];

            Instantiate(questPrefab, botlleSpawnPoints[spawnIndex].position, botlleSpawnPoints[spawnIndex].rotation);

            availableSpawnIndices.RemoveAt(randomIndex);
        }
    }*/
}
