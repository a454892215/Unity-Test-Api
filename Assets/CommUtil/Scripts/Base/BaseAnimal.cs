using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyGameComm
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class BaseAnimal : MonoBehaviour
    {
        public int maxHp = 100; //最大血量
        public int maxAttack = 10; //最大攻击力
        public int maxDefence = 10; //最大防御力

        public int currentHp;
        public int currentAttack;
        public int currentDefence;

        public int attackType1Range = 2; //攻击类型1范围
        public float moveVelocity = 1f; //移动速度

        protected Rigidbody2D m_Rigidbody2D;
        protected Animator m_Animator;

        void Awake()
        {

           // Color handlesColor;
           // ColorUtility.TryParseHtmlString("#FF0000", out handlesColor);
           // Handles.color = Color.red;
            currentHp = maxHp;
            currentAttack = maxAttack;
            currentDefence = maxDefence;
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_Animator = GetComponent<Animator>();
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnHpChange(int value)
        {
            currentHp += value;
        }

        public void OnAttackChange(int value)
        {
            currentAttack += value;
        }

        public void OnDefenceChange(int value)
        {
            currentDefence += value;
        }

#if UNITY_EDITOR
        protected void OnDrawGizmosSelected()
        {

            //绘制攻击类型1的范围
            if(attackType1Range > 0)
            {
                Handles.color = new Color(1.0f, 0, 0, 0.1f);
                Handles.DrawSolidDisc(transform.position, Vector3.back, attackType1Range);
            }
           
        }
#endif
    }
}

