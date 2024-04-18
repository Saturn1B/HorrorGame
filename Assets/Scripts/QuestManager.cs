using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] public List<Transform> positions = new List<Transform>();
    [SerializeField] public List<GameObject> quests = new List<GameObject>();

    void Start()
    {
        List<Transform> availablePos = new List<Transform>(positions);
        List<int> usedIndexes = new List<int>();

        foreach (GameObject quest in quests)
        {
            int randomIndex;
            Transform spawnTransform;

            do
            {
                randomIndex = Random.Range(0, availablePos.Count);
            }
            while (usedIndexes.Contains(randomIndex));

            spawnTransform = availablePos[randomIndex];
            usedIndexes.Add(randomIndex);

            Instantiate(quest, spawnTransform.position, Quaternion.identity);
        }
    }
}
