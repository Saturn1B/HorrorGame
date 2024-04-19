using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : NetworkBehaviour
{
    public GameObject cubePrefab;

    void Update()
    {
        // Vérifier si le joueur appuie sur la touche "E" et est le joueur local
        if (isLocalPlayer && Input.GetKeyDown(KeyCode.E))
        {
            CmdSpawnCube();
        }
    }

    [Command]
    void CmdSpawnCube()
    {
        GameObject cube = Instantiate(cubePrefab, transform.position + transform.forward * 2f, Quaternion.identity);
        //NetworkServer.Spawn(cube);

        // Informer les clients de l'apparition du cube
        RpcSpawnCube(cube);

    }

    [ClientRpc]
    void RpcSpawnCube(GameObject cube)
    {
        Instantiate(cubePrefab, transform.position + transform.forward * 2f, Quaternion.identity);
        
    }
}
