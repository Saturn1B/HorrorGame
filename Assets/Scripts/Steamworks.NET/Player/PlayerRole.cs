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
    [SyncVar]
    protected internal PlayerRoles role = PlayerRoles.Crewmate;

    public void Start()
    {
        GameManager.instance.AddPlayer(this);
    }
}
