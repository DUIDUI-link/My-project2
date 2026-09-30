using UnityEngine;

/// <summary>
/// 玩家：左右旋转、前后移动、按空格开火
/// </summary>
public class Player : MonoBehaviour
{
    [Header("移动参数")]
    public float moveSpeed = 3f;      // 前进/后退速度
    public float rotateSpeed = 180f;  // 旋转速度（度/秒）

    [Header("开火参数")]
    public GameObject bulletPrefab;   // 子弹预制体
    public Transform firePoint;       // 炮口位置
    public float fireCooldown = 0.2f; // 开火冷却（秒）

    private Rigidbody2D rb;
    private float nextFireTime = 0f;  // 下一次可开火的时间点

    void Start()
    {
        // 缓存刚体组件，避免每帧 GetComponent
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleRotation();
        HandleFire();
    }

    void FixedUpdate()
    {
        // 物理相关放在 FixedUpdate，比 Update 更稳定
        HandleMovement();
    }

    // ---------- 旋转 ----------
    void HandleRotation()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (h != 0)
        {
            // 按 D（h=1）顺时针转，按 A（h=-1）逆时针转
            float angle = -h * rotateSpeed * Time.deltaTime;
            transform.Rotate(0f, 0f, angle);
        }
    }

    // ---------- 移动 ----------
    void HandleMovement()
    {
        float v = Input.GetAxisRaw("Vertical");
        Vector2 dir = transform.up * v;
        rb.velocity = dir * moveSpeed;
    }

    // ---------- 开火 ----------
    void HandleFire()
    {
        // 按空格 + 冷却时间到，才能开火
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireCooldown;
        }
    }

    void Fire()
    {
        // 在炮口位置生成一颗子弹，朝向与炮口一致
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}