using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler_manager : MonoBehaviour
{
    [System.Serializable] 
    //cria a piscina (explicação detalha em: SpawnFromPool)
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> poolList;
    Dictionary<string, Queue<GameObject>> poolDictionary;

    #region Singleton

    public static ObjectPooler_manager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach(Pool pool in poolList)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    /* @brief Responsável por spawnar os objetos da piscina.
     *
     * armazena todos os objetos que estão na cena  numa fila para que
     * eles só precisem ser intanciados uma vez e quando necessario, ser colocado em uma
     * nova posição.
     */
    public GameObject SpawnFromPool(string tag,  Vector3 position, Quaternion rotation)
    {
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();
        if(pooledObject != null)
        {
            pooledObject.OnObjectSpawn();
        }
        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}
