using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerRoles
{
    Crewmate = 0,
    Impostor
}

public class PlayerRole : NetworkBehaviour
{
    public static PlayerRole local;

    [SyncVar]
    protected internal PlayerRoles role = PlayerRoles.Crewmate;

    public void Start()
    {
        if (isLocalPlayer)
        {
            local = this;
        }

        if (GameManager.instance.gameObject != null)
        {
            GameManager.instance.AddPlayer(this);
        }
        
    }
}
