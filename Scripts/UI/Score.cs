using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Shooter _shooter;

    private void OnEnable()
    {
        _shooter.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _shooter.ScoreChanged -= OnScoreChanged;
    }

    private void Start()
    {
        _scoreText.text = _shooter.Score.ToString();
    }

    private void OnScoreChanged(int score)
    {
        _scoreText.text = score.ToString();
    }
}
