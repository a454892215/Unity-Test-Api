using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyGameComm
{
 
    public class BaseEenemy : BaseAnimal
    {
        public int viewDistance = 5; //巡视范围
        public int viewAngle = 120; //巡视角度

        protected override void Awake()
        {
            base.Awake();
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

        //检测是否可以前进
        protected bool CanMoveForward(Collider2D collider2D)
        {
            int dir = moveSpeedX > 0 ? 1 : -1;
            Vector3 start = transform.position + Vector3.right * (collider2D.bounds.size.x * 0.6f * dir);
            Vector3 end = start + Vector3.down * 0.5f;
            RaycastHit2D raycastHit2D = RayUtil.CastLine(start, end, "Ground");
            return raycastHit2D.collider != null;
        }

#if UNITY_EDITOR
        protected new void OnDrawGizmosSelected()
        {

            //绘制巡视范围范围
            if (viewDistance > 0)           
            {
                Handles.color = new Color(0, 1.0f, 0, 0.2f);//透明绿
                Handles.DrawSolidDisc(transform.position, Vector3.back, viewDistance);
            }
            base.OnDrawGizmosSelected();

        }
#endif
    }

}
