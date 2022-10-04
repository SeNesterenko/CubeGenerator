using UnityEngine;
using Random = UnityEngine.Random;

public class RecoloringBehavior : MonoBehaviour
{
    [SerializeField] private float _recoloringDuration = 2f;
    [SerializeField] private int _recoloringDetention;

    private Color _startColor;
    private Color _nextColor;
    private Renderer _renderer;

    private float _currentTimeDuration;
    private float _currentTimeDetention;
    private bool _isWaitRecoloring = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        GenerateNextColor();
    }

    private void Update()
    {
        if (_isWaitRecoloring)
        {
            _currentTimeDetention += Time.deltaTime;

            if (_currentTimeDetention >= _recoloringDetention)
            {
                _currentTimeDetention = 0f;
                GenerateNextColor();
                _isWaitRecoloring = !_isWaitRecoloring;
            }
            return;
        }
        
        _currentTimeDuration += Time.deltaTime;

        var progress = _currentTimeDuration / _recoloringDuration;
        var currentColor = Color.Lerp(_startColor, _nextColor, progress);
        _renderer.material.color = currentColor;

        if (_currentTimeDuration >= _recoloringDuration)
        {
            _currentTimeDuration = 0f;
            _isWaitRecoloring = !_isWaitRecoloring;
        }
    }

    private void GenerateNextColor()
    {
        _startColor = _renderer.material.color;
        _nextColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 0.5f);
    }
}