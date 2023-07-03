using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _isTurnLeft;
    GameObject _shooter;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _shooter = GameObject.Find("Kyle Blackthorne");
        float speed = _speed;

        if (_shooter.GetComponent<SpriteRenderer>().flipX == _isTurnLeft)
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
