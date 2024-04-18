using Mirror;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SteamLobbyUi : MonoBehaviour
{
    public List<string> playerUi = new List<string>();

    public GameObject startGameButt;

    private void Start()
    {
        if (NetworkServer.active)//host
        {
            startGameButt.SetActive(true);
            startGameButt.GetComponent<Button>().onClick.AddListener(StartGameClicked);
            playerUi.Add(SteamFriends.GetPersonaName());
            
        }
        else//other
        {
            playerUi.Add(SteamFriends.GetPersonaName());
            startGameButt.SetActive(false);
        }
    }

    private void StartGameClicked()
    {
        NetworkManager.singleton.ServerChangeScene("MultiScene");
        
        //NetworkServer.SendToReady()
    }
}
