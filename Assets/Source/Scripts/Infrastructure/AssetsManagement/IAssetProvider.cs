using UnityEngine;

public interface IAssetProvider : IService
{
    public GameObject Instantiate(string path);
    public GameObject Instantiate(string path, Vector3 position);
}