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

        private int numberOfJumpPress = 0; //每次跳跃连续执行次数

        //检测和处理跳跃
        private void CheckAndHandleJumpAct()
        {
            if (CrossPlatformInputManager.GetButton("Jump"))//当按下不放
            {
                numberOfJumpPress++;

            }
            else
            {
                if (numberOfJumpPress > 0) 
                {
                    int factor = Mathf.Clamp(numberOfJumpPress, 1, 10);
                    platform2DPlayer.OnClickJump(factor/10f);
                }
                numberOfJumpPress = 0;
            }

        }
    }
}
