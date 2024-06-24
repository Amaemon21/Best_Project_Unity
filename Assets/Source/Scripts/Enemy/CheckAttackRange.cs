using UnityEngine;

[RequireComponent(typeof(EnemyAttack))]
public class CheckAttackRange : MonoBehaviour
{
    [HideInInspector] [SerializeField] private EnemyAttack _enemyAttack;

    [SerializeField] private TriggerObserver _triggerObserver;

    private void OnValidate()
    {
        _enemyAttack ??= GetComponent<EnemyAttack>();
    }

    private void OnEnable()
    {
        _enemyAttack.DisableAttack();

        _triggerObserver.TriggerEnter += TriggerEnter;
        _triggerObserver.TriggerExit += TriggerExit;
    }

    private void OnDisable()
    {
        _triggerObserver.TriggerEnter -= TriggerEnter;
        _triggerObserver.TriggerExit -= TriggerExit;
    }

    private void TriggerEnter(Collider obj)
    {
        _enemyAttack.EnableAttack();
    }

    private void TriggerExit(Collider obj)
    {
        _enemyAttack.DisableAttack();
    }
}