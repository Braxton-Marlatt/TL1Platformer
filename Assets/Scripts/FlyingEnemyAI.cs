using UnityEngine;
using UnityEngine.AI;

public class FlyingEnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
	[SerializeField] private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer

	NavMeshAgent agent;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false; //Restrict transform orientation change
		agent.updateUpAxis = false; //Prevent automatic up axis realignment

	}

	void Update()
	{
		//This is where we change the target.
		//There are other controls available.
		agent.SetDestination(target.position);
		Vector3 direction = target.position - transform.position;

        // Flip the sprite based on the direction of movement
        if (direction.x != 0)
        {
            spriteRenderer.flipX = direction.x > 0;
        }

	}
}