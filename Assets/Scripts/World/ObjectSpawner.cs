using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ObjectSpawn
    {
        public string[] prefabTags;
        public float spawnRate = 2.5f;
        public bool heightFixed = false;
        public float heightOffset = 1f;
        public float cameraXOfsset = 1f;
        public SpawnRandomizer randomizer;

        [HideInInspector] public float timer = 0f;

        [System.Serializable]
        public class SpawnRandomizer
        {
            public bool isRandom = false;
            [Range(0, 100)] public int probability;
        }

    }

    public List<ObjectSpawn> objects;

    private void Update()
    {
        if (!Player.isMoving) return;
        foreach (var obj in objects)
        {
            obj.timer += Time.deltaTime;

            if (obj.timer >= obj.spawnRate)
            {
                SpawnObj(obj);
                obj.timer = 0f;
            }
        }

    }

    private void SpawnObj(ObjectSpawn obj)
    {
        int probability = obj.randomizer.isRandom ? Random.Range(1, ((100 / (obj.randomizer.probability)) + 1)) : 1;
        if (probability == 1)
        {
            var screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));
            var yPos = !obj.heightFixed ? Random.Range(-obj.heightOffset, obj.heightOffset) : obj.heightOffset;

            var spawnPos = new Vector3(screenRight.x + obj.cameraXOfsset, yPos, 0);

            ObjectPooler_manager.Instance.SpawnFromPool(obj.prefabTags[Random.Range(0, obj.prefabTags.Length)], spawnPos, Quaternion.identity);
        }
    }
}