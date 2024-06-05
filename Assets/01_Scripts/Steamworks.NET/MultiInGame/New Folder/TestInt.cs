using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInt : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnScoreChanged))]
    private int score;

    void Update()
    {
        if (isLocalPlayer && Input.GetKeyDown(KeyCode.I))
        {
            CmdAddScore();
        }
    }

    [Command]
    void CmdAddScore()
    {
        score += 1;
    }

    void OnScoreChanged(int oldScore, int newScore)
    {
        Debug.Log($"Score updated from {oldScore} to {newScore}");
        // Update UI or other relevant game objects here
    }

    void OnGUI()
    {
        if (isLocalPlayer)
        {
            GUILayout.Label($"Score: {score}");
        }
    }
}
