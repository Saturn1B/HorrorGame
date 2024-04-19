using Mirror;
using UnityEngine;
using Steamworks;
using System;
using System.Collections.Generic;

public class SteamLobby : NetworkBehaviour
{
    private NetworkManager networkManager;

    public GameObject hostButton = null;

    private CSteamID currentLobbyId;

    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEntered;

    private const string HostAddressKey = "host";

    public SteamLobbyUi steamLobbyUi;

    private void Start()
    {
        networkManager = GetComponent<NetworkManager>();

        if (!SteamManager.Initialized) { return; }

        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);

    }

    public void HostLobby()
    {
        hostButton.SetActive(false);
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, networkManager.maxConnections);
    }


    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK)
        {
            hostButton.SetActive(true);
            return;
        }

        networkManager.StartHost();

        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey, SteamUser.GetSteamID().ToString());

        steamLobbyUi = GameObject.Find("Canvas").GetComponent<SteamLobbyUi>(); 
        if (steamLobbyUi != null)
        {
            string name = SteamFriends.GetPersonaName();
            steamLobbyUi.playerUi.Add(name);
        }
    }

    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        if(NetworkServer.active)
        {
            return;
        }

        string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey); 
        networkManager.networkAddress = hostAddress;
        networkManager.StartClient();

        hostButton.SetActive(false);


        int numMembers = SteamMatchmaking.GetNumLobbyMembers(currentLobbyId);
        for (int i = 0; i < numMembers; i++)
        {
            CSteamID memberSteamId = SteamMatchmaking.GetLobbyMemberByIndex(currentLobbyId, i);
            string playerName = SteamFriends.GetFriendPersonaName(memberSteamId);
            Debug.Log("Steam Username: " + playerName);
        }
    }
    
}
