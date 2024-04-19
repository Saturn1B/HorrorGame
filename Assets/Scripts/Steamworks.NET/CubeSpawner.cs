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
        // Appelé sur le serveur
        GameObject cube = Instantiate(cubePrefab, transform.position + transform.forward * 2f, Quaternion.identity);

        // Synchroniser l'apparition du cube sur le réseau
        NetworkServer.Spawn(cube);

        // Informer les clients de l'apparition du cube
        RpcSpawnCube(cube);
    }

    [ClientRpc]
    void RpcSpawnCube(GameObject cube)
    {
        // Appelé sur tous les clients
        // Assurer que le cube instancié est synchronisé sur le réseau
        cube.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    }
}
