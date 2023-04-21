using UnityEngine;
using System.Collections;

/**
 * @brief Класс управления камерой
 * Позволяет камере следить за игроком
 */
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0F;

    [SerializeField]
    private Transform target;

    /**
     * @brief Метод, вызывающийся при загрузке объекта
     * Подгружает необходимые компоненты
     */
    private void Awake()
    {
        if (!target) target = FindObjectOfType<Character>().transform;
    }

    /**
     * @brief Метод, вызывающийся каждый кадр
     * Внутри перемещение игрока преобразуется в движение камеры
     */
    private void Update()
    {
        Vector3 position = target.position;         position.z = -10.0F;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    }
}
