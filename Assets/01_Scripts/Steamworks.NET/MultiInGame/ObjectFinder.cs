using Mirror;
using UnityEngine;

public class ObjectFinder : NetworkBehaviour
{
    [SerializeField] private int objectID; // Set this in the Inspector for each object (1, 2, or 3)
    public ObjectStateSync stateSync;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            stateSync.FindObject(objectID);
            gameObject.SetActive(false);
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
        stateSync.FindObject(objectID);
    }
}