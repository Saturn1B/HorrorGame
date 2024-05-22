
using UnityEngine;
using Mirror;
using TMPro;  // Assurez-vous d'avoir TextMeshPro si vous l'utilisez

public class QuestSyncCanvas : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI sharedTextA;  // Utilisez TextMesh si vous utilisez TextMeshPro
    [SerializeField] private TextMeshProUGUI sharedTextB;
    [SerializeField] private TextMeshProUGUI sharedTextC;
    [SerializeField] private TextMeshProUGUI sharedTextD;
    [SyncVar(hook = nameof(OnTextChanged))] private string syncedText;

    void Start()
    {
        if (sharedTextA == null)
        {
            Debug.LogError("SharedText is not assigned!");
            return;
        }

        UpdateText("Pitot");
   
    }

    public void QuestA()
    {
        UpdateText("Quest A Complete");
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
        sharedTextA.text = newText;
    }
}