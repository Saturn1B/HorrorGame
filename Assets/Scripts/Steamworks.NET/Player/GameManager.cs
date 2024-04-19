using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StartGameMessage : NetworkMessage
{

}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this; 
    }

    public static Action OnGameStarted;

    private List<PlayerRole> playerlist = new List<PlayerRole>();



    IEnumerator Start() 
    {
        NetworkClient.RegisterHandler<StartGameMessage>(OnStartGameMessage);

        while (!NetworkServer.active)
        {
            yield return null;
        }

        while(playerlist.Count != NetworkServer.connections.Count || playerlist.TrueForAll(x => x.connectionToClient.isReady))
        {
            yield return null;  
        }

        int rand = UnityEngine.Random.Range(0, playerlist.Count);

        playerlist[rand].role = PlayerRoles.Impostor;

        NetworkServer.SendToReady(new StartGameMessage());
    }

    public void AddPlayer(PlayerRole player)
    {
        playerlist.Add(player); 
    }

    private void OnStartGameMessage(StartGameMessage msg)//En gros ici la fonction pour le start de la game apres le check que tt les joeueur sont bien co 
    {
        OnGameStarted?.Invoke();    
    }
}
