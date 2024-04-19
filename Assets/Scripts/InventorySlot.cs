using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Mirror;

public class InventorySlot : NetworkBehaviour
{
	private Image itemImage;
	private TMP_Text itemNumberText;
	private int itemNumber;
	[HideInInspector] public ItemScriptable itemDescription;

	public Camera playerCAm;

	private void Start()
	{
		itemImage = transform.GetChild(0).GetComponent<Image>();
		itemNumberText = transform.GetComponentInChildren<TMP_Text>();
		itemImage.gameObject.SetActive(false);
		itemNumberText.gameObject.SetActive(false);
        
    }

	public void SetItem(ItemScriptable itemScriptable)
	{
		itemDescription = itemScriptable;
		itemImage.gameObject.SetActive(true);
		itemImage.sprite = itemDescription.itemSprite;
		AddItemNumber(1);
	}

	public void UseItem()
	{
		if (itemDescription == null) return;

		AddItemNumber(-1);

		Vector3 instantiatePos = playerCAm.transform.position + playerCAm.transform.forward;
		Quaternion instantiateRot = playerCAm.transform.parent.transform.rotation;
		GameObject item = Instantiate(itemDescription.itemPrefab, instantiatePos, instantiateRot);
		item.GetComponent<ItemObject>().Use();

        CmdSpawn(item);

        if (itemNumber <= 0)
		{
			itemDescription = null;
			itemImage.gameObject.SetActive(false);
		}
	}

    [Command]
    private void CmdSpawn(GameObject itemPrefab)
    {
        NetworkServer.Spawn(itemPrefab); // Synchroniser l'objet sur tous les clients
    }

    [ClientRpc]
    private void RpcSpawn(GameObject item)
    {
        // Ne pas instancier à nouveau l'objet sur le client qui a appelé la commande
        if (!isServer)
        {
            // Il n'est pas nécessaire d'instantier l'objet ici
            // L'objet est déjà synchronisé et instancié automatiquement sur le client par Mirror
        }
		else
		{

		}
    }

    public void AddItemNumber(int value)
	{
		itemNumber += value;
		if(itemNumber >= 1)
		{
			itemNumberText.gameObject.SetActive(true);
			itemNumberText.text = itemNumber.ToString();
		}
		else
		{
			itemNumberText.gameObject.SetActive(false);
		}
	}
	public int GetItemNumber() { return itemNumber; }
}
