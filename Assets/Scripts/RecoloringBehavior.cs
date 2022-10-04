using UnityEngine;
using Random = UnityEngine.Random;

public class RecoloringBehavior : MonoBehaviour
{
    [SerializeField] private float _detention;
    
    [SerializeField] private float _recoloringDuration;

    [SerializeField] private float _resizingDuration;
    [SerializeField] private Vector3 _objectScale;

    private Renderer _renderer;

    private Vector3 _startSize = new (1, 1, 1);
    private float _currentTimeDurationResizing;
    private float _currentTimeDetentionResizing;
    private bool _isWaitResizing;
    
    private Color _startColor;
    private Color _nextColor;
    private float _currentTimeDurationRecoloring;
    private float _currentTimeDetentionRecoloring;
    private bool _isWaitRecoloring;
    

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        GenerateNextColor();
    }

    private void Update()
    {
        RecolorObject();
        ResizingObject();
    }

    private void ResizingObject()
    {
        if (_isWaitResizing)
        {
            _currentTimeDetentionResizing += Time.deltaTime;

            if (!(_currentTimeDetentionResizing >= _detention)) return;
            _currentTimeDetentionResizing = 0f;
            GenerateNextScale();
            _isWaitResizing = !_isWaitResizing;
            return;
        }

        _currentTimeDurationResizing += Time.deltaTime;

        var progress = _currentTimeDurationResizing / _resizingDuration;
        var currentSize = Vector3.Lerp(_startSize, _objectScale, progress);
        _renderer.transform.localScale = currentSize;

        if (!(_currentTimeDurationResizing >= _resizingDuration)) return;
        _currentTimeDurationResizing = 0f;
        _isWaitResizing = !_isWaitResizing;
    }

    private void GenerateNextScale()
    {
        (_objectScale, _startSize) = (_startSize, _objectScale);
    }
    
    private void RecolorObject()
    {
        if (_isWaitRecoloring)
        {
            _currentTimeDetentionRecoloring += Time.deltaTime;

            if (!(_currentTimeDetentionRecoloring >= _detention)) return;
            _currentTimeDetentionRecoloring = 0f;
            GenerateNextColor();
            _isWaitRecoloring = !_isWaitRecoloring;

            return;
        }

        _currentTimeDurationRecoloring += Time.deltaTime;

        var progress = _currentTimeDurationRecoloring / _recoloringDuration;
        var currentColor = Color.Lerp(_startColor, _nextColor, progress);
        _renderer.material.color = currentColor;

        if (!(_currentTimeDurationRecoloring >= _recoloringDuration)) return;
        _currentTimeDurationRecoloring = 0f;
        _isWaitRecoloring = !_isWaitRecoloring;
    }

    private void GenerateNextColor()
    {
        _startColor = _renderer.material.color;
        _nextColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 0.5f);
    }
}