using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float _timeDestroy;

    private void Start()
    {
        Destroy(gameObject, _timeDestroy);
    }
}