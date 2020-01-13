using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameComm
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class BaseAnimal : MonoBehaviour
    {
        public int MaxHp = 100; //最大血量
        public int MaxAttack = 10; //最大攻击力
        public int MaxDefence = 10; //最大防御力

        public int CurrentHp;
        public int CurrentAttack;
        public int CurrentDefence;

        public int AttackType1Range = 2; //攻击类型1范围
        public float moveVelocity = 1f; //移动速度

        protected Rigidbody2D m_Rigidbody2D;
        protected Animator m_Animator;

        void Awake()
        {
            CurrentHp = MaxHp;
            CurrentAttack = MaxAttack;
            CurrentDefence = MaxDefence;
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
            CurrentHp += value;
        }

        public void OnAttackChange(int value)
        {
            CurrentAttack += value;
        }

        public void OnDefenceChange(int value)
        {
            CurrentDefence += value;
        }
    }
}

