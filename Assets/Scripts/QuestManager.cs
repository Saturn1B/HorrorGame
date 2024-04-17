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
            // Vérifie s'il reste des positions disponibles
            if (availablePos.Count == 0)
            {
                Debug.LogWarning("Il n'y a plus de positions disponibles pour instancier les quêtes.");
                break; // Sort de la boucle si toutes les positions ont été utilisées
            }

            int randomIndex;
            Transform spawnTransform;

            // Trouve un index non utilisé
            do
            {
                randomIndex = Random.Range(0, availablePos.Count);
            }
            while (usedIndexes.Contains(randomIndex));

            spawnTransform = availablePos[randomIndex];
            usedIndexes.Add(randomIndex);

            // Instancie la quête
            Instantiate(quest, spawnTransform.position, Quaternion.identity);
        }
    }
}
