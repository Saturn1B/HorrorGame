using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SyncVar]
    private GameManager.PlayerRole playerRole;

    // M�thode RPC pour attribuer le r�le c�t� client
    [ClientRpc]
    public void RpcAssignRole(GameManager.PlayerRole role)
    {
        playerRole = role;
        Debug.Log($"Role assigned: {role}");

        // Ici vous pouvez ajouter le code pour ajuster le gameplay en fonction du r�le
        // Par exemple, activer ou d�sactiver des fonctionnalit�s sp�cifiques au r�le
    }

    // Ajoutez d'autres m�thodes ou propri�t�s selon les besoins

    // Exemple de m�thode pour ex�cuter une action sp�cifique au r�le
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
                // Ajoutez ici le code pour l'action de d�fense
                break;
            // Ajoutez d'autres cas pour les autres r�les si n�cessaire
            default:
                Debug.LogWarning("Unknown player role");
                break;
        }
    }
}