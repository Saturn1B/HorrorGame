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


        }
        else//other
        {
            //playerUi.Add(SteamFriends.GetPersonaName());
            startGameButt.SetActive(false);

            Debug.Log(" I'm client ");


        }


    }



    public void AddPlayerName(string playerName)
    {
        playerUi.Add(playerName);
        UpdatePlayerNamesUI();
    }

    private void UpdatePlayerNamesUI()
    {
        // Mettre à jour l'interface utilisateur (UI) pour afficher les pseudonymes des joueurs
        Debug.Log("Player Names Updated: " + playerUi.Count);

        // Ici, vous pouvez appeler une fonction pour mettre à jour l'UI avec la liste des pseudonymes
        // UIManager.Instance.UpdatePlayerList(playerNames.ToList());
    }

    private void StartGameClicked()
    {
        //NetworkManager.singleton.ServerChangeScene("MultiScene");
        CustomNetworkManager.Instance.ActivateLoadedSceneManually();
        //NetworkServer.SendToReady()
    }
}
