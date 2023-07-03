using System.Collections;
using System.Linq;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] private float _speed;

    public event System.Action<float> RedColorChanged;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _currentDestinationPosition;
    private SpriteRenderer _spriteRenderer;
    private bool _isContinue;

    private void Start()
    {
        _startPosition = new Vector3(0f,0f,0f) + transform.position;
        _endPosition = new Vector3(0f,-0.3f,0f) + transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _isContinue = true;
        StartCoroutine(DetectEnemy());
    }

    private void Update()
    {
        if (transform.position == _startPosition)
            _currentDestinationPosition = _endPosition;
        else if (transform.position == _endPosition)
            _currentDestinationPosition = _startPosition;

        MoveTowardsTarget(_currentDestinationPosition);
        DetectEnemy();
    }

    private IEnumerator DetectEnemy()
    {
        while (_isContinue)
        {
            AlertByColor();
            AlertBySound();
            yield return null;
        }
    }

    private void MoveTowardsTarget(Vector3 destinationPosition)
    {
        var moveVector = Vector2.MoveTowards(transform.position, destinationPosition, _speed * Time.deltaTime);
        transform.position = moveVector;
    }

    private void AlertByColor()
    {
        GameObject[] ennemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (ennemies.Length == 0)
        {
            _spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            float minDistance = ennemies.Select(x => Mathf.Abs((x.transform.position - transform.position).magnitude)).Min();
            _spriteRenderer.color = new Color(1f, minDistance / 2, minDistance / 2, 1f);
        }
    }

    private void AlertBySound()
    {
    }

}