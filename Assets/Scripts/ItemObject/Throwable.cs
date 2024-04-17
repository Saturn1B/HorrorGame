using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : ItemObject
{
	private Rigidbody rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	public override void Use()
	{
		Vector3 throwDirection = Camera.main.transform.forward * 10 + Vector3.up * 3;
		rb.AddForce(throwDirection, ForceMode.Impulse);
	}
}
