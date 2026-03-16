using System.Collections;
using UnityEngine;

public class HealthBarSliderSmooth : HealthBarSlider
{
    [SerializeField] private float _smoothTime = 0.5f;

    private Coroutine _currentCoroutine;

    protected override void OnHealthUpdated()
    {
        InitializeSlider();

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(ChangeHealthSmooth());
    }

    private IEnumerator ChangeHealthSmooth()
    {
        float runningTime = 0f;
        float startValue = _slider.value;

        while (runningTime < _smoothTime)
        {
            _slider.value = Mathf.Lerp(startValue, _health.CurrentHealth, runningTime / _smoothTime);
            runningTime += Time.deltaTime;

            yield return null;
        }

        _slider.value = _health.CurrentHealth;
    }
}