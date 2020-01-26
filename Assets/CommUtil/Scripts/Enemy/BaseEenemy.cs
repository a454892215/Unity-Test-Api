using CommUtil.Scripts.Base;
using CommUtil.Scripts.utils;
using UnityEditor;
using UnityEngine;

namespace CommUtil.Scripts.Enemy
{
    public class BaseEenemy : BaseAnimal
    {
        public int viewDistance = 5; //巡视范围

        [Range(0.0f, 360.0f)]
        public float viewAngle = 120; //巡视角度

        [Range(0.0f, 360.0f)]
        public float viewDirection;

        //检测是否可以前进 :如果自身的左边或者右边没有与地面碰撞，则表示到了边界，不能继续向前
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
                Vector3 forward = moveSpeedX < 0 ? Vector2.left : Vector2.right;
                forward = Quaternion.Euler(0, 0, moveSpeedX < 0 ? -viewDirection : viewDirection) * forward;
                Vector3 endpoint = transform.position + (Quaternion.Euler(0, 0, viewAngle * 0.5f) * forward);
                Handles.DrawSolidArc(transform.position, -Vector3.forward, (endpoint - transform.position).normalized, viewAngle, viewDistance);
            }
            base.OnDrawGizmosSelected();

        }
#endif
    }

}
