using UnityEngine;

/// <summary>
/// 子弹：向前飞行，撞到墙或敌人时销毁自己
/// </summary>
public class Bullet : MonoBehaviour
{
    [Header("子弹参数")]
    public float speed = 8f;        // 飞行速度（单位/秒）



    void Update()
    {
        // 每帧朝自己的正前方移动，乘 Time.deltaTime 保证与帧率无关
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    // 当有碰撞体进入自己的 Trigger 时，Unity 自动调用
    void OnTriggerEnter2D(Collider2D other)
    {
        // 撞到普通墙：子弹消失
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        // 撞到敌人：敌人消失 + 子弹消失
        else if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        // 【新增】撞到空气墙：子弹直接消失
        else if (other.CompareTag("AirWall"))
        {
            Destroy(gameObject);
        }
    }


}