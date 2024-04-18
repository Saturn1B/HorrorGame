using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : NetworkBehaviour
{
    public Camera playerCam;

    public void Start()
    {
        if (this.isLocalPlayer)
        {
            // Activate the camera attached to the player object
            Camera playerCamera = this.GetComponentInChildren<Camera>();
            if (playerCamera != null)
                playerCamera.enabled = true;

            playerCam = playerCamera;
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
