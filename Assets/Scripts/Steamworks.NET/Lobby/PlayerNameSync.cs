using Mirror;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNameSync : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnDisplayNameChanged))]
    public string displayName;

    private CSteamID steamID;

    public TextMeshProUGUI textName;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        // Récupérer l'ID Steam du joueur local
        steamID = SteamUser.GetSteamID();
        // Récupérer le pseudonyme Steam du joueur local
        displayName = SteamFriends.GetPersonaName();

        // Mettre à jour le pseudonyme sur le serveur
        CmdChangeDisplayName(displayName);
    }

    [Command]
    private void CmdChangeDisplayName(string newName)
    {
        // Mettre à jour le pseudonyme sur le serveur
        displayName = newName;
        RpcUpdateDisplayName(newName);
    }

    [ClientRpc]
    private void RpcUpdateDisplayName(string newName)
    {
        // Mettre à jour le pseudonyme sur tous les clients
        displayName = newName;
        textName.text = newName;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        // Mettre à jour le pseudonyme affiché lorsque le joueur rejoint la partie
        OnDisplayNameChanged(displayName, displayName);
    }

    private void OnDisplayNameChanged(string oldName, string newName)
    {
        // Mettre à jour l'affichage du pseudonyme dans l'interface utilisateur
        Debug.Log("Display Name Changed: " + newName);
        textName.text = newName;
    }

    [Server]
    public void ChangeDisplayName(string newName)
    {
        // Vérifier si le joueur a les droits nécessaires pour changer de pseudonyme (à implémenter)
        // ...

        // Mettre à jour le pseudonyme sur le serveur
        displayName = newName;
    }
}