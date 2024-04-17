using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] public List<Transform> questPos = new List<Transform>();
    [SerializeField] public List<GameObject> questList = new List<GameObject>();

    void Start()
    {
        List<Transform> availablePos = new List<Transform>(questPos);
        for (int i = 0; i < questList.Count; i++)
        {
            int randomIndex = Random.Range(0, questPos.Count);

            Transform spawnTransform = availablePos[randomIndex];

            Instantiate(questList[i], spawnTransform.position, Quaternion.identity);

            availablePos.RemoveAt(randomIndex);
        }
    }
}