using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // 在x轴能移动的最大速度
        [SerializeField] private float m_JumpForce = 400f;                  // 跳跃施加的力度
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  //蹲伏速度百分比
        [SerializeField] private bool m_AirControl = false;                 // 跳跃时候是否可以转向
       // [SerializeField] private LayerMask m_WhatIsGround;                  // 是否在地面的LayerMask

        private Transform m_GroundCheck;    // 标记角色是否在地面的Transform
        const float k_GroundedRadius = .2f; // 重叠圆半径 确定是否在地面 Radius of the overlap circle to determine if grounded  
        private bool m_Grounded;            // 玩家是否在地面  Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // 标记检查天花板位置的Transform position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // 重叠圆半径 确定是否跳跃 Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Animator 控件
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // 是否面向右边 For determining which way the player is currently facing.

        private void Awake()
        {
            // 找到相关控件
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void FixedUpdate()
        {
            m_Grounded = false; //默认不在地面上

            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius);
            for (int i = 0; i < colliders.Length; i++)
            {
                //如果角色脚部检测到任何一个碰撞 ，则确定在地面上
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            //设置动画属性在地面
            m_Anim.SetBool("Ground", m_Grounded);

            // 设置动画Y轴的速度属性在地面
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }


        public void Move(float move, bool crouch, bool jump)
        {
            // 如果蹲伏 检查玩家是否站立 If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius))
                {
                    crouch = true; // 设置为蹲伏状态
                }
            }

            // 设置是否蹲伏动画
            m_Anim.SetBool("Crouch", crouch);

            //如果玩家在地面 或者跳跃的时候可以转向 only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // 如果正在蹲伏 就减速 Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // 设置速度动画参数 The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // 移动角色 Move the character
                m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // 如果玩家不是面向右边 则向右 If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    Flip();
                }
                    // 向左 Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    Flip();
                }
            }
            // 玩家是否跳跃 If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                print("===================m_JumpForce:" + m_JumpForce);
            }
        }

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
