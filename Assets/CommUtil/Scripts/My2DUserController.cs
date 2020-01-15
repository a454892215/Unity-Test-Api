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

        protected void Start()
        {
        }

        protected void Update()
        {
           

        }

        //跳跃按钮按下的持续时间
        private float jumpButtonDownKeepTime = 0f;

        //当产生用户手势
        private void OnGenerateUserGesture()
        {
            if (!platform2DPlayer.m_IsJump)
            {
                // Read the jump input in Update so button presses aren't missed.
                platform2DPlayer.m_IsJump = CrossPlatformInputManager.GetButtonUp("Jump");
                if (CrossPlatformInputManager.GetButton("Jump"))
                {
                    jumpButtonDownKeepTime += Time.deltaTime;
                }
                else
                {
                    //当抬起跳跃按钮时候触发
                    float realValidTime = Mathf.Clamp(jumpButtonDownKeepTime * 6, 0.6f, 1.5f);
                    platform2DPlayer.handleJump(realValidTime);
                    jumpButtonDownKeepTime = 0;
                }
                print("==========================jumpButtonDownKeepTime:" + jumpButtonDownKeepTime);
            }

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

        void FixedUpdate()
        {
            OnGenerateUserGesture();
            // 是否按下左边Ctrl
            // bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = platform2DPlayer.OnHorizontalMove();
            platform2DPlayer.CheckFlip(h);
        }
    }
}
