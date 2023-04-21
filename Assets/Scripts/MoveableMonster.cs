using UnityEngine;
using System.Collections;
using System.Linq;

/**
 * Класс движущегося монстра
 */
public class MoveableMonster : Monster
{
    [SerializeField]
    private float speed = 2.0F;

    private Vector3 direction;
    

    private SpriteRenderer sprite;

    /**
     * Метод, вызывающийся при загрузке объекта
     * Подгружает необходимые компоненты
     */
    protected void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    /**
     * Метод, вызывающийся после загрузки объекта
     * Запоминает направление движения
     */
    protected void Start()
    {
        direction = transform.right;
    }

    /**
     * Метод, вызывающийся в каждом кадре
     * Передвигает монстра
     */
    protected void Update()
    {
        Move();
    }

    /**
     * Метод вызывающийся при столкновении с другим коллайдером
     * Проверяет столкновение с игроком
     * Если игрок прыгнул сверху -> монстр получает урон
     * Если игрок столкнулся иначе -> игрок получает урон
     * @param collider - объект коллайдера, с которым произошло столкновение
     */
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit is Character)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.3F) ReceiveDamage();
            else unit.ReceiveDamage();
        }
    }

    /**
     * Метод для передвижения монстра
     * Меняет направление движения если монстр дошел до стены
     */
    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.5F + transform.right * direction.x * 0.5F, 0.1F);

        if (colliders.Length > 0 && colliders.All(x => !x.GetComponent<Character>())) direction *= -1.0F;
        
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
}
