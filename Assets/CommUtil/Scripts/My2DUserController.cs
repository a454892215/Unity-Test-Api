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

        void FixedUpdate()
        {
            OnGenerateUserGesture();

        }

        //跳跃按钮按下的持续时间
        private float jumpButtonDownKeepTime = 0f;

        //当产生用户手势
        private void OnGenerateUserGesture()
        {
            bool isPress = CrossPlatformInputManager.GetButton("Jump");//当按下不放
            if (isPress)
            {
                jumpButtonDownKeepTime += Time.deltaTime;
                if(jumpButtonDownKeepTime > 0.15)
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

            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            platform2DPlayer.OnHorizontalMove(h);
            platform2DPlayer.CheckFlip(h);

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


    }
}
