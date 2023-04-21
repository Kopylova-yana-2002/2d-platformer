using UnityEngine;
using System.Collections;

/**
 * @brief Класс описывающий полоску жизней игрока
 */
public class LivesBar : MonoBehaviour
{
    private Transform[] hearts = new Transform[5];

    private Character character;

    /**
     * @brief Метод, вызывающийся при загрузке объекта
     * Подгружает необходимые компоненты
     */
    private void Awake()
    {
        character = FindObjectOfType<Character>();

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = transform.GetChild(i);
            Debug.Log(hearts[i]);
        }
    }
    /**
     * @brief Метод для обновления полоски жизней
     * Отрисовывает полные сердчеки по количеству жизней
     */
    public void Refresh()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < character.Lives) hearts[i].gameObject.SetActive(true);
            else hearts[i].gameObject.SetActive(false);
        }
    }
}
