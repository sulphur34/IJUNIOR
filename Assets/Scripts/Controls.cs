using UnityEngine;

public class Controls : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _moveVector;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Shoot");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            bool isArmed = _animator.GetBool("isArmed");
            _animator.SetBool("isArmed", !isArmed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            float speed = 0.4f;
            Flip();
            Move(speed);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            _animator.SetFloat("moveX", 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            float speed = -0.4f;
            Flip();
            Move(speed);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            _animator.SetFloat("moveX", 0);
        }
    }

    private void Move(float speed)
    {
        _moveVector.x = speed * Time.deltaTime;
        _animator.SetFloat("moveX", Mathf.Abs(_moveVector.x));
        transform.Translate(_moveVector.x, 0, 0);
    }

    private void Flip()
    {
        _spriteRenderer.flipX = _moveVector.x < 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float bulletforce = 10f;
        collision.gameObject.GetComponent<Rigidbody>();
        Vector3 impactForce = collision.relativeVelocity;
        Debug.Log(impactForce.magnitude);

        if (impactForce.magnitude > bulletforce)
            _animator.SetTrigger("Damage");
    }
}
