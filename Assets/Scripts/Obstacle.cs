using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject _indicator;
    [SerializeField] private GameObject _body;

    public bool IsActive { get; private set; }

    public void Activate(float indicatorDuration, float lifetime)
    {
        if (IsActive) return;
        StartCoroutine(LifetimeRoutine(indicatorDuration, lifetime));
    }

    public void Deactivate()
    {
        StopAllCoroutines();
        _indicator.SetActive(false);
        _body.SetActive(false);
        IsActive = false;
    }

    private IEnumerator LifetimeRoutine(float indicatorDuration, float lifetime)
    {
        IsActive = true;

        _indicator.SetActive(true);
        _body.SetActive(false);
        yield return new WaitForSeconds(indicatorDuration);

        _indicator.SetActive(false);
        _body.SetActive(true);
        yield return new WaitForSeconds(lifetime);

        Deactivate();
    }
}