using UnityEngine;

public class ActorUI : MonoBehaviour
{ 
    [SerializeField] private HealthBar _healthBar;

    private IHealth _health;

    public void Construct(IHealth health)
    {
        _health = health;
        _health.HealthChanged += UpdateUI;
    }

    private void Start()
    {
        IHealth health = GetComponent<EnemyHealth>();

        if (health != null)
            Construct(health);
    }

    private void OnEnable() => _health.HealthChanged -= UpdateUI;

    private void UpdateUI()
    {
        _healthBar.UpdateValue(_health.Current, _health.Max);
    }
}