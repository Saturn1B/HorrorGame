using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceChatManager : MonoBehaviour
{
    private VoiceChatManager voiceChatManager;

    private void Start()
    {
        //voiceChatManager = VoiceChatManager.Instance;
    }

    private void Update()
    {
        // Exemple : Démarrer la transmission vocale lorsque le joueur appuie sur la touche V
        if (Input.GetKeyDown(KeyCode.V))
        {
            //voiceChatManager.StartTransmitting();
        }

        // Exemple : Arrêter la transmission vocale lorsque le joueur relâche la touche V
        if (Input.GetKeyUp(KeyCode.V))
        {
            //voiceChatManager.StopTransmitting();
        }
    }
}
