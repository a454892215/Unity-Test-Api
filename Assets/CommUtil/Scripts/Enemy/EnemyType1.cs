using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameComm
{
    //平台游戏类型的 Enemy
    [RequireComponent(typeof(BoxCollider2D))]
    public class EnemyType1 : BaseEenemy
    {
        BoxCollider2D m_BoxCollider2D;
        protected override void Awake()
        {
            base.Awake();
            m_BoxCollider2D = GetComponent<BoxCollider2D>();
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            moveSpeedX = 50;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            if (MouseUtil.IsMouseLeftDown())
            {
                CanMoveForward(m_BoxCollider2D);
            }
            CanMoveForward(m_BoxCollider2D);
        }


        protected override void OnMove()
        {
            //  
            if (CanMoveForward(m_BoxCollider2D))
            {
                moveByHorizontalSpeed();
            }
            else
            {
                moveSpeedX = 0 - moveSpeedX;
            }




        }
        //水平移动
        protected virtual void moveByMovePosition()
        {
            Vector2 position = m_Rigidbody2D.position;
            position.x = position.x + Time.deltaTime * moveSpeedX; //Time.deltaTime * moveSpeedX 每一帧移动距离
            m_Rigidbody2D.MovePosition(position);
        }
        //水平移动
        protected virtual void moveByHorizontalSpeed()
        {
            Vector2 velocity = m_Rigidbody2D.velocity;
            velocity.x = Time.deltaTime * moveSpeedX;
            m_Rigidbody2D.velocity = velocity;
        }
    }
}

