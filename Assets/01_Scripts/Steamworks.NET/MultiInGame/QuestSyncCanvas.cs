
using UnityEngine;
using Mirror;
using TMPro;  // Assurez-vous d'avoir TextMeshPro si vous l'utilisez

public class QuestSyncCanvas : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI sharedText;  // Utilisez TextMesh si vous utilisez TextMeshPro
    [SyncVar(hook = nameof(OnTextChanged))] private string syncedText;

    void Start()
    {
        if (sharedText == null)
        {
            Debug.LogError("SharedText is not assigned!");
            return;
        }

        UpdateText("Pitot");

        int i = 0;

        if (Input.GetKeyDown(KeyCode.K))
        {
            
            if (isLocalPlayer)
            {
                i++;
                UpdateText("ouiii mais non" + i);
            }

        }
       
    }

    public void UpdateText(string newText)
    {
        if (isServer)
        {
            syncedText = newText;
        }
        else
        {
            CmdUpdateText(newText);
        }
    }

    [Command]
    void CmdUpdateText(string newText)
    {
        syncedText = newText;
    }

    void OnTextChanged(string oldText, string newText)
    {
        sharedText.text = newText;
    }
}