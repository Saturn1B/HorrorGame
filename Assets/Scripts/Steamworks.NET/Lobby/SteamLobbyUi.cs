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

            for (int i = 0; i < playerUi.Count; i++)
            {
                namePlayerTexts[i].text = playerUi[i];
            }
        }
        else//other
        {
            //playerUi.Add(SteamFriends.GetPersonaName());
            startGameButt.SetActive(false);

            Debug.Log(" I'm client ");

            for (int i = 0; i < playerUi.Count; i++)
            {
                namePlayerTexts[i].text = playerUi[i];
            }
        }

        
    }

    private void StartGameClicked()
    {
        NetworkManager.singleton.ServerChangeScene("MultiScene");
        
        //NetworkServer.SendToReady()
    }
}
