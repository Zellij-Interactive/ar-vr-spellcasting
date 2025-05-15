using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float lifetime = 3f; // seconds before disappearing

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
