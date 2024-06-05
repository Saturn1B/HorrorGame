using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedScoreManager : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnScoreChanged))]
    private int sharedScore;

    void Update()
    {
        if (isClient && Input.GetKeyDown(KeyCode.I))
        {
            CmdAddScore();
        }
    }

    [Command]
    void CmdAddScore()
    {
        sharedScore += 1;
    }

    void OnScoreChanged(int oldScore, int newScore)
    {
        Debug.Log($"Score updated from {oldScore} to {newScore}");
        // Update UI or other relevant game objects here
    }

    void OnGUI()
    {
        GUILayout.Label($"Shared Score: {sharedScore}");
    }
}
