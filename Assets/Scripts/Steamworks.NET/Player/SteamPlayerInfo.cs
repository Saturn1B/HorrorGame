using Mirror;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamPlayerInfo : NetworkBehaviour
{
    private void Start()
    {
        if (SteamManager.Initialized)
        {
            string playerName = SteamFriends.GetPersonaName();
            Debug.Log("Player Name: " + playerName);

            CmdSendPlayerNameToServer(playerName);
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
        // Appelle la fonction pour mettre à jour le pseudonyme côté client
        SteamLobbyUi.instance.AddPlayerName(playerName);
    }
}
