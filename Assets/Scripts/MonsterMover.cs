using UnityEngine;

public class MonsterMover : MonoBehaviour
{
    public float speed = 1.5f;
    public float directionChangeInterval = 2f;
    public float maxWanderDistance = 2f;

    private Vector3 moveDirection;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        ChangeDirection();
        InvokeRepeating(nameof(ChangeDirection), directionChangeInterval, directionChangeInterval);
    }

    void Update()
    {
        Vector3 newPosition = transform.position + moveDirection * speed * Time.deltaTime;

        if (Vector3.Distance(startPosition, newPosition) <= maxWanderDistance)
        {
            transform.position = newPosition;
        }
        else
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        float angle = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0, angle, 0);
        moveDirection = transform.forward;
    }

    void OnCollisionEnter(Collision collision)
    {
        ChangeDirection();
    }
}
