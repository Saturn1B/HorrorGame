using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyPlayers : NetworkBehaviour
{
    public static LobbyPlayers Instance;

    public GameObject[] players; //0 == host    1 2 3 == client
    public TextMeshProUGUI[] playersName;

    public void Awake()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].SetActive(false);
        }
    }


}
