using UnityEngine;

public class Spawner : MonoBehaviour
{
   public GameObject prefab;
   private float timer;

   public float minGapSize = 3.0f;
   public float gapSizeDecrement = 0.5f;
   public float spawnRateDecrement = 0.1f;
   public float minSpawnRate = 0.5f;
   public float spawnRate = 1f;

   public float minHeight = -1f;

   public float maxHeight = 1f;

   public float pipeSpeed = 2f;

   public float gapSize = 4.5f;
   private int pipesSpawned = 0;
   private void OnEnable()
   {
    InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
   }
   
   private void OnDisable()
   {
    CancelInvoke(nameof(Spawn));
   }

   private void Spawn()
   {
     GameObject Pipes = Instantiate(prefab, transform.position, Quaternion.identity);
     Pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
     Pipes pipeScript = Pipes.GetComponent<Pipes>();
     if (pipeScript != null)
        {
            pipeScript.SetGapSize(gapSize);
        }
    
   pipesSpawned++;
   if (pipesSpawned % 5 == 0)
        {
            gapSize = Mathf.Max(gapSize - gapSizeDecrement, minGapSize);
            Debug.Log($"Gap size decreased to: {gapSize}");
        }
   
   }
}
