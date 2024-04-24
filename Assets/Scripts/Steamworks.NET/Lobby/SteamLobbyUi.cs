using Mirror;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SteamLobbyUi : NetworkBehaviour
{
    public static SteamLobbyUi instance;

    public List<string> playerUi = new List<string>();

    public List<TextMeshProUGUI> namePlayerTexts;    

    public GameObject startGameButt;

    private void Start()
    {
        if (NetworkServer.active)//host
        {
            
            startGameButt.SetActive(true);
            startGameButt.GetComponent<Button>().onClick.AddListener(StartGameClicked);
            playerUi.Add(SteamFriends.GetPersonaName());

            Debug.Log(" I'm host");

            if (SteamManager.Initialized)
            {
                string playerName = SteamFriends.GetPersonaName();
                Debug.Log("Player Name: " + playerName);

                CmdSendPlayerNameToServer(playerName);
            }
        }
        else//other
        {
            //playerUi.Add(SteamFriends.GetPersonaName());
            startGameButt.SetActive(false);

            Debug.Log(" I'm client ");

            if (SteamManager.Initialized)
            {
                string playerName = SteamFriends.GetPersonaName();
                Debug.Log("Player Name: " + playerName);

                CmdSendPlayerNameToServer(playerName);
            }
        }

        
    }

    [Command]
    public void CmdSendPlayerNameToServer(string playerName)
    {
        RpcReceivePlayerNameFromServer(playerName);
    }


    [ClientRpc]
    public void RpcReceivePlayerNameFromServer(string playerName)
    {
        // Appelle la fonction pour mettre � jour le pseudonyme c�t� client
        AddPlayerName(playerName);
    }

    public void AddPlayerName(string playerName)
    {
        playerUi.Add(playerName);
        UpdatePlayerNamesUI();
    }

    private void UpdatePlayerNamesUI()
    {
        // Mettre � jour l'interface utilisateur (UI) pour afficher les pseudonymes des joueurs
        Debug.Log("Player Names Updated: " + playerUi.Count);

        // Ici, vous pouvez appeler une fonction pour mettre � jour l'UI avec la liste des pseudonymes
        // UIManager.Instance.UpdatePlayerList(playerNames.ToList());
    }

    private void StartGameClicked()
    {
        NetworkManager.singleton.ServerChangeScene("MultiScene");
        
        //NetworkServer.SendToReady()
    }
}
