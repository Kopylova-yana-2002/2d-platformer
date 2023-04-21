using UnityEngine;
using System.Collections;

/**
 * Родительский класс для всех "живых" классов - игрока и монстров
 */
public class Unit : MonoBehaviour
{
    /**
     * Базовый метод получения урона
     */
    public virtual void ReceiveDamage()
    {
        Die();
    }

    /**
     * Метод вызываемый при смерти объекта
     */
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
