using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootSolver : MonoBehaviour
{
	[SerializeField] private Transform body;
	float footSpacing;
	[SerializeField] private float stepDistance;
	[SerializeField] private float stepHeight;
	[SerializeField] private float speed;
	[SerializeField] private Vector3 footOffset;

	[SerializeField] private IKFootSolver otherFoot;

	Vector3 newPosition;
	Vector3 currentPosition;
	Vector3 oldPosition;

	float lerp;

	int direction;

	private void Start()
	{
		footSpacing = transform.localPosition.x;
		currentPosition = newPosition = oldPosition = transform.position;
		lerp = 1;
	}

	private void Update()
	{
		transform.position = currentPosition + footOffset;

		Ray ray = new Ray(body.position + (body.right * footSpacing), Vector3.down);
		if(Physics.Raycast(ray, out RaycastHit info, 10))
		{
			if(Vector3.Distance(newPosition, info.point) > stepDistance && !otherFoot.IsMoving())
			{
				lerp = 0;
				direction = body.InverseTransformPoint(info.point).z > body.InverseTransformPoint(newPosition).z ? 1 : -1;
				newPosition = info.point + (body.forward * stepDistance * direction) + footOffset;
			}
		}
		if (lerp < 1)
		{
			Vector3 footPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
			footPosition.y = Mathf.Sin(lerp * Mathf.PI) * stepHeight;

			currentPosition = footPosition;
			lerp += Time.deltaTime * speed;
		}
		else
		{
			oldPosition = newPosition;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(newPosition, 0.5f);
	}

	public bool IsMoving()
	{
		return lerp < 1;
	}
}
