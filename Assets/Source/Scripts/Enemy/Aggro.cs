using System.Collections;
using UnityEngine;

public class Aggro : MonoBehaviour
{
    [HideInInspector][SerializeField] private Follow _follow;

    [SerializeField] private TriggerObserver _triggerObserver;
    [SerializeField] private float _cooldown;

    private bool _hasAggroTarget;

    private Coroutine _aggroCoroutine;

    private void OnValidate()
    {
        _follow ??= GetComponent<Follow>();   
    }

    private void OnEnable()
    {
        _triggerObserver.TriggerEnter += TriggerEnter;
        _triggerObserver.TriggerExit += TriggerExit;
    }

    private void OnDisable()
    {
        _triggerObserver.TriggerEnter -= TriggerEnter;
        _triggerObserver.TriggerExit -= TriggerExit;
    }

    private void Start()
    {
        SwitchFollowOff();
    }

    private void TriggerEnter(Collider obj)
    {
        if (!_hasAggroTarget) 
        {
            _hasAggroTarget = false;

            StopAggroCoroutine();

            SwitchFollowOn();
        } 
    }

    private void TriggerExit(Collider obj)
    {
        if (_hasAggroTarget)
        {
            _hasAggroTarget = false;
            _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
        }
    }

    private void StopAggroCoroutine()
    {
        if (_aggroCoroutine != null)
        {
            StopCoroutine(_aggroCoroutine);

            _aggroCoroutine = null;
        }
    }

    private IEnumerator SwitchFollowOffAfterCooldown()
    {
        yield return new WaitForSeconds(_cooldown);
        SwitchFollowOff();
    }

    private void SwitchFollowOn() => _follow.enabled = true;

    private void SwitchFollowOff() => _follow.enabled = false;
}