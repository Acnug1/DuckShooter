using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main; // находим main камеру
        Cursor.visible = false; // отключаем видимость курсора мыши
    }

    private void Update()
    {
        Vector3 crosshairPosition = _camera.ScreenToWorldPoint(Input.mousePosition); // трансформируем локальную позицию курсора на камере в координаты мирового пространства в Unity
        transform.position = new Vector3(crosshairPosition.x, crosshairPosition.y, transform.position.z); // двигаем наше перекрестье за нашим курсором в мировых координатах
    }
}
