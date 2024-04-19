using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootSolver : MonoBehaviour
{
	[SerializeField] private Transform body;
	[SerializeField] private float stepDistance;
	[SerializeField] private float stepHeight;
	[SerializeField] private float speed;
	[SerializeField] private Vector3 footOffset;

	[SerializeField] private IKFootSolver otherFoot1, otherFoot2;
	[SerializeField] private SpiderMovement spiderMovement;

	Vector3 newPosition;
	Vector3 currentPosition;
	Vector3 oldPosition;

	float lerp;

	int direction;

	private void Start()
	{
		spiderMovement = FindObjectOfType<SpiderMovement>();
		footOffset += new Vector3(transform.localPosition.x, 0, 0);
		currentPosition = newPosition = oldPosition = transform.position;
		lerp = 1;
	}

	private void Update()
	{
		transform.position = currentPosition;
		Ray ray = new Ray(body.position + body.right * footOffset.x + body.forward * footOffset.z + Vector3.up * 2, Vector3.down);
		if(Physics.Raycast(ray, out RaycastHit info, 10))
		{
			if (Vector3.Distance(newPosition, info.point) > stepDistance && !otherFoot1.IsMoving() && !otherFoot2.IsMoving())
			{
				lerp = 0;
				direction = body.InverseTransformPoint(info.point).z > body.InverseTransformPoint(newPosition).z ? 1 : -1;
				newPosition = info.point * direction;
				newPosition.y = info.point.y;
				Debug.Log($"hit object: {info.transform.name} {Mathf.FloorToInt(newPosition.y)}");
			}
		}
		if (lerp < 1)
		{
			Vector3 footPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
			footPosition.y = Mathf.Clamp(Mathf.Sin(lerp * Mathf.PI) * stepHeight, newPosition.y, newPosition.y + stepHeight);

			currentPosition = footPosition;
			lerp += Time.deltaTime * (speed * (spiderMovement.speed / 5));
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
