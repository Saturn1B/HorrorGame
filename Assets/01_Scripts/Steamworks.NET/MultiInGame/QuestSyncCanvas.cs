using UnityEngine;
using Mirror;
using TMPro;  // Assurez-vous d'avoir TextMeshPro si vous l'utilisez

public class QuestSyncCanvas : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI sharedTextA;
    [SerializeField] private TextMeshProUGUI sharedTextB;
    [SerializeField] private TextMeshProUGUI sharedTextC;
    [SerializeField] private TextMeshProUGUI sharedTextD;

    [SyncVar(hook = nameof(OnTextChangedA))] private string syncedTextA;
    [SyncVar(hook = nameof(OnTextChangedB))] private string syncedTextB;
    [SyncVar(hook = nameof(OnTextChangedC))] private string syncedTextC;
    [SyncVar(hook = nameof(OnTextChangedD))] private string syncedTextD;

    [SyncVar(hook = nameof(OnValueChanged))] private int syncedValue;


    public bool questAfinish = false;

    void Start()
    {
        if (sharedTextA == null || sharedTextB == null || sharedTextC == null || sharedTextD == null)
        {
            Debug.LogError("One or more SharedText fields are not assigned!");
            return;
        }

        UpdateTextA("Find 3 barrels");
        UpdateTextB("Find 10 barrels");
        UpdateTextC("Find 650 barrels");
        UpdateTextD("Find 10000 barrels");
    }

    public void QuestA()
    {

        if (syncedValue == 3)
        {
            UpdateTextA("Quest A Complete");
        }
        
    }

    /// <summary>
    ///                 syncedValue
    /// </summary>

    public void UpdateChanged(int newText)
    {
        if (isServer)
        {
            syncedValue = newText;
        }
        else
        {
            CmdUpdateInt(newText);
        }
    }

    [Command]
    void CmdUpdateInt(int newText)
    {
        syncedValue = newText;
    }

    void OnValueChanged(int oldText, int newText)
    {
        syncedValue = newText;
    }

    /// <summary>
    /// 
    /// </summary>


    public void EndGame()
    {
        sharedTextB.gameObject.SetActive(false);
        sharedTextC.gameObject.SetActive(false);
        sharedTextD.gameObject.SetActive(false);

        UpdateTextA("You need to go back to the boat");
    }

    public void UpdateTextA(string newText)
    {
        if (isServer)
        {
            syncedTextA = newText;
        }
        else
        {
            CmdUpdateTextA(newText);
        }
    }

    [Command]
    void CmdUpdateTextA(string newText)
    {
        syncedTextA = newText;
    }

    void OnTextChangedA(string oldText, string newText)
    {
        sharedTextA.text = newText;
    }

    // Repeat for sharedTextB
    public void UpdateTextB(string newText)
    {
        if (isServer)
        {
            syncedTextB = newText;
        }
        else
        {
            CmdUpdateTextB(newText);
        }
    }

    [Command]
    void CmdUpdateTextB(string newText)
    {
        syncedTextB = newText;
    }

    void OnTextChangedB(string oldText, string newText)
    {
        sharedTextB.text = newText;
    }

    // Repeat for sharedTextC
    public void UpdateTextC(string newText)
    {
        if (isServer)
        {
            syncedTextC = newText;
        }
        else
        {
            CmdUpdateTextC(newText);
        }
    }

    [Command]
    void CmdUpdateTextC(string newText)
    {
        syncedTextC = newText;
    }

    void OnTextChangedC(string oldText, string newText)
    {
        sharedTextC.text = newText;
    }

    // Repeat for sharedTextD
    public void UpdateTextD(string newText)
    {
        if (isServer)
        {
            syncedTextD = newText;
        }
        else
        {
            CmdUpdateTextD(newText);
        }
    }

    

    [Command]
    void CmdUpdateTextD(string newText)
    {
        syncedTextD = newText;
    }

    void OnTextChangedD(string oldText, string newText)
    {
        sharedTextD.text = newText;
    }
}