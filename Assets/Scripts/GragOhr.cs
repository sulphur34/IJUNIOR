using UnityEngine;

public class GragOhr : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;

    private Animator _animator;
    private bool _isDead;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _isDead = false;
    }

    // Update is called once per frame
    private void Update() 
    {
        float shootingRange = 3f;
        if (_isDead == false)
        {
            float distance = (_target.position - transform.position).magnitude;
            _animator.SetFloat("ShootRange", distance);

            if (distance > shootingRange)
                MoveTowardsTarget();
            else
                _animator.SetFloat("moveX", 0);
        }        
    }

    private void MoveTowardsTarget()
    {
        var moveVector = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        _animator.SetFloat("moveX", Mathf.Abs(moveVector.x));
        transform.position = moveVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float bulletforce = 10f;
        float timeToDestroy = 3;
        collision.gameObject.GetComponent<Rigidbody>();
        Vector3 impactForce = collision.relativeVelocity;

        if (impactForce.magnitude > bulletforce && _isDead == false)
        {
            _animator.SetTrigger("Dead");
            _isDead = true;
            Destroy(gameObject, timeToDestroy);
        }
    }
}
