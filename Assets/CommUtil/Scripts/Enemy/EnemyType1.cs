
using UnityEngine;

namespace CommUtil.Scripts.Enemy
{
    //平台游戏类型的 Enemy
    [RequireComponent(typeof(BoxCollider2D))]
    public sealed class EnemyType1 : BaseEnemy
    {
        BoxCollider2D _mBoxCollider2D;
        private Rigidbody2D _mRigidBody2D;
        protected override void Awake()
        {
            _mBoxCollider2D = GetComponent<BoxCollider2D>();
            _mRigidBody2D = GetComponent<Rigidbody2D>();
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            moveSpeedX = 50;
        }

        protected override void OnMove()
        {
            if (CanMoveForward(_mBoxCollider2D))
            {
                MoveByHorizontalSpeed();
            }
            else
            {
                moveSpeedX = 0 - moveSpeedX;
            }
        }

        //水平移动
        private void MoveByHorizontalSpeed()
        {
            Vector2 velocity = _mRigidBody2D.velocity;
            velocity.x = Time.deltaTime * moveSpeedX;
            _mRigidBody2D.velocity = velocity;
        }
    }
}

