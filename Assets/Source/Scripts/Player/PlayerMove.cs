using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour, ISavedProgress
{
    [HideInInspector] [SerializeField] private CharacterController _characterController;

    [Space(5)]
    [SerializeField] private float _speed;

    private Camera _camera;
    private Transform _transform;

    private IInputService _inputService;
    private ISaveLoadSerivce _saveLoadServer;

    private void OnValidate()
    {
        _characterController ??= GetComponent<CharacterController>();
    }

    private void Awake()
    {
        _inputService = AllServices.Container.Single<IInputService>();
        _saveLoadServer = AllServices.Container.Single<ISaveLoadSerivce>();

        _transform = transform;
        _camera = Camera.main;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movementDirection = Vector3.zero;

        if (_inputService.Axis.sqrMagnitude > Constans.Treshold)
        {
            movementDirection = _camera.transform.TransformDirection(_inputService.Axis);
            movementDirection.y = 0;
            movementDirection.Normalize();

            _transform.forward = movementDirection;
        }

        movementDirection += Physics.gravity;

        _characterController.Move(movementDirection * _speed *  Time.deltaTime);
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), _transform.position.AsVectorData());
    }

    public void LoadProgress(PlayerProgress progress)
    {
        if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
        {
            Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;

            if (savedPosition != null)
            {
                Warp(savedPosition);
            }
        }
    }

    private void Warp(Vector3Data vector3Data)
    {
        _characterController.enabled = false;
        _transform.position = vector3Data.AsUnityVector().AddY(_characterController.height);
        _characterController.enabled = true;
    }

    private string CurrentLevel()
    {
        return SceneManager.GetActiveScene().name;
    }
}