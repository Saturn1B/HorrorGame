using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : NetworkBehaviour
{
    public GameObject cubePrefab;

    void Update()
    {
        // V�rifier si le joueur appuie sur la touche "E"
        if (isLocalPlayer && Input.GetKeyDown(KeyCode.E))
        {
            CmdSpawnCube();
        }
    }

    [Command] 
    void CmdSpawnCube()
    {
        // Appel� sur le serveur
        GameObject cube = Instantiate(cubePrefab, transform.position + transform.forward * 2f, Quaternion.identity);
        NetworkServer.Spawn(cube);
    }
}
