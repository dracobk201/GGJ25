using ScriptableObjectArchitecture;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObjectCollection objectPoolCollection = default;
    [SerializeField] private IntReference objectPoolAmount = default;
    [SerializeField] private GameObject objectPrefab = default;

    private void Awake()
    {
        InitializeObjectPool();
    }

    public void InitializeObjectPool()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        objectPoolCollection.Clear();
        InstantiateObjects();
    }

    private void InstantiateObjects()
    {
        GameObject newObject = null;
        Transform targetParent = transform;

        for (int i = 0; i < objectPoolAmount.Value; i++)
        {
            if (newObject == null)
            {
                newObject = Instantiate(objectPrefab);
                newObject.name = $"{objectPrefab.name}-{i}";
                newObject.transform.SetParent(targetParent);
            }
            else
            {
                newObject = Instantiate(newObject, targetParent);
                newObject.name = $"{objectPrefab.name}-{i}";
            }
            objectPoolCollection.Add(newObject);
            newObject.SetActive(false);
        }
    }
}