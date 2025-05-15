using UnityEngine;

public class FadeAndDestroy : MonoBehaviour
{
    public float fadeDuration = 2f;

    private Material material;
    private Color startColor;
    private float timer = 0f;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // Clone the material to avoid affecting shared materials
            material = renderer.material;
            startColor = material.color;
        }
    }

    void Update()
    {
        if (material == null) return;

        timer += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);

        Color newColor = startColor;
        newColor.a = alpha;
        material.color = newColor;

        if (timer >= fadeDuration)
        {
            Destroy(gameObject);
        }
    }
}

