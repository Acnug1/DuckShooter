using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _timeLeft;
    [SerializeField] private TMP_Text _timer;
    [SerializeField] private Shooter _shooter;
    [SerializeField] private Image _plusImage;
    [SerializeField] private Image _numberImage;

    private bool _gameOver = false;

    public event UnityAction TimeLefted;

    private void OnEnable()
    {
        _shooter.TimeAdded += OnTimeAdded;
    }

    private void OnDisable()
    {
        _shooter.TimeAdded -= OnTimeAdded;
    }

    private void Start()
    {
        _timer.text = _timeLeft.ToString();

        Color color = _plusImage.color; // во временную переменную структуры color записываем значение цвета _plusImage (превращаем структуру в переменную, для того, чтобы можно было изменить её значение)
        color.a = 0; // изменяем альфу в 0
        _plusImage.color = color; // присваиваем структуре значение измененной переменной color
        _numberImage.color = color;
    }

    private void Update()
    {
        if (_timeLeft > 0)
        {
            _timer.text = _timeLeft.ToString();
            _timeLeft -= Time.deltaTime;
        }
        else if (_timeLeft <= 0 && _gameOver == false)
        {
            OnGameOver();
        }
    }

    private void OnTimeAdded()
    {
        _timeLeft++;
        Sequence sequence = DOTween.Sequence(); // создаем новую последовательность для Dotween анимации
        sequence.Append(_plusImage.DOFade(1, 0.3f).SetRelative()); // делаем при попадании по цели анимацию появления индикатора добавленного времени и запоминаем конечное значение времени воспроизведения анимации
        sequence.Insert(0, _numberImage.DOFade(1, 0.3f).SetRelative()); // с нулевой секунды также добавляем анимацию для другой картинки
        sequence.Append(_plusImage.DOFade(0, 0.3f)); // с предыдущего конечного значения снижаем alpha до 0
        sequence.Insert(0.3f, _numberImage.DOFade(0, 0.3f)); // с 0.3 секунды (по окончанию предыдущей анимации) снижаем alpha до 0
    }

    private void OnGameOver()
    {
        _gameOver = true;
        _timer.text = "0";
        TimeLefted?.Invoke();
    }
}
