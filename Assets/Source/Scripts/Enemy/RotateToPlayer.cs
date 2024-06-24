using UnityEngine;

public class RotateToPlayer : Follow
{
    [SerializeField] private float _speed;

    private Transform _heroTransform;
    private IGameFactory _gameFactory;
    private Vector3 _positionToLook;

    private void Start()
    {
        _gameFactory = AllServices.Container.Single<IGameFactory>();

        if (IsHeroExist())
            InitializeHeroTransform();
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
        if (IsInitialized())
            RotateTowardsHero();
    }

    private void RotateTowardsHero()
    {
        UpdatePositionToLookAt();

        transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
    }

    private void UpdatePositionToLookAt()
    {
        Vector3 positionDelta = _heroTransform.position - transform.position;
        _positionToLook = new Vector3(positionDelta.x, transform.position.y, positionDelta.z);
    }

    private void OnPlayerCreated()
    {
        InitializeHeroTransform();
    }

    private void InitializeHeroTransform()
    {
        _heroTransform = _gameFactory.PlayerGameObject.transform;
    }

    private bool IsHeroExist() => _gameFactory.PlayerGameObject != null;

    private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) => Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());

    private Quaternion TargetRotation(Vector3 position) => Quaternion.LookRotation(position);

    private float SpeedFactor() => _speed * Time.deltaTime;

    private bool IsInitialized() => _heroTransform != null;
}