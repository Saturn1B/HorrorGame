using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestActive
{
    QuestA,
    QuestB,
    QuestC,
    QuestD
}

public class QuestAction : ItemObject
{
    [SerializeField]
    private QuestActive activeQuest;

    private Rigidbody rb;
    private bool thrown;    

    [SerializeField]
    private QuestSyncCanvas questSyncCanvas;

    public ObjectStateSync stateSync;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void QuestComplet()
    { 
        switch (activeQuest)
        {
            case QuestActive.QuestA:
                Debug.Log("A");
                //questSyncCanvas.QuestA();
                //questSyncCanvas.UpdateChanged(barrelid);
                stateSync.FindObject1();
                break;
            case QuestActive.QuestB:
                Debug.Log("B");
                stateSync.FindObject2();
                break;
            case QuestActive.QuestC:
                Debug.Log("C");
                stateSync.FindObject3();
                break;
            case QuestActive.QuestD:
                Debug.Log("D");
                break;


        }

    }
   
}
