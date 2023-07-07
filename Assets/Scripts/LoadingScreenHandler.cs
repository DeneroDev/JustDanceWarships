using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LoadingScreenHandler : MonoBehaviour
{
    [SerializeField] private float alphaStep;
    
    private float _targetAlpha;
    private SpriteRenderer _image;

    private Action _callback;

    public void Awake()
    {
        _image = GetComponent<SpriteRenderer>();
    }

    public void Show(Action onShowComplete)
    {
        alphaStep = Mathf.Abs(alphaStep);
        _targetAlpha = 1;
        _callback = onShowComplete;
        StartCoroutine(SetAlpha());
    }

    public void Hide(Action onHideComplete)
    {
        alphaStep = -alphaStep;
        _targetAlpha = 0.3f;
        _callback = onHideComplete;
        StartCoroutine(SetAlpha());
    }

    IEnumerator SetAlpha()
    {
        while (_targetAlpha != _image.color.a)
        {
            var newAlpha = Mathf.Clamp(_image.color.a + alphaStep, 0.3f, 1);
            _image.color = new Color(1, 1, 1, newAlpha);
            yield return new WaitForSeconds(0.01f);
        }
        
        if(_image.color.a == 0.3f)
            _image.color = new Color(1, 1, 1, 0);
        
        var cacheCallback = _callback;
        _callback = null;
        cacheCallback?.Invoke();
    }
}
