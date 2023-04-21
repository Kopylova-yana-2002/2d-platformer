using UnityEngine;
using System.Collections;

/**
 * 
 */
public class ShootableMonster : Monster
{
    [SerializeField]
    private float rate = 2.0F;
    [SerializeField]
    private Color bulletColor = Color.white;

    private Bullet bullet;

    /**
     * Метод, вызывающийся при загрузке объекта
     * Подгружает необходимые компоненты
     */
    protected void Awake()
    {
        bullet = Resources.Load<Bullet>("Bullet");
    }

    /**
     * Метод, вызывающийся после загрузки объекта
     * Вызывает метод Shoot с интервалом rate
     */
    protected void Start()
    {
        InvokeRepeating("Shoot", rate, rate);
    }

    /**
     * Метод для выстрела, создает новую пулю и выстреливает в нужном направлении
     */
    private void Shoot()
    {
        Vector3 position = transform.position;          position.y += 0.5F;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

        newBullet.Parent = gameObject;
        newBullet.Direction = -newBullet.transform.right;
        newBullet.Color = bulletColor;
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
}
