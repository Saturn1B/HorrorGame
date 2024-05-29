using System.Collections.Generic;
using UnityEngine;

public class QuestSpawner : MonoBehaviour
{
    // Liste des points de spawn
    public List<Transform> spawnPoints;
    // Liste des prefabs des qu�tes
    public List<GameObject> questPrefabs;

    void Start()
    {
        SpawnQuests();
    }

    void SpawnQuests()
    {
        if (spawnPoints.Count < questPrefabs.Count)
        {
            Debug.LogError("Il n'y a pas assez de points de spawn pour toutes les qu�tes !");
            return;
        }

        // Liste des indices de points de spawn disponibles
        List<int> availableSpawnIndices = new List<int>();
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            availableSpawnIndices.Add(i);
        }

        // Pour chaque qu�te, choisir un point de spawn al�atoire
        foreach (var questPrefab in questPrefabs)
        {
            // S�lection al�atoire d'un index de point de spawn
            int randomIndex = Random.Range(0, availableSpawnIndices.Count);
            int spawnIndex = availableSpawnIndices[randomIndex];

            // Instantiation de la qu�te sur le point de spawn s�lectionn�
            Instantiate(questPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);

            // Suppression du point de spawn utilis� pour �viter les duplications
            availableSpawnIndices.RemoveAt(randomIndex);
        }
    }
}


/*private void Bottles()
    {
        List<Transform> availableBottlePos = new List<Transform>(bottlePos);

        int bottlesInstantiated = 0;

        while (bottlesInstantiated < bottleNumber && availableBottlePos.Count > 0)
        {
            int randomIndex = Random.Range(0, availableBottlePos.Count);
            Transform spawnTransform = availableBottlePos[randomIndex];

            // V�rifie si une bouteille existe d�j� � cette position
            if (!occupiedPositions.Contains(spawnTransform.position))
            {
                Instantiate(bottlePrefab, spawnTransform.position, Quaternion.identity);
                bottlesInstantiated++;
                occupiedPositions.Add(spawnTransform.position);
            }

            availableBottlePos.RemoveAt(randomIndex);
        }
    }
}*/
