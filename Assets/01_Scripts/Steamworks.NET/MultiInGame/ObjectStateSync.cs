using UnityEngine;
using Mirror;

public class ObjectStateSync : NetworkBehaviour
{
    // SyncVars pour synchroniser l'état des objets
    [SyncVar(hook = nameof(OnObject1StateChanged))]
    private bool isObject1Found;

    [SyncVar(hook = nameof(OnObject2StateChanged))]
    private bool isObject2Found;

    [SyncVar(hook = nameof(OnObject3StateChanged))]
    private bool isObject3Found;

    // Méthodes pour récupérer les objets
    public void FindObject(int objectID)
    {
        if (isServer)
        {
            SetObjectFound(objectID);
        }
        else
        {
            CmdFindObject(objectID);
        }
    }

    [Command]
    private void CmdFindObject(int objectID)
    {
        SetObjectFound(objectID);
    }

    [Server]
    private void SetObjectFound(int objectID)
    {
        switch (objectID)
        {
            case 1:
                isObject1Found = true;
                break;
            case 2:
                isObject2Found = true;
                break;
            case 3:
                isObject3Found = true;
                break;
        }
    }

    // Hooks pour mettre à jour l'état sur les clients
    private void OnObject1StateChanged(bool oldState, bool newState)
    {
        Debug.Log($"Object 1 found state changed to: {newState}");
        // Mettez à jour l'interface utilisateur ou d'autres composants ici
    }

    private void OnObject2StateChanged(bool oldState, bool newState)
    {
        Debug.Log($"Object 2 found state changed to: {newState}");
        // Mettez à jour l'interface utilisateur ou d'autres composants ici
    }

    private void OnObject3StateChanged(bool oldState, bool newState)
    {
        Debug.Log($"Object 3 found state changed to: {newState}");
        // Mettez à jour l'interface utilisateur ou d'autres composants ici
    }
}