using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyAnimator))]
public class AnimateAlongAgent : MonoBehaviour
{
    private readonly float _minimalVelocity = 0.1f;

    [HideInInspector] [SerializeField] private NavMeshAgent _agent;
    [HideInInspector] [SerializeField] private EnemyAnimator _enemyAnimator;

    private void OnValidate()
    {
        _agent ??= GetComponent<NavMeshAgent>();
        _enemyAnimator ??= GetComponent<EnemyAnimator>();
    }

    private void Update()
    {
        if (ShouldMove())
            _enemyAnimator.Move(_agent.velocity.magnitude);
        else
            _enemyAnimator.StopMoving();
    }

    private bool ShouldMove()
    {
        return _agent.velocity.magnitude > _minimalVelocity && _agent.remainingDistance > _agent.radius;
    }
}