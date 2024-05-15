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

    public void FixedUpdate()
    {
        if(!temp)
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

    public bool AreAllClientsReady()
    {
        

        return true;
    }

    private void AssignRoles()
    {
        List<PlayerRole> availableRoles = new List<PlayerRole> { PlayerRole.Defender, PlayerRole.Defender }; // Deux rôles de défenseurs
        bool attackerAssigned = false;

        foreach (var connPair in NetworkServer.connections)
        {
            if (connPair.Value != null && connPair.Value.identity != null)
            {
                NetworkConnectionToClient conn = connPair.Value;
                PlayerRole role;

                if (!attackerAssigned)
                {
                    role = PlayerRole.Attacker;
                    attackerAssigned = true;
                }
                else
                {
                    role = availableRoles[Random.Range(0, availableRoles.Count)];
                    availableRoles.Remove(role);
                }

                playerRoles.Add(conn, role);
                // Envoyez les informations sur le rôle au client
                conn.identity.GetComponent<Player>().RpcAssignRole(role);
            }
        }

        rolesAssigned = true;
    }
}