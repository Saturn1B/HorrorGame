using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAction : ItemObject
{
    private Rigidbody rb;
    private bool thrown;

    [SerializeField]
    private QuestSyncCanvas questSyncCanvas;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void QuestComplet()
    {
        questSyncCanvas.QuestA();
    }
   
}
