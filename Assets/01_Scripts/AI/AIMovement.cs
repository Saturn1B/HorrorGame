using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterState
{
	WANDER,
	CHASE,
	SCARE
}

public class AIMovement : MonoBehaviour
{
	[SerializeField] public NavMeshAgent agent;
	[SerializeField] private float range;

	[SerializeField] private float detectionRadius;

	[SerializeField] private Transform centerPoint;


	[SerializeField] private float minTime, maxTime;

	[SerializeField] private MonsterState _monsterState;

	[SerializeField] private float wanderSpeed, chaseSpeed, scareSpeed;

	float startSearchTimer;
	[SerializeField] float searchTimer;


	private CharacterController[] players;
	private CharacterController chasingPlayer;

	bool scareEnd;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		StartCoroutine(StateTimer());
		players = FindObjectsOfType<CharacterController>();
		startSearchTimer = searchTimer;
	}

	private void Update()
	{
		if(agent.velocity.magnitude > 0)
		{
			Vector3 direction = agent.velocity.normalized;
			direction.y = 0;

			if(direction != Vector3.zero)
			{
				Quaternion lookRotation = Quaternion.LookRotation(direction);
				transform.rotation = lookRotation;
				//transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
			}
		}

		switch (_monsterState)
		{
			case MonsterState.WANDER:
				if (agent.remainingDistance <= agent.stoppingDistance)
				{
					Vector3 point;
					if (RandomPoint(centerPoint.position, range, out point))
					{
						if(agent.speed != wanderSpeed)
							agent.speed = wanderSpeed;

						Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
						agent.SetDestination(point);
					}
				}

				if (startSearchTimer <= 0)
				{
					Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
					foreach (Collider hit in hitColliders)
					{
						if (hit.GetComponent<CharacterController>())
						{
							StopAllCoroutines();
							_monsterState = MonsterState.CHASE;
							chasingPlayer = hit.GetComponent<CharacterController>();
							agent.speed = chaseSpeed;
							StartCoroutine(StateTimer());
						}
					}
				}
				else
				{
					startSearchTimer -= Time.deltaTime;
				}

				break;
			case MonsterState.CHASE:
				if (Vector3.Distance(transform.position, chasingPlayer.transform.position) <= 1)
				{
					//TO DO make some scary shit + kill chasing player
					StopAllCoroutines();
					_monsterState = MonsterState.WANDER;
					chasingPlayer = null;
					startSearchTimer = searchTimer;
					agent.speed = wanderSpeed;
					StartCoroutine(StateTimer());

					break;
				}

				if (chasingPlayer != null)
					agent.SetDestination(chasingPlayer.transform.position);
				break;
			case MonsterState.SCARE:

				if (Vector3.Distance(transform.position, chasingPlayer.transform.position) <= detectionRadius)
				{
					Debug.Log("near player");

					if (!scareEnd)
					{
						Debug.Log("change scare destination");
						scareEnd = true;
						//TO DO make some scary shit
						StopAllCoroutines();
						agent.SetDestination(transform.position + transform.forward * detectionRadius / 1.5f);
						StartCoroutine(SetScareDestination(chasingPlayer.transform.position));
					}

					break;
				}

				if (chasingPlayer != null)
					agent.SetDestination(chasingPlayer.transform.position);

				break;
		}
	}

	Vector3 center;
	IEnumerator SetScareDestination(Vector3 playerPos)
	{
		while(agent.remainingDistance > agent.stoppingDistance)
		{
			yield return null;
		}

		agent.speed = chaseSpeed;
		center = (centerPoint.position - playerPos).normalized * (range + 10);
		Vector3 point;
		while (!RandomPoint(center, range, out point))
		{
			yield return null;
		}

		agent.SetDestination(point);

		_monsterState = MonsterState.WANDER;
		chasingPlayer = null;
		startSearchTimer = searchTimer;
		StartCoroutine(StateTimer());
		scareEnd = false;
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
		Debug.Log($"start timer: {timer}");

		yield return new WaitForSeconds(timer);
		//select new random state that isn't the current state
		_monsterState = SetRandomState();
		switch (_monsterState)
		{
			case MonsterState.WANDER:
				chasingPlayer = null;
				startSearchTimer = searchTimer;
				agent.speed = wanderSpeed;
				break;
			case MonsterState.CHASE:
				CharacterController closestPlayer = null;
				float closestDistance = Mathf.Infinity;

				foreach (CharacterController player in players)
				{
					if (Vector3.Distance(transform.position, player.transform.position) < closestDistance)
					{
						closestDistance = Vector3.Distance(transform.position, player.transform.position);
						closestPlayer = player;
					}
				}
				chasingPlayer = closestPlayer;
				agent.speed = chaseSpeed;
				break;
			case MonsterState.SCARE:
				chasingPlayer = players[Random.Range(0, players.Length)];
				agent.speed = scareSpeed;
				break;
		}
		Debug.Log($"new state: {_monsterState}");

		StartCoroutine(StateTimer());
	}

	private MonsterState SetRandomState()
	{
		System.Array states = System.Enum.GetValues(typeof(MonsterState));

		if (_monsterState == MonsterState.SCARE)
			return MonsterState.WANDER;

		MonsterState state = (MonsterState)states.GetValue(Random.Range(0, states.Length));
		while(state == _monsterState)
		{
			state = (MonsterState)states.GetValue(Random.Range(0, states.Length));
		}

		return state;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(transform.position, detectionRadius);
		Gizmos.DrawWireSphere(center, range);
		Gizmos.DrawSphere(agent.destination, 1);
	}
}
