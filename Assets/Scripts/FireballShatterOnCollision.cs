using UnityEngine;

public class FireballShatterOnCollision : MonoBehaviour
{
    public GameObject fragmentPrefab;  // Drag fire.fbx prefab here
    public int fragmentCount = 10;
    public float explosionForce = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < fragmentCount; i++)
        {
            Vector3 spawnPos = transform.position + Random.insideUnitSphere * 0.2f;
            GameObject fragment = Instantiate(fragmentPrefab, spawnPos, Random.rotation);
            Rigidbody rb = fragment.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 forceDir = (spawnPos - transform.position).normalized;
                rb.AddForce(forceDir * explosionForce, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }
}
