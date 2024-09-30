using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private EnemyAnimator _enemyAnimator;

    [SerializeField] private float _attackCooldown = 3f;
    [SerializeField] private float _cleavage = 5f;
    [SerializeField] private float _effectiveDistance;

    [Space(5)]
    [SerializeField] private float _damage;

    private Collider[] _hits = new Collider[1];

    private IGameFactory _gameFactory;

    private Transform _playerTransform;
    private Transform _transform;

    private bool _isAttacking;
    private bool _isAttackActive;

    private float _cooldown;
    private int _layerMask;

    private void OnValidate()
    {
        _enemyAnimator ??= GetComponent<EnemyAnimator>();
    }

    private void Awake()
    {
        _gameFactory = AllServices.Container.Single<IGameFactory>();
        _transform = transform;
        _layerMask = 1 << LayerMask.NameToLayer("Player");
    }

    private void OnEnable()
    {
        _gameFactory.PlayerCreated += OnPlayerCreated;
    }

    private void OnDisable()
    {
        if (_gameFactory != null)
            _gameFactory.PlayerCreated -= OnPlayerCreated;
    }

    private void Update()
    {
        UpdateCooldown();

        if (CanAttack())
            StartAttack();
    }

    public void EnableAttack()
    {
        _isAttackActive = true;
    }

    public void DisableAttack()
    {
        _isAttackActive = false;
    }

    private void OnAttack()
    {
        if (Hit(out Collider hit))
        {
            hit.transform.GetComponent<IHealth>().TakeDamage(_damage);
        }
    }

    private void OnAttackEnded()
    {
        _cooldown = _attackCooldown;
        
        _isAttacking = false;
    }

    private void StartAttack()
    {
        _transform.LookAt(_playerTransform);
        _enemyAnimator.PlayAttack();

        _isAttacking = true;
    }

    private bool Hit(out Collider hit)
    {
        int hitsCount = Physics.OverlapSphereNonAlloc(StartPoint(), _cleavage, _hits, _layerMask);

        hit = _hits.FirstOrDefault();

        return hitsCount > 0;
    }

    private void UpdateCooldown()
    {
        if (!CooldownIsUp())
            _cooldown -= Time.deltaTime;
    }

    private bool CanAttack() => _isAttackActive && !_isAttacking && CooldownIsUp();

    private bool CooldownIsUp() => _cooldown <= 0;

    private Vector3 StartPoint()
    {
        return new Vector3(_transform.position.x, _transform.position.y + 0.5f, _transform.position.z) + _transform.forward * _effectiveDistance;
    }

    private void OnPlayerCreated()
    {
        _playerTransform = _gameFactory.PlayerGameObject.transform;
    }
}