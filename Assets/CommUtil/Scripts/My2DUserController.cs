using CommUtil.Scripts.player;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace CommUtil.Scripts
{
    [RequireComponent(typeof(Platform2DPlayer))]
    public class My2DUserController : MonoBehaviour
    {
        private Platform2DPlayer _platform2DPlayer;

        void Awake()
        {
            _platform2DPlayer = GetComponent<Platform2DPlayer>();
        }

        void FixedUpdate()
        {
            CheckUserGesture();
        }

        //检测用户手势
        private void CheckUserGesture()
        {
            CheckAndHandleJumpAct();

            CheckAndHandleHorizontalAct();

            CheckAndHandleAttackAct();
        }

        //检测和处理攻击动作
        private void CheckAndHandleAttackAct()
        {
            if (Input.GetKey(KeyCode.K))
            {
                _platform2DPlayer.mAnimator.SetFloat("fAttack", 0.1f);
            }
            else if (Input.GetKey(KeyCode.L))
            {
                _platform2DPlayer.mAnimator.SetFloat("fAttack", 9.99f);
            }
            else
            {
                _platform2DPlayer.mAnimator.SetFloat("fAttack", -1);
            }
        }

        //检测和处理水平行为
        private void CheckAndHandleHorizontalAct()
        {
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            _platform2DPlayer.OnHorizontalMove(h);
            _platform2DPlayer.CheckFlip(h);
        }

        private int _numberOfJumpPress; //每次跳跃连续执行次数

        //检测和处理跳跃
        private void CheckAndHandleJumpAct()
        {
            if (CrossPlatformInputManager.GetButton("Jump"))//当按下不放
            {
                _numberOfJumpPress++;
                if(_numberOfJumpPress > 4)
                {
                    ConfirmJump();
                }

            }
            else
            {
                ConfirmJump();
            }

        }

        //确定跳跃
        private void ConfirmJump()
        {
            if (_numberOfJumpPress > 0)
            {
                _platform2DPlayer.OnClickJump(_numberOfJumpPress / 5f);
            }
            _numberOfJumpPress = 0;
        }
    }
}
