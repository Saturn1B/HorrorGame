using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterState
{
	WANDER,
	CHASE
}

public class AIMovement : MonoBehaviour
{
	[SerializeField] private NavMeshAgent agent;
	[SerializeField] private float range;

	[SerializeField] private Transform centerPoint;


	[SerializeField] private float minTime, maxTime;

	[SerializeField] private MonsterState _monsterState;

	private CharacterController[] players;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		//StartCoroutine(StateTimer());
		players = FindObjectsOfType<CharacterController>();
	}

	private void Update()
	{
		switch (_monsterState)
		{
			case MonsterState.WANDER:
				if (agent.remainingDistance <= agent.stoppingDistance)
				{
					Vector3 point;
					if (RandomPoint(centerPoint.position, range, out point))
					{
						Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
						agent.SetDestination(point);
					}
				}
				break;
			case MonsterState.CHASE:
					agent.SetDestination(players[0].GetComponent<Transform>().position);
				break;
		}
	}

	private bool RandomPoint(Vector3 center, float range, out Vector3 result)
	{
		Vector3 randomPoint = center + Random.insideUnitSphere * range;
		NavMeshHit hit;
		if(NavMesh.SamplePosition(randomPoint, out hit, 1, NavMesh.AllAreas))
		{
			result = hit.position;
			return true;
		}

		result = Vector3.zero;
		return false;
	}

	private IEnumerator StateTimer()
	{
		float timer = Random.Range(minTime, maxTime);
		yield return new WaitForSeconds(timer);
		_monsterState = SetRandomState();
		StartCoroutine(StateTimer());
	}

	private MonsterState SetRandomState()
	{
		System.Array states = System.Enum.GetValues(typeof(MonsterState));
		MonsterState state = (MonsterState)states.GetValue(Random.Range(0, states.Length));
		return state;
	}
}
