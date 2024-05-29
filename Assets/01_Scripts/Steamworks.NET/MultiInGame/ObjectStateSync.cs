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
    public void FindObject1()
    {
        if (isServer)
        {
            isObject1Found = true;
        }
        else
        {
            CmdFindObject1();
        }
    }

    [Command]
    private void CmdFindObject1()
    {
        isObject1Found = true;
    }

    public void FindObject2()
    {
        if (isServer)
        {
            isObject2Found = true;
        }
        else
        {
            CmdFindObject2();
        }
    }

    [Command]
    private void CmdFindObject2()
    {
        isObject2Found = true;
    }

    public void FindObject3()
    {
        if (isServer)
        {
            isObject3Found = true;
        }
        else
        {
            CmdFindObject3();
        }
    }

    [Command]
    private void CmdFindObject3()
    {
        isObject3Found = true;
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