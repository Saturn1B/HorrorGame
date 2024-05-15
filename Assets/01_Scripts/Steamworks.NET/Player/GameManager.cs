using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public enum PlayerRole
    {
        Attacker,
        Defender,
        // Ajoutez plus de r�les au besoin
    }

    private Dictionary<NetworkConnectionToClient, PlayerRole> playerRoles = new Dictionary<NetworkConnectionToClient, PlayerRole>();

    public void AssignRoles()
    {
        // Assignez des r�les aux joueurs, par exemple al�atoirement
        List<PlayerRole> availableRoles = new List<PlayerRole> { PlayerRole.Attacker, PlayerRole.Defender };
        foreach (var connPair in NetworkServer.connections)
        {
            if (connPair.Value != null && connPair.Value.identity != null)
            {
                NetworkConnectionToClient conn = connPair.Value;
                PlayerRole randomRole = availableRoles[Random.Range(0, availableRoles.Count)];
                playerRoles.Add(conn, randomRole);
                availableRoles.Remove(randomRole);
                // Envoyez les informations sur le r�le au client
                conn.identity.GetComponent<Player>().RpcAssignRole(randomRole);
            }
        }
    }
}