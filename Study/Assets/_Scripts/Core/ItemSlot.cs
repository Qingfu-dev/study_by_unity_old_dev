using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core
{   ///<summary>背包的“一格”：物品引用+数量。纯数据，可序列化</summary>
    [System.Serializable]
    public class ItemSlot
    {
        public ItemData item;
        public int amount;

        public ItemSlot()//给序列化用
        {

        }
        public ItemSlot(ItemData item, int amount)//创建给代码用
        {
            this.item = item; this.amount = amount;
        }


    }
}