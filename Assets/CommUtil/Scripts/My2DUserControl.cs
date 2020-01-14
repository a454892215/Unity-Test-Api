using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameComm
{
    public class My2DUserControl : Platform2DPlayer
    {
        private bool m_IsJump;
        private Transform m_GroundCheckTransform;
        private bool m_IsGrounded;            // 玩家是否在地面  
        public float  isGroundedCheckRadius = .2f; // 重叠圆半径 确定是否在地面
        public float x_MaxSpeed = 2f;
        public float jumpForce = 300f;

        protected override void Start()
        {
            m_GroundCheckTransform = transform.Find("GroundCheck");
        }

        protected override void Update()
        {
            if (!m_IsJump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_IsJump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
    
            if (Input.GetKey(KeyCode.K))
            {
                m_Animator.SetFloat("fAttack", 0.1f);
            }
            else if (Input.GetKey(KeyCode.L))
            {
                m_Animator.SetFloat("fAttack", 9.99f);
            }
            else
            {
                m_Animator.SetFloat("fAttack", -1);
            }

            /* if (CrossPlatformInputManager.GetButtonDown("Fire2"))
             {
                 m_Animator.SetInteger("attack", 2);
             }*/
        }


        protected override void FixedUpdate()
        {
            m_IsGrounded = false; //默认不在地面上
            //获取在一个圆的半径范围内的collider
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheckTransform.position, isGroundedCheckRadius);
            for (int i = 0; i < colliders.Length; i++)
            {
                //如果角色脚部检测到任何一个非角色碰撞 并且Y轴速度小于0.01f（避免与可以向上跳的台阶碰撞误判）
                if (colliders[i].gameObject != gameObject &&  Math.Abs(m_Rigidbody2D.velocity.y) < 0.01f)
                    m_IsGrounded = true;
            }
           // print("==========AddForce===========velocity.y" + m_Rigidbody2D.velocity.y);
            if (m_IsJump && m_IsGrounded)
            {
                m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce)); //跳跃会和MovePosition冲突
               // print("==========AddForce===========" + transform.position.y);
            }

            m_Animator.SetBool("isGround", m_IsGrounded);
            m_IsJump = false;
            // 是否按下左边Ctrl
            bool crouch = Input.GetKey(KeyCode.LeftControl);

            //水平移动 值域：[-1,1]
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            if (h != 0)
            {
                m_Rigidbody2D.velocity = new Vector2(h * x_MaxSpeed, m_Rigidbody2D.velocity.y);
            }
            m_Animator.SetFloat("xSpeed", Math.Abs(h));
            CheckFlip(h);

        }

        private void CheckFlip(float h)
        {
            // 如果玩家不是面向右边 则向右 
            if (h > 0 && !m_FacingRight) Flip();
            else if (h < 0 && m_FacingRight) Flip();
        }

        private bool m_FacingRight = true;  // 是否面向右边

        //转向
        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
