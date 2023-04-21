using UnityEngine;
using System.Collections;

/**
 * @brief Родительский класс для всех "живых" классов - игрока и монстров
 */
public class Unit : MonoBehaviour
{
    /**
     * @brief Базовый метод получения урона
     */
    public virtual void ReceiveDamage()
    {
        Die();
    }

    /**
     * @brief Метод вызываемый при смерти объекта
     */
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
