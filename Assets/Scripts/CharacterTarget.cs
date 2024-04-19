using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using Mirror;
using Unity.Burst.CompilerServices;

public class CharacterTarget : NetworkBehaviour
{
	private Transform playerCamera;
	private PlayerInventory playerInventory;
	[SerializeField] private int playerReach;

	private void Start()
	{
		playerCamera = GetComponentInChildren<Camera>().transform;
		playerInventory = GetComponentInChildren<PlayerInventory>();
	}

	private void Update()
	{
		RaycastHit hit;

		if(Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, playerReach))
		{
			if (hit.transform.GetComponent<ItemObject>())
			{
				HUDManager.Instance.DiplayIndication($"pick up {hit.transform.GetComponent<ItemObject>().itemDescription.itemName}");
				if (Input.GetKeyDown(KeyCode.E))
				{
					if (playerInventory.AddItem(hit.transform.GetComponent<ItemObject>().itemDescription))
					{
                        Destroy(hit.transform.gameObject);

						CmdDestoy(hit.transform.gameObject);
                    }
						
				}
			}
			else
				HUDManager.Instance.HideIndication();
		}
		else
			HUDManager.Instance.HideIndication();
	}


	[Command]
    private void CmdDestoy(GameObject obj)
    {
        if (!isLocalPlayer)
		{
            Destroy(obj);
        }

		RpcDestroy(obj);
    }

	[ClientRpc]
    private void RpcDestroy(GameObject obj)
    {
        if (!isLocalPlayer)
        {
            Destroy(obj);
        }
    }
}
