using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(CharacterController))]
public class PlayerAttack : MonoBehaviour, ISavedProgressReader
{
    [HideInInspector] [SerializeField] private PlayerAnimator _playerAnimator;
    [HideInInspector] [SerializeField] private CharacterController _characterController;

    private int _layerMask;
    private Transform _transform;

    private Collider[] _hits = new Collider[3];

    private IInputService _inputService;
    private DamageStats _stats;

    private void OnValidate()
    {
        _playerAnimator ??= GetComponent<PlayerAnimator>();
        _characterController ??= GetComponent<CharacterController>();
    }

    private void Awake()
    {
        _inputService = AllServices.Container.Single<IInputService>();

        _layerMask = 1 << LayerMask.NameToLayer("Hitbox");

        _transform = transform;
    }

    private void Update()
    {
        if (_inputService.IsAttackButton() && !_playerAnimator.IsAttacking)
        {
            _playerAnimator.PlayAttack();
        }
    }

    public void LoadProgress(PlayerProgress progress)
    {
        _stats = progress.DamageStats;
    }

    public void OnAttack()
    {
        for (int i = 0; i < Hit(); i++)
        {
            _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
        }
    }

    private int Hit()
    {
        return Physics.OverlapSphereNonAlloc(StartPoint() + _transform.forward, _stats.Radius, _hits, _layerMask);
    }

    private Vector3 StartPoint()
    {
        return new Vector3(_transform.position.x, _characterController.center.y / 2, _transform.position.z);
    }
}