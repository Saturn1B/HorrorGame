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
        // Exemple : D�marrer la transmission vocale lorsque le joueur appuie sur la touche V
        if (Input.GetKeyDown(KeyCode.V))
        {
            //voiceChatManager.StartTransmitting();
        }

        // Exemple : Arr�ter la transmission vocale lorsque le joueur rel�che la touche V
        if (Input.GetKeyUp(KeyCode.V))
        {
            //voiceChatManager.StopTransmitting();
        }
    }
}
