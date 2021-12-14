using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private string _tag;
    private GameObject _prefab;
    private int _size;
    private Queue<GameObject> _objectPool;

    public ObjectPool(string tag, GameObject prefab, int size)
    {
        _tag = tag;
        _prefab = prefab;
        _size = size;
    }

    public void Init()
    {
        _objectPool = new Queue<GameObject>();
        for (int i = 0; i < _size; i++)
        {
            var obj = Object.Instantiate(_prefab);
            obj.SetActive(false);
            _objectPool.Enqueue(obj);
        }
    }
    
    public GameObject SpawnFromPool(Vector3 position, Quaternion rotation)
    {
        if (_objectPool.Count <= 0)
        {
            Debug.LogError("Hemos sobrepasado el tamaño de la pool");
            return null;
        }

        var obj = _objectPool.Dequeue();
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        _objectPool.Enqueue(obj);
        return obj;
    }
}
