using UnityEngine;
using System.Collections;

/**
 * Класс подбираемого сердечка для восстановления здоровья
 */
public class Heart : MonoBehaviour
{
    /**
     * Метод вызывающийся при столкновении с другим коллайдером
     * Проверяет столкновение с игроком -> добавляет игроку одно здоровье
     * @param collider - объект коллайдера, с которым произошло столкновение
     */
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();
        
        if (character)
        {
            character.Lives++;
            Destroy(gameObject);
        }
    }
}
