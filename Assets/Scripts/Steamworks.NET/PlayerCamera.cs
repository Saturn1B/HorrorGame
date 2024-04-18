using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : NetworkBehaviour
{
    //public Camera camera;

    public void Start()
    {
        //camera = Camera.main;

        if (this.isLocalPlayer)
        {
            // Activate the camera attached to the player object
            Camera playerCamera = this.GetComponentInChildren<Camera>();
            if (playerCamera != null)
                playerCamera.enabled = true;
        }
        else
        {
            // Disable camera for other players
            Camera playerCamera = this.GetComponentInChildren<Camera>();
            if (playerCamera != null)
                playerCamera.enabled = false;
        }
    }

}
