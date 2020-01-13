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

        void Awake()
        {

        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


#if UNITY_EDITOR
        protected new void OnDrawGizmosSelected()
        {

            //绘制巡视范围范围
            if (viewDistance > 0)           
            {
                Handles.color = new Color(0, 1.0f, 0, 0.2f);
                Handles.DrawSolidDisc(transform.position, Vector3.back, viewDistance);
            }
            base.OnDrawGizmosSelected();

        }
#endif
    }

}
