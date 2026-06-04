using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private string MenuName;
    [SerializeField] private GameObject DeathScreen;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _timeScore;

    [Header("Obstacles")]
    [SerializeField] private Obstacle[] _obstacles;
    [SerializeField] private float _obstacleInterval = 3f;
    [SerializeField] private float _obstacleLifetime = 5f;
    [SerializeField] private float _indicatorDuration = 1f;

    private bool _doSetTimer = true;
    private float _timer;
    private bool _spawningActive;
    private float _nextSpawnTime;
    private int _currentObstacleIndex;

    private void Start()
    {
        StartObstacles();
    }
    void Update()
    {
        if (_doSetTimer)
        {
            _timer += Time.deltaTime;
            SetTimer();
        }

        if (_spawningActive && Time.time >= _nextSpawnTime)
        {
            SpawnNextObstacle();
            _nextSpawnTime = Time.time + _obstacleInterval;
        }
    }

    private void SpawnNextObstacle()
    {
        if (_obstacles.Length == 0) return;

        int attempts = 0;
        while (_obstacles[_currentObstacleIndex].IsActive && attempts < _obstacles.Length)
        {
            _currentObstacleIndex = (_currentObstacleIndex + 1) % _obstacles.Length;
            attempts++;
        }

        _obstacles[_currentObstacleIndex].Activate(_indicatorDuration, _obstacleLifetime);
        _currentObstacleIndex = (_currentObstacleIndex + 1) % _obstacles.Length;
    }

    public void StartObstacles()
    {
        _spawningActive = true;
        _nextSpawnTime = Time.time + _obstacleInterval;
    }

    public void StopObstacles()
    {
        _spawningActive = false;
        foreach (var obstacle in _obstacles)
            obstacle.Deactivate();
    }

    public void SetTimer() => _timerText.text = $"Time: {_timer:F0}";
    public void StopTimer() => _doSetTimer = false;
    public void ResetGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public void GoToMenu() => SceneManager.LoadScene(MenuName);

    public void OpenDeathMenu()
    {
        DeathScreen.SetActive(true);
        _timeScore.text = _timer.ToString("F1");
        StopObstacles();
    }

    public void CloseDeathMenu() => DeathScreen.SetActive(false);
}
