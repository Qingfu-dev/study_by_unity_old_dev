using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//玩家能被打，所以实现IDamageable：
public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxhealth = 100f;
    private float _heath;

    void IDamageable.TakeDamage(float damage)
    {
        _heath -= damage;
        Debug.Log($"玩家受到{damage}伤害，剩余{_heath}");
        if (_heath <= 0) Die();

    }
    private void Die()
    {
        /*死亡逻辑*/
    }


}
