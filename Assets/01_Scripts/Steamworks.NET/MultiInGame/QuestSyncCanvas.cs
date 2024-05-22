
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
   
    }

    private int i = 0;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Avant");
            i++;
            UpdateText("ouiii mais non" + i.ToString());
            Debug.Log("Apres");
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