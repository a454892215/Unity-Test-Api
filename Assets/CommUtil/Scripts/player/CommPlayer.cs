using System;
using CommUtil.Scripts.Base;

namespace CommUtil.Scripts.player
{
    public class CommPlayer : BaseAnimal
    {
        public float jumpForce = 300f;

        private void OnDestroy()
        {
            SceneSwitcher.SwitchScene("Scene3");
        }
    }
    

}
