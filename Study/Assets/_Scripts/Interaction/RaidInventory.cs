using Core;
using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Interaction
{

    /// <summary>
    /// 背包纯数据类：不继承MonoBehaviour,不依赖场景，
    /// 像普通字段一样被GameManager/RaidManager持有。
    /// 任何修改都会触发OnChanged--UI订阅它即时刷新，绝不轮询
    /// </summary>
    [Serializable]
    public class RaidInventory
    {
        /// <summary>
        /// 数据变化事件：AddItem/RemoveItem/Swap/Clear 后触发
        /// </summary>
        public event Action OnChange;

        public int maxSlots = 4; //总格数(装备扩容时增大)

        private readonly List<ItemSlot> _slots = new List<ItemSlot>();
        public IReadOnlyList<ItemSlot> Slots => _slots;//只读暴露，放篡改

        public int UsedSlots           //已用格数 = 每种物品占用格数之和
        {
            get
            {
                int used = 0;
                foreach (var s in _slots) used += Mathf.Max(1, s.item.slotsRequired);
                return used;
            }
        }

        public int RemainingSlots => Mathf.Max(0, maxSlots - UsedSlots);

        /// <summary>
        /// 添加物品：优先堆叠已有格子，堆满开新格：
        /// 空间不足 -> [整次拒绝]返回false （物品留在地面，不让拾取方为难）
        /// </summary>

        public bool AddItem(ItemData item, int amount)
        {
            if (item == null || amount <= 0) return false;

            //第0步：预演--算出已有格子最多还能叠多少
            int remaining = amount;
            foreach (var s in _slots)
            {
                if (s.item != item) continue;
                int can = item.maxStack - s.amount;//这格子还能装多少个 
                remaining -= Mathf.Min(Mathf.Max(0, can), remaining);
            }
            int needNew = remaining > 0 ? Mathf.CeilToInt((float)remaining / item.maxStack) : 0;

            //第一步：容量检查--不过就整次拒绝
            if (needNew * Mathf.Max(1, item.slotsRequired) > RemainingSlots) return false;

            //第二步：正真写入（先堆叠，再开新格）
            remaining = amount;
            foreach (var s in _slots)
            {
                if (s.item != item || s.amount >= item.maxStack) continue;
                int add = Mathf.Min(item.maxStack - s.amount, remaining);
                s.amount += add; remaining -= add;
            }
            while (remaining > 0)
            {
                int put = Mathf.Min(item.maxStack, remaining);
                _slots.Add(new ItemSlot(item, put));
                remaining -= put;

            }
            OnChange?.Invoke(); //数据变了喊一下UI刷新
            return true;

        }

    }
}