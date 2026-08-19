using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interaction
{

    /// <summary>
    /// 交互契约：任何“玩家靠近按E能操作”的物体实现它
    /// </summary>


    public interface IInterctable
    {
        void OnInteract(PlayerController player);//按E时做什么
        string GetPrompt();//提示条显示什么
    }

}
