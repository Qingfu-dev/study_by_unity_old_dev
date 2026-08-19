using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Data
{
    /// <summary>
    /// 物品大类：决定它的处理逻辑
    /// </summary>
    public enum ItemType { Currency, Consumable, Equipment }


    /// <summary>
    /// 物品静态配置。关键：这是个“数据资产”不是逻辑
    /// Project里面右键，Creat，Game/ItemData就能创建
    /// 每个物品一个.asset文件，Inspector填数值即可
    /// </summary>
    [CreateAssetMenu(menuName = "Game/ItemData")]
    public class ItemData : ScriptableObject
    {
        [Header("基本信息")]
        public string itemName = "未命名";
        public Sprite icon;

        [Header("堆叠与占用")]
        public int maxStack = 1; //单格子最大堆叠
        public int slotsRequired = 1; //占几个背包格子

        [Header("类型")]
        public ItemType itemType;

        [Header("价值")]
        public int value = 1;

    }
}
