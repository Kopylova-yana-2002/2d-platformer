using UnityEngine;
using System.Collections;

/**
 * @brief Класс неподвижного противника
 */
public class Monster : Unit
{
    /**
     * @brief Метод вызывающийся при столкновении с другим коллайдером
     * Проверяет столкновение с другим объектом
     * Если было столкновение с пулей -> монстр получает урон
     * Если было столкновение с игроком -> игрок получает урон
     * @param collider - объект коллайдера, с которым произошло столкновение
     */
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet bullet = collider.GetComponent<Bullet>();

        if (bullet)
        {
            ReceiveDamage();
        }

        Character character = collider.GetComponent<Character>();

        if (character)
        {
            character.ReceiveDamage();
        }
    }
}
