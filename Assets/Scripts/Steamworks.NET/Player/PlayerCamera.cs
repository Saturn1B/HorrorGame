using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCamera : NetworkBehaviour
{
    public Camera playerCam;
    public GameObject canvas;

    public void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "MultiLobby")
        {
            if (this.isLocalPlayer)
            {           
                Camera playerCamera = this.GetComponentInChildren<Camera>();
                if (playerCamera != null)
                    playerCamera.enabled = false;

                canvas.SetActive(false);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Camera playerCamera = this.GetComponentInChildren<Camera>();
                if (playerCamera != null)
                    playerCamera.enabled = false;

                canvas.SetActive(false);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            
        }
        else
        {
            if (this.isLocalPlayer)
            {
                // Activate the camera attached to the player object
                Camera playerCamera = this.GetComponentInChildren<Camera>();
                if (playerCamera != null)
                    playerCamera.enabled = true;

                playerCam = playerCamera;
                canvas.SetActive(true);
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

}
