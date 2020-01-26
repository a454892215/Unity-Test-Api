using System;
using UnityEngine;

namespace CommUtil.Scripts.player
{
    public class Platform2DPlayer : CommPlayer
    {
        private Transform _mGroundCheckTransform;
        public float isGroundedCheckRadius = .2f; // 重叠圆半径 确定是否在地面
        public float xMaxSpeed = 2f;
        public float xMaxLimitSpeed = 3.5f;
        public bool mIsGrounded; // 玩家是否在地面  
        private Rigidbody2D _mRigidBody2D;

        protected override void Awake()
        {
            base.Awake();
            _mGroundCheckTransform = transform.Find("GroundCheck");
            _mRigidBody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            UpdatePlayerIsGround();
        }

        //更新玩家是否在地面的状态
        private void UpdatePlayerIsGround()
        {
            mIsGrounded = false; //默认不在地面上
            //获取在一个圆的半径范围内的collider
            Collider2D[] colliderArr =
                Physics2D.OverlapCircleAll(_mGroundCheckTransform.position, isGroundedCheckRadius);
            foreach (var t in colliderArr)
            {
                //如果角色脚部检测到任何一个非角色碰撞 并且Y轴速度小于0.01f（避免与可以向上跳的台阶碰撞误判）
                if (t.gameObject != gameObject && Math.Abs(_mRigidBody2D.velocity.y) < 0.01f)
                    mIsGrounded = true;
            }

            mAnimator.SetBool(kIsGround, mIsGrounded);
        }

        // Update is called once per frame

        //水平移动 值域：[-1,1]
        public void OnHorizontalMove(float h)
        {
            if (Mathf.Abs(h) > 0.01f)
            {
                float xSpeed = Mathf.Clamp(_mRigidBody2D.velocity.x + h * xMaxSpeed, -xMaxLimitSpeed, xMaxLimitSpeed);
                _mRigidBody2D.velocity = new Vector2(xSpeed, _mRigidBody2D.velocity.y);
            }

            mAnimator.SetFloat(kXSpeed, Math.Abs(h));
        }

        //当点击跳跃
        public void OnClickJump(float jumpForceFactor)
        {
            if (mIsGrounded)
            {
                float horizontalForce = _mRigidBody2D.velocity.x * 100;
                _mRigidBody2D.AddForce(new Vector2(horizontalForce, jumpForce * jumpForceFactor)); //跳跃会和MovePosition冲突
            }
        }

        //转向判断
        public void CheckFlip(float h)
        {
            // 如果玩家不是面向右边 则向右 
            if (h > 0 && !_mFacingRight) Flip();
            else if (h < 0 && _mFacingRight) Flip();
        }

        private bool _mFacingRight = true; // 是否面向右边
        private static readonly int kIsGround = Animator.StringToHash("isGround");
        private static readonly int kXSpeed = Animator.StringToHash("xSpeed");
        private SpriteRenderer _spriteRenderer;

        //转向
        private void Flip()
        {
            _mFacingRight = !_mFacingRight;
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }
}