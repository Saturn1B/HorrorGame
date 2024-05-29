using Mirror;
using UnityEngine;
using Steamworks;
using System;
using System.Collections.Generic;
using Telepathy;
using System.Collections;
using UnityEngine.SceneManagement;

public class SteamLobby : NetworkBehaviour
{
    private NetworkManager networkManager;

    public GameObject hostButton = null;

    public GameObject canvas;
    public GameObject canvasHud;

    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEntered;

    private const string HostAddressKey = "host";

    private bool isSceneLoaded = false; // Indique si la sc�ne de jeu est d�j� charg�e

    private SyncList<string> lobbyNames = new SyncList<string>();


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
        //SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, networkManager.maxConnections);
        if (SteamAPI.Init())
        {
            Debug.Log("Steam API initialized successfully.");
            if (networkManager == null)
                networkManager = GetComponent<NetworkManager>();

            if (networkManager != null)
            {
                try
                {
                    Debug.Log($"Attempting to create lobby with maxConnections: {networkManager.maxConnections}");
                    SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, networkManager.maxConnections);
                    Debug.Log("Lobby creation successful.");
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Exception while creating lobby: {ex.Message}\n{ex.StackTrace}");
                }
            }
            else
            {
                Debug.LogError("networkManager is null.");
            }
        }
        else
        {
            Debug.LogError("Steam API initialization failed.");
        }
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

        //StartCoroutine(Temp());
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

        if (isClient)
        {
            canvas.SetActive(false);
        }

        string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);
        networkManager.networkAddress = hostAddress;
        networkManager.StartClient();

        hostButton.SetActive(false);
        canvasHud.SetActive(false);


        //StartCoroutine(Temp());
    }

    public IEnumerator Temp()
    {
        yield return new WaitForSeconds(2f);

        StartCoroutine(PreloadGameSceneAsync());
    }

    // Coroutine pour pr�charger la sc�ne de jeu de mani�re asynchrone
    private IEnumerator PreloadGameSceneAsync()
    {
        // Chargement de la sc�ne de jeu de mani�re asynchrone
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MultiScene", LoadSceneMode.Additive);

        Debug.LogWarning("La sc�ne de jeu n'est pas encore charg�e !");
        // Attend que le chargement soit termin�
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // La sc�ne de jeu est charg�e
        Debug.LogWarning("La sc�ne de jeu est charg�e !");
        isSceneLoaded = true;
    }

    public void StartGame()
    {
        if (!isServer)
            return; // Assurez-vous que seul le serveur peut d�marrer la partie

        // Assurez-vous que la sc�ne de jeu est charg�e avant de d�marrer la partie
        if (isSceneLoaded)
        {
            NetworkManager.singleton.ServerChangeScene("MultiScene");
        }
        else
        {
            Debug.LogWarning("La sc�ne de jeu n'est pas encore charg�e !");
        }
    }


}
