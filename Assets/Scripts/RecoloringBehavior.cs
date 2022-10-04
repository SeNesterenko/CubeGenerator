using UnityEngine;
using Random = UnityEngine.Random;

public class RecoloringBehavior : MonoBehaviour
{
    [SerializeField] private float _recoloringDuration = 2f;
    [SerializeField] private int _recoloringDetention;

    private Color _startColor;
    private Color _nextColor;
    private Renderer _renderer;

    private float _currentTime;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        GenerateNextColor();
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        var progress = _currentTime / _recoloringDuration;
        var currentColor = Color.Lerp(_startColor, _nextColor, progress);
        _renderer.material.color = currentColor;

        if (_currentTime >= _recoloringDuration)
        {
            Invoke("DelayRecoloringTime", _recoloringDetention);
        }
    }

    private void DelayRecoloringTime()
    {
        _currentTime = 0f;
        GenerateNextColor();
    }

    private void GenerateNextColor()
    {
        _startColor = _renderer.material.color;
        _nextColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 0.5f);
    }
}