using UnityEngine;
using System.Collections;


/**
 * @brief Класс пули (снаряда)
 */
public class Bullet : MonoBehaviour
{
    private GameObject parent;
    public GameObject Parent { set { parent = value; }  get { return parent; } }

    private float speed = 10.0F;
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }

    public Color Color
    {
        set { sprite.color = value; }
    }

    private SpriteRenderer sprite;

    /**
     * @brief Метод, вызывающийся при загрузке объекта
     * Подгружает необходимые компоненты
     */
    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    /**
     * @brief Метод, вызывающийся сразу после загрузки объекта
     * Ставит таймер в 1.5 секунды на уничтожение этой пули
     */
    private void Start()
    {
        Destroy(gameObject, 1.4F);
    }

    /**
     * @brief Метод, вызываемый каждый кадр
     * Перемещает пулю в сторону полета
     */
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    /**
     * @brief Метод вызывающийся при столкновении с другим коллайдером
     * Проверяет попадание пули в другой объект, уничтожая его если попадание есть
     * @param collider - объект коллайдера, с которым произошло столкновение
     */
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit.gameObject != parent)
        {
            Destroy(gameObject);
        }
    }
}
