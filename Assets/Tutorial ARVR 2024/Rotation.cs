using UnityEngine;
public class Rotation : MonoBehaviour
{
    public float speed = 25f;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * speed);
    }
}