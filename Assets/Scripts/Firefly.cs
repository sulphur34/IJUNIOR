using System.Collections;
using UnityEngine;

public class Firefly : MonoBehaviour
{
    [SerializeField] private float _maxDistance;
    [SerializeField] Transform _basePoint;

    private float _minDistance;
    private bool _isContinue;

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        _isContinue = true;
        _minDistance = 0f;
        StartCoroutine(FlyRandom());
    }

    private IEnumerator FlyRandom()
    {
        while (_isContinue)
        {
            StartCoroutine(FlyRandomDirection());
            yield return new WaitForSecondsRealtime(0.2f);
        }
    }

    private IEnumerator FlyRandomDirection()
    {
        int iterations = 50;
        float distance = Random.Range(_minDistance, _maxDistance);

        Vector3 newPosition = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized * distance;

        for (int i = 0; i < iterations; i++)
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, 0.005f);
            yield return null;
        }
    }
}
