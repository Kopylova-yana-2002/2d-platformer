using UnityEngine;
using System.Collections;

/**
 * @brief Класс препятствия, наносящего урон
 */
public class Obstacle : MonoBehaviour
{
    /**
     * @brief Метод вызывающийся при столкновении с другим коллайдером
     * Проверяет столкновение с игроком и наносит ему урон
     * @param collider - объект коллайдера, с которым произошло столкновение
     */
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit is Character)
        {
            unit.ReceiveDamage();
        }
    }
}
