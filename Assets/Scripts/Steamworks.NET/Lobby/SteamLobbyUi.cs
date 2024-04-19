using Mirror;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SteamLobbyUi : MonoBehaviour
{
    public List<string> playerUi = new List<string>();
    public List<TextMeshProUGUI> namePlayerTexts;    

    public GameObject startGameButt;

    private CSteamID currentLobbyId;

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

        for (int i = 0; i < playerUi.Count; i++)
        {
            namePlayerTexts[i].text = playerUi[i];
        }
    }

    private void StartGameClicked()
    {
        NetworkManager.singleton.ServerChangeScene("MultiScene");
        
        //NetworkServer.SendToReady()
    }
}
