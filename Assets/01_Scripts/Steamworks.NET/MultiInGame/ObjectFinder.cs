using UnityEngine;
using Mirror;

public class PlayerObjectFinder : NetworkBehaviour
{
    [SerializeField]
    private ObjectStateSync stateSync;
    [SerializeField]
    private int obj;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        //stateSync = FindObjectOfType<ObjectStateSync>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FindObject(obj);
        }
    }

    public void FindObject(int objectID)
    {
        if (isLocalPlayer)
        {
            CmdFindObject(objectID);
        }
    }

    [Command]
    private void CmdFindObject(int objectID)
    {
        if (stateSync != null)
        {
            stateSync.FindObject(objectID);
        }
        else
        {
            Debug.LogWarning("ObjectStateSync is not found.");
        }
    }
}