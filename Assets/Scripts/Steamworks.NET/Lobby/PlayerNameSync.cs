using Mirror;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNameSunc : NetworkBehaviour
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
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        // Mettre à jour le pseudonyme affiché lorsque le joueur rejoint la partie
        OnDisplayNameChanged(displayName, displayName);
    }

    private void OnDisplayNameChanged(string oldName, string newName)
    {
        // Mettre à jour l'affichage du pseudonyme dans l'interface utilisateur, par exemple
        //Debug.Log("Display Name Changed: " + newName);

        if (isLocalPlayer)
        {
            // Afficher le pseudonyme du joueur local
            Debug.Log("My Display Name: " + newName);
        }
        else
        {
            // Afficher le pseudonyme du joueur distant
            Debug.Log("Friend's Display Name: " + newName);
        }

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
