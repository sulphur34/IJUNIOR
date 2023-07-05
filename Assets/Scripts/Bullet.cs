using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _isTurnLeft;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        float speed = _speed;

        if (gameObject.GetComponent<SpriteRenderer>().flipX == _isTurnLeft)
            speed *= -1;
        
        _rigidbody.velocity = transform.right * speed;
    }

    private void Update()
    {
        Destroy(gameObject, 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
