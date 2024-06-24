using UnityEngine;

public class GameRuner : MonoBehaviour
{
    [SerializeField] private GameBootstrapper _gameBootstrapper;

    private void Awake()
    {
        GameBootstrapper bootstraper = FindFirstObjectByType<GameBootstrapper>();

        if (bootstraper == null)
            Instantiate(_gameBootstrapper);
    }
}