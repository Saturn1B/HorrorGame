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

    private bool isSceneLoaded = false; // Indique si la scène de jeu est déjà chargée

    private SyncList<string> lobbyNames = new SyncList<string>();


    private void Start()
    {
        Debug.LogError("Start SteamLobby");

        if (networkManager == null)
            networkManager = NetworkManager.singleton;

        Debug.LogError("Found networkManager.");


        if (!SteamManager.Initialized)
        {
            Debug.LogError("SteamManager is not initialized.");
            return;
        }
        Debug.LogError("SteamManager is initialized.");

        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);

        Debug.LogError("Callbacks are initialized.");
    }

    void Update()
	{
		// Ensure SteamAPI runs its internal updates
		SteamAPI.RunCallbacks();
	}

    //link to host button on main menu
	public void HostLobby()
    {
        //hostButton.SetActive(false);
        if (SteamAPI.Init())
        {
            Debug.Log("Steam API initialized successfully.");
            if (networkManager == null)
                networkManager = NetworkManager.singleton;

            //if (NetworkManager.singleton != null)
            //{
            //    Debug.LogError("found networkManager!");

                try
                {
                    Debug.LogError($"Attempting to create lobby with maxConnections: {NetworkManager.singleton.maxConnections}");
                    SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, NetworkManager.singleton.maxConnections);
                    Debug.LogError("Lobby creation successful.");
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Exception while creating lobby: {ex.Message}\n{ex.StackTrace}");
                }
            //}
            //else
            //{
            //    Debug.LogError("networkManager is null.");
            //}
        }
        else
        {
            Debug.LogError("Steam API initialization failed.");
        }
    }


    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        Debug.LogError("OnLobbyCreated callback triggered.");

        if (callback.m_eResult != EResult.k_EResultOK)
        {
            Debug.LogError("Failed to create lobby.");

            //hostButton.SetActive(true);
            return;
        }

        Debug.LogError("Lobby created successfully.");
        Debug.LogError($"Lobby ID: {callback.m_ulSteamIDLobby}");

        NetworkManager.singleton.StartHost();

        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey, SteamUser.GetSteamID().ToString());

        //StartCoroutine(Temp());
    }

    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        Debug.LogError($"Entered lobby: {callback.m_ulSteamIDLobby}");

        if (NetworkServer.active)
        {
            Debug.LogError("NetworkServer is active.");
            return;
        }

        if (isClient)
        {
            Debug.LogError("Client is active, setting canvas inactive.");
            canvas.SetActive(false);
        }

        string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);
        Debug.LogError($"Host address: {hostAddress}");
        NetworkManager.singleton.networkAddress = hostAddress;
        NetworkManager.singleton.StartClient();

        hostButton.SetActive(false);
        canvasHud.SetActive(false);


        //StartCoroutine(Temp());
    }

    public IEnumerator Temp()
    {
        yield return new WaitForSeconds(2f);

        StartCoroutine(PreloadGameSceneAsync());
    }

    // Coroutine pour précharger la scène de jeu de manière asynchrone
    private IEnumerator PreloadGameSceneAsync()
    {
        // Chargement de la scène de jeu de manière asynchrone
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MultiScene", LoadSceneMode.Additive);

        Debug.LogWarning("La scène de jeu n'est pas encore chargée !");
        // Attend que le chargement soit terminé
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // La scène de jeu est chargée
        Debug.LogWarning("La scène de jeu est chargée !");
        isSceneLoaded = true;
    }

    public void StartGame()
    {
        if (!isServer)
            return; // Assurez-vous que seul le serveur peut démarrer la partie

        // Assurez-vous que la scène de jeu est chargée avant de démarrer la partie
        if (isSceneLoaded)
        {
            NetworkManager.singleton.ServerChangeScene("MultiScene");
        }
        else
        {
            Debug.LogWarning("La scène de jeu n'est pas encore chargée !");
        }
    }


}
