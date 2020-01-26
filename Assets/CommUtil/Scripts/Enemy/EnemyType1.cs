using CommUtil.Scripts.utils;
using UnityEngine;

namespace CommUtil.Scripts.Enemy
{
    //平台游戏类型的 Enemy
    [RequireComponent(typeof(BoxCollider2D))]
    public class EnemyType1 : BaseEenemy
    {
        BoxCollider2D _mBoxCollider2D;
        protected override void Awake()
        {
            _mBoxCollider2D = GetComponent<BoxCollider2D>();
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            moveSpeedX = 50;
        }

        // Update is called once per frame
        protected override void Update()
        {
            if (MouseUtil.IsMouseLeftDown())
            {
                CanMoveForward(_mBoxCollider2D);
            }
            CanMoveForward(_mBoxCollider2D);
        }


        protected override void OnMove()
        {
            //  
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
        protected virtual void MoveByMovePosition()
        {
            Vector2 position = MRigidbody2D.position;
            position.x = position.x + Time.deltaTime * moveSpeedX; //Time.deltaTime * moveSpeedX 每一帧移动距离
            MRigidbody2D.MovePosition(position);
        }
        //水平移动
        protected virtual void MoveByHorizontalSpeed()
        {
            Vector2 velocity = MRigidbody2D.velocity;
            velocity.x = Time.deltaTime * moveSpeedX;
            MRigidbody2D.velocity = velocity;
        }
    }
}

