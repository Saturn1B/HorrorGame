using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CharacterTarget : MonoBehaviour
{
	private Transform playerCamera;
	private PlayerInventory playerInventory;
	private CharacterMovement characterMovement;
	[SerializeField] private int playerReach;

	private void Start()
	{
		playerCamera = GetComponentInChildren<Camera>().transform;
		playerInventory = GetComponentInChildren<PlayerInventory>();
		characterMovement = GetComponent<CharacterMovement>();
	}

	private void Update()
	{
		if (characterMovement.GetPlayerState() == PlayerState.RAT) return;

		RaycastHit hit;

		if(Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, playerReach))
		{
			if (hit.transform.GetComponent<ItemObject>())
			{
				HUDManager.Instance.DiplayIndication($"pick up {hit.transform.GetComponent<ItemObject>().itemDescription.itemName}");
				if (Input.GetKeyDown(KeyCode.E))
				{
					if (playerInventory.AddItem(hit.transform.GetComponent<ItemObject>().itemDescription))
						Destroy(hit.transform.gameObject);
				}
			}
			else
				HUDManager.Instance.HideIndication();
		}
		else
			HUDManager.Instance.HideIndication();
	}
}
