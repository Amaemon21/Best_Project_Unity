using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _rotationAngleX;
    [SerializeField] private float _distance;
    [SerializeField] private float _offsetY;

    private Transform _target;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void LateUpdate()
    {
        if (_target == null)
            return;

        Quaternion rotation = Quaternion.Euler(_rotationAngleX, 0f, 0f);

        Vector3 position = rotation * new Vector3(0, 0, -_distance) + FollowingPointPosition();

        _transform.position = position; 
        _transform.rotation = rotation;
    }

    public void Follow(Transform target)
    {
        _target = target;
    }

    private Vector3 FollowingPointPosition()
    {
        Vector3 followPosition = _target.position;
        followPosition.y += _offsetY;

        return followPosition;
    }
}