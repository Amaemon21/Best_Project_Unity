using UnityEngine;
using UnityEngine.AI;

public class AgentMoveToPlayer : Follow
{
    private const float _minimalDistance = 1;

    [HideInInspector] [SerializeField] private NavMeshAgent _agent;

    private Transform _target;
    private IGameFactory _gameFactory;

    private void OnValidate()
    {
        _agent ??= GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _gameFactory = AllServices.Container.Single<IGameFactory>();

        if (_gameFactory.PlayerGameObject != null)  
            InitializeTargetTransform();
        else
            _gameFactory.PlayerCreated += OnPlayerCreated;
    }

    private void OnEnable()
    {
        if (_gameFactory != null)
            _gameFactory.PlayerCreated -= OnPlayerCreated;
    }

    private void Update()
    {
        if (TryInitialized() && IsTargetNotReched())
        {
            _agent.destination = _target.position;
        }
    }

    private void OnPlayerCreated()
    {
        InitializeTargetTransform();
    }

    private void InitializeTargetTransform()
    {
        _target = _gameFactory.PlayerGameObject.transform;
    }

    private bool IsTargetNotReched() => _agent.transform.position.SqrMagnitudeTo(_target.position) >= _minimalDistance;

    private bool TryInitialized() => _target != null;
}