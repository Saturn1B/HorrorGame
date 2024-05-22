using Mirror;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    public enum PlayerRole
    {
        Attacker,
        Defender,
        // Ajoutez plus de rôles au besoin
    }

    private Dictionary<NetworkConnectionToClient, PlayerRole> playerRoles = new Dictionary<NetworkConnectionToClient, PlayerRole>();
    private bool rolesAssigned = false;
    private bool temp = false;

    // End game
    public bool isEndGame = false;
    public GameObject endZone;

    // Start Game 
    public bool isStarted = false;
    public GameObject questCanvas;

    public void FixedUpdate()
    {
        if(!temp)//Check if all players are here & if is good go assign Roles
        {
            foreach (var connPair in NetworkServer.connections)
            {
                if (connPair.Value != null && !connPair.Value.isReady)
                {
                    Debug.Log("honon");
                    return;
                }
            }

            Debug.Log("ho oui ");

            if (SceneManager.GetActiveScene().name == "MultiScene" && !rolesAssigned)
            {
                AssignRoles();
            }

            temp = true;
        }
    }

    private void AssignRoles()
    {
        List<NetworkConnectionToClient> players = new List<NetworkConnectionToClient>();

        // Récupérer toutes les connexions des joueurs
        foreach (var connPair in NetworkServer.connections)
        {
            if (connPair.Value != null && connPair.Value.identity != null)
            {
                players.Add(connPair.Value);
            }
        }

        // Vérifier si au moins un joueur est présent
        if (players.Count == 0)
        {
            Debug.LogWarning("No players connected.");
            return;
        }

        // Choisir aléatoirement un joueur comme attaquant
        int attackerIndex = Random.Range(0, players.Count);
        NetworkConnectionToClient attackerConn = players[attackerIndex];

        // Attribuer le rôle d'attaquant à ce joueur
        playerRoles.Add(attackerConn, PlayerRole.Attacker);
        players.RemoveAt(attackerIndex); // Retirer l'attaquant de la liste des joueurs

        // Attribuer le rôle de défenseur aux autres joueurs
        foreach (var conn in players)
        {
            playerRoles.Add(conn, PlayerRole.Defender);
        }

        // Envoyer les informations sur les rôles à tous les clients
        foreach (var conn in NetworkServer.connections)
        {
            if (conn.Value != null && conn.Value.identity != null)
            {
                PlayerRole role = playerRoles[conn.Value];
                conn.Value.identity.GetComponent<Player>().RpcAssignRole(role);
            }
        }

        rolesAssigned = true;
    }
}