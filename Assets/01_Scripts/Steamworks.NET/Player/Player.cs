using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SyncVar]
    private GameManager.PlayerRole playerRole;

    // Méthode RPC pour attribuer le rôle côté client
    [ClientRpc]
    public void RpcAssignRole(GameManager.PlayerRole role)
    {
        playerRole = role;
        Debug.Log($"Role assigned: {role}");

        // Ici vous pouvez ajouter le code pour ajuster le gameplay en fonction du rôle
        // Par exemple, activer ou désactiver des fonctionnalités spécifiques au rôle
    }

    // Ajoutez d'autres méthodes ou propriétés selon les besoins

    // Exemple de méthode pour exécuter une action spécifique au rôle
    public void PerformRoleAction()
    {
        switch (playerRole)
        {
            case GameManager.PlayerRole.Attacker:
                Debug.Log("Performing attack action");
                // Ajoutez ici le code pour l'action d'attaque
                break;
            case GameManager.PlayerRole.Defender:
                Debug.Log("Performing defend action");
                // Ajoutez ici le code pour l'action de défense
                break;
            // Ajoutez d'autres cas pour les autres rôles si nécessaire
            default:
                Debug.LogWarning("Unknown player role");
                break;
        }
    }
}