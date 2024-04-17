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

        for (int i = 0; i < quests.Count; i++)
        {
            int randomIndex = Random.Range(0, quests.Count);

            Transform spawnTransform = availablePos[randomIndex];

            Instantiate(quests[i], spawnTransform.position, Quaternion.identity);

            availablePos.RemoveAt(randomIndex);
        }
    }
}