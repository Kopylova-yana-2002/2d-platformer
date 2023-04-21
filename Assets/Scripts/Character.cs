using UnityEngine;
using System.Collections;

/**
 * Класс, реализующий основную логику играбельного персонажа
 */
public class Character : Unit
{
    [SerializeField]
    private int lives = 5;

    public int Lives
    {
        get { return lives; }
        set
        {
           if (value < 5) lives = value;
            livesBar.Refresh();
        }
    }
    private LivesBar livesBar;

    [SerializeField]
    private float speed = 3.0F;
    [SerializeField]
    private float jumpForce = 15.0F;

    private bool isGrounded = false;

    private Bullet bullet;

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;

    /**
     * Метод, вызывающийся при загрузке объекта
     * Подгружает необходимые компоненты
     */
    private void Awake()
    {
        livesBar = FindObjectOfType<LivesBar>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        bullet = Resources.Load<Bullet>("Bullet");
    }

    /**
     * Метод, вызывающийся каждый фиксированный прометжуток времени
     * Внутри вызывается проверка на нахождение персонажа на земле
     */
    private void FixedUpdate()
    {
        CheckGround();
    }

    /**
     * Метод, вызывающийся каждый кадр
     * Внутри обрабатывается нажатие клавиш и проверка состяния нахождения на земле
     */
    private void Update()
    {
        if (isGrounded) State = CharState.Idle;

        if (Input.GetButtonDown("Fire1")) Shoot();
        if (Input.GetButton("Horizontal")) Run();
        if (isGrounded && Input.GetButtonDown("Jump")) Jump();
    }

    /**
     * Метод для перемещения персонажа по горизонтали
     */
    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        sprite.flipX = direction.x < 0.0F;

        if (isGrounded) State = CharState.Run;
    }

    /**
     * Метод для совершения прыжка
     */
    private void Jump()
    {
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    /**
     * Метод для выстрела
     */
    private void Shoot()
    {
        Vector3 position = transform.position; position.y += 0.8F;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);
    }

    /**
     * Метод, обрабатывающий получение урона
     */
    public override void ReceiveDamage()
    {
        Lives--;

        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.up * 8.0F, ForceMode2D.Impulse);

        Debug.Log(lives);
    }

    /**
     * Метод проверяет находится ли персонаж на земле и меняет соответствующие поля
     */
    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);

        isGrounded = colliders.Length > 1;

        if (!isGrounded) State = CharState.Jump;
    }

    /**
     * Метод вызывающийся при столкновении с другим коллайдером
     * Проверяет попадание пули и получает урон если пуля попала
     * @param collider - объект коллайдера, с которым произошло столкновение
     */
    private void OnTriggerEnter2D(Collider2D collider)
    {

        Bullet bullet = collider.gameObject.GetComponent<Bullet>();
        if (bullet && bullet.Parent != gameObject)
        {
            ReceiveDamage();
        }
    }
}

/**
 * Перечисление, содержащее все состояния персонажа
 */
public enum CharState
{
    Idle,
    Run,
    Jump
}