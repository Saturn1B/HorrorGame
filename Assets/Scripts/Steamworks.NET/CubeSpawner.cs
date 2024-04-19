using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : NetworkBehaviour
{
    public GameObject cubePrefab;

    void Update()
    {
        // V�rifier si le joueur appuie sur la touche "E" et est le joueur local
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

        // Synchroniser l'apparition du cube sur le r�seau
        NetworkServer.Spawn(cube);

        // Informer les clients de l'apparition du cube
        RpcSpawnCube(cube);
    }

    [ClientRpc]
    void RpcSpawnCube(GameObject cube)
    {
        // Appel� sur tous les clients
        // Assurer que le cube instanci� est synchronis� sur le r�seau
        cube.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    }
}
