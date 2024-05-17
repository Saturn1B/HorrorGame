using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] public List<Transform> questsPos = new List<Transform>();
    [SerializeField] public List<GameObject> quests = new List<GameObject>();
    //[SerializeField] public List<Transform> Torches = new List<Transform>();


    [SerializeField] public List<Transform> bottlePos = new List<Transform>();
    [SerializeField] public GameObject bottlePrefab;
    [SerializeField] int bottleNumber;

    private HashSet<Vector3> occupiedPositions = new HashSet<Vector3>();

    void Start()
    {
        Quests();
        Bottles();
    }

    private void Quests()
    {
        List<Transform> availablePos = new List<Transform>(questsPos);
        List<int> usedIndexes = new List<int>();

        foreach (GameObject quest in quests)
        {
            int randomIndex;
            Transform spawnTransform;

            do
            {
                randomIndex = Random.Range(0, availablePos.Count);
            } while (usedIndexes.Contains(randomIndex));

            spawnTransform = availablePos[randomIndex];
            usedIndexes.Add(randomIndex);

            Instantiate(quest, spawnTransform.position, Quaternion.identity);
        }
    }

    private void Bottles()
    {
        List<Transform> availableBottlePos = new List<Transform>(bottlePos);

        int bottlesInstantiated = 0;

        while (bottlesInstantiated < bottleNumber && availableBottlePos.Count > 0)
        {
            int randomIndex = Random.Range(0, availableBottlePos.Count);
            Transform spawnTransform = availableBottlePos[randomIndex];

            // Vérifie si une bouteille existe déjà à cette position
            if (!occupiedPositions.Contains(spawnTransform.position))
            {
                Instantiate(bottlePrefab, spawnTransform.position, Quaternion.identity);
                bottlesInstantiated++;
                occupiedPositions.Add(spawnTransform.position);
            }

            availableBottlePos.RemoveAt(randomIndex);
        }
    }
}
