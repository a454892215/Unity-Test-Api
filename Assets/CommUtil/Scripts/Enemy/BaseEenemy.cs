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
        protected void CanMoveForward(Collider2D collider2D)
        {
            Bounds bounds = collider2D.bounds;
            Vector3 size = bounds.size;
          //  Ray ray = new Ray(bounds.center, Vector3.down);  //指定原点和方向
          //  Physics.Linecast(bounds.center, bounds.center + Vector3.down);

            Debug.DrawRay(transform.position, Vector2.down * 0.51f, Color.red);
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.down, 0.15f);
            print("===========size:" + raycastHit2D.collider == null);
            //  Transform transform_collider2D = collider2D.transform;
            //  transform_collider2D.InverseTransformPoint(collider2D.bounds.center);

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
