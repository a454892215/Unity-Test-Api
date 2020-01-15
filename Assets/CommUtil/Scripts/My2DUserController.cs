using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameComm
{
    [RequireComponent(typeof(Platform2DPlayer))]
    public class My2DUserController : MonoBehaviour
    {
        private Platform2DPlayer platform2DPlayer;

        void Awake()
        {
            platform2DPlayer = GetComponent<Platform2DPlayer>();
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

            CheckAndHandleActtckAct();
        }

        //检测和处理攻击动作
        private void CheckAndHandleActtckAct()
        {
            if (Input.GetKey(KeyCode.K))
            {
                platform2DPlayer.m_Animator.SetFloat("fAttack", 0.1f);
            }
            else if (Input.GetKey(KeyCode.L))
            {
                platform2DPlayer.m_Animator.SetFloat("fAttack", 9.99f);
            }
            else
            {
                platform2DPlayer.m_Animator.SetFloat("fAttack", -1);
            }
        }

        //检测和处理水平行为
        private void CheckAndHandleHorizontalAct()
        {
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            platform2DPlayer.OnHorizontalMove(h);
            platform2DPlayer.CheckFlip(h);
        }

        //跳跃按钮按下的持续时间
        private float jumpButtonDownKeepTime = 0f;

        //检测和处理跳跃
        private void CheckAndHandleJumpAct()
        {
            bool isPressJump = CrossPlatformInputManager.GetButton("Jump");//当按下不放
            if (isPressJump)
            {
                jumpButtonDownKeepTime += Time.deltaTime;
                if (jumpButtonDownKeepTime > 0.15)
                {
                    platform2DPlayer.OnClickJump(1);
                }
            }
            else
            {
                jumpButtonDownKeepTime = 0;
            }

            if (CrossPlatformInputManager.GetButtonUp("Jump"))
            {
                platform2DPlayer.OnClickJump(0.7f);
            }
        }
    }
}
