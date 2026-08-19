using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{

    /// <summary>
    /// 伤害契约：任何“可以受到伤害”的对象实现它（玩家，敌人，箱子。。。）
    /// 攻击方只调damageable.TakeDamage(damage),不关心目标是谁
    /// </summary>



    public interface IDamageable
    {
        void TakeDamage(float damage);

    }
}