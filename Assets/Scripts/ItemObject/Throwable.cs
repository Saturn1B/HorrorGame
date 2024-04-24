using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : ItemObject
{
	private Rigidbody rb;
	private bool thrown;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	public override void Use(CharacterTarget usingPlayer)
	{
		Vector3 throwDirection = Camera.main.transform.forward * 10 + Vector3.up * 3;
		rb.AddForce(throwDirection + usingPlayer.transform.GetComponent<CharacterController>().velocity * .75f, ForceMode.Impulse);
		thrown = true;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (thrown)
		{
			Debug.Log(rb.velocity.magnitude);
			thrown = false;
		}
	}
}
