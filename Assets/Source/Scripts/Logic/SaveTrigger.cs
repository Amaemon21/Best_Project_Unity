using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SaveTrigger : MonoBehaviour
{
    [HideInInspector] [SerializeField] private BoxCollider _boxCollider;

    private ISaveLoadSerivce _saveLoadServer;

    private void Awake()
    {
        _saveLoadServer = AllServices.Container.Single<ISaveLoadSerivce>();
    }

    private void OnValidate()
    {
        _boxCollider ??= GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMove>(out PlayerMove player))
        {
            _saveLoadServer.SaveProggress();
            Debug.Log("Save progress");
            gameObject.SetActive(false);
        }          
    }

    private void OnDrawGizmos()
    {
        if (!_boxCollider)
            return;
    
        Gizmos.color = new Color32(30, 200, 30, 130);
        Gizmos.DrawCube(transform.position + _boxCollider.center, _boxCollider.size);
    }
}