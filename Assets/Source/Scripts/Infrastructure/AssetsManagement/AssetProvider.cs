using UnityEngine;

public class AssetProvider : IAssetProvider
{
    public GameObject Instantiate(string path)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        return GameObject.Instantiate(prefab);
    }

    public GameObject Instantiate(string path, Vector3 position)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        return GameObject.Instantiate(prefab, position, Quaternion.identity);
    }
}