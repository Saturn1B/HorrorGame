using Mirror;
using UnityEngine;
using Steamworks;
using System;
using System.Collections.Generic;

public class SteamLobby : NetworkBehaviour
{
    private NetworkManager networkManager;

    public GameObject hostButton = null;

    

    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEntered;

    private const string HostAddressKey = "host";

    //public List<string> lobbyNames = new List<string>();

    
    public SyncList<string> lobbyNames = new SyncList<string>();


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

        //lobbyNames.Add(SteamFriends.GetPersonaName());
        /*string name = SteamFriends.GetPersonaName();
        lobbyNames.Add(name);*/
    }

    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);        
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        if(NetworkServer.active)
        {
            // Récupérer l'ID du lobby
            CSteamID lobbyId = new CSteamID(callback.m_ulSteamIDLobby);

            // Récupérer le nombre de membres dans le lobby
            int numMembers = SteamMatchmaking.GetNumLobbyMembers(lobbyId);

            // Parcourir tous les membres du lobby
            for (int i = 0; i < numMembers; i++)
            {
                // Récupérer l'ID Steam du membre
                CSteamID memberId = SteamMatchmaking.GetLobbyMemberByIndex(lobbyId, i);

                // Récupérer le pseudonyme du membre
                string memberName = SteamFriends.GetFriendPersonaName(memberId);

                // Ajouter le pseudonyme à la liste synchronisée (côté serveur)
                lobbyNames.Add(memberName);
            }
        }
        else if (NetworkClient.active)
        {
            // Rien à faire côté client dans cette méthode
            // Les données seront synchronisées automatiquement par Mirror
        }
        else
        {
            Debug.LogWarning("OnLobbyEntered called but neither NetworkServer nor NetworkClient are active.");
        }

        

        string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey); 
        networkManager.networkAddress = hostAddress;
        networkManager.StartClient();

        hostButton.SetActive(false);


        
    }
    

}
