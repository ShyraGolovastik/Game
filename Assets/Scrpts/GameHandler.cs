using System;
using TMPro;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private Canvas _pauseCanvas;
    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] private float _scoresPerSecond;
    [SerializeField] private float _health;
    [SerializeField] private TextMeshProUGUI _healthText;
    public event Action OnGameStart;
    public event Action OnGameOver;

    public void StartGame() => OnGameStart();
    
    public void EndGame() => OnGameOver();
    public PlayerMover Player { get; set; }
    public float ScoresPerSecond => _scoresPerSecond;
    public bool IsPaused { get; set; }
    private void Awake()
    {
        Player = FindAnyObjectByType<PlayerMover>();
        Player.OnHealthChanged = (x) => _healthText.text = x.ToString();
        Player.Hp = _health;

        OnGameOver += () => _gameOverCanvas.enabled = true;
        OnGameOver += () => Player.enabled = false;
        OnGameStart += () => Player.enabled = true;
      
    } 
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Time.timeScale = 1.0f;
                IsPaused = false;
                _pauseCanvas.enabled = false;
            }
            else
            {
                IsPaused = true;
                _pauseCanvas.enabled = true;
                Time.timeScale = 0.0f;
            }
        }
    }
}
