using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameComm
{
    public class Platform2DPlayer : CommPlayer
    {
        private Transform m_GroundCheckTransform;

        public float isGroundedCheckRadius = .2f; // 重叠圆半径 确定是否在地面
        public float x_MaxSpeed = 2f;
        public bool m_IsJump { get; set; }
        private bool m_IsGrounded;            // 玩家是否在地面  

        protected override void Awake()
        {
            base.Awake();
            m_GroundCheckTransform = transform.Find("GroundCheck");
        }
        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        //水平移动 值域：[-1,1]
        public float OnHorizontalMove()
        {
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            if (h != 0)
            {
                m_Rigidbody2D.velocity = new Vector2(h * x_MaxSpeed, m_Rigidbody2D.velocity.y);
            }
            m_Animator.SetFloat("xSpeed", Math.Abs(h));
            return h;
        }


        public void handleJump()
        {
            m_IsGrounded = false; //默认不在地面上
            //获取在一个圆的半径范围内的collider
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheckTransform.position, isGroundedCheckRadius);
            for (int i = 0; i < colliders.Length; i++)
            {
                //如果角色脚部检测到任何一个非角色碰撞 并且Y轴速度小于0.01f（避免与可以向上跳的台阶碰撞误判）
                if (colliders[i].gameObject != gameObject && Math.Abs(m_Rigidbody2D.velocity.y) < 0.01f)
                    m_IsGrounded = true;
            }
            if (m_IsJump && m_IsGrounded)
            {
                m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce)); //跳跃会和MovePosition冲突
                                                                    // print("==========AddForce===========" + transform.position.y);
            }
            m_IsJump = false;
            m_Animator.SetBool("isGround", m_IsGrounded);
        }


        public void CheckFlip(float h)
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
