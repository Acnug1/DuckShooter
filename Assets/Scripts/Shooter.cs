using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Hit _hit;
    [SerializeField] private float _hitLifeTime;

    private Camera _camera;
    private int _score = 0;

    public int Score => _score;

    public event UnityAction<int> ScoreChanged;
    public event UnityAction TimeAdded;

    private void Start()
    {
        _camera = Camera.main; // находим main камеру
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition; // записываем в Vector3 координаты нахождения курсора мыши
            Ray shootingRay = _camera.ScreenPointToRay(mousePosition); // кидаем луч из камеры (области видимости) в позицию курсора мыши
            RaycastHit2D hitInfo = Physics2D.Raycast(shootingRay.origin, shootingRay.direction); // записываем информацию о попадании нашим Raycast в RaycastHit2D hitInfo

            if (hitInfo.collider != null && hitInfo.collider.TryGetComponent(out Target target) && Time.timeScale != 0) // если наш Raycast попал в цель, у которой есть коллайдер и на нем есть компонент Target target
            {
                target.Hit(); // то мы попали по нашей цели (вызываем у неё анимацию попадания)

                _score++;
                ScoreChanged?.Invoke(_score);
                TimeAdded?.Invoke();
            }
            else if (hitInfo.collider != null && Time.timeScale != 0)
            {
                Vector3 hitPosition = _camera.ScreenToWorldPoint(Input.mousePosition); // переводим позицию мыши с локальных координат области видимости камеры в мировые
                Hit hitPoint = Instantiate(_hit, new Vector3(hitPosition.x, hitPosition.y, transform.position.z), Quaternion.identity, hitInfo.collider.gameObject.transform); // создаем коллизию попадания в позиции мыши и делаем её дочерней к объекту попадания
                int hitPointLayer = hitInfo.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder; // находим слой объекта, в который мы попали
                hitPoint.GetComponent<SpriteRenderer>().sortingOrder = hitPointLayer + 1; // делаем коллизию нашего попадания следующим слоем над объектом попадания

                StartCoroutine(DestroyHitPoint(hitPoint));
            }
        }
    }

    private IEnumerator DestroyHitPoint(Hit hitPoint)
    {
        var waitForSeconds = new WaitForSeconds(_hitLifeTime);
        yield return waitForSeconds;

        Destroy(hitPoint.gameObject);
    }
}
