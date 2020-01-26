using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace CommUtil.Scripts.Base
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class BaseAnimal : MonoBehaviour
    {
        public int maxAttack = 10; //最大攻击力
        public int maxDefence = 10; //最大防御力


        public int currentAttack;
        public int currentDefence;

        public int attackType1Range = 2; //攻击类型1范围
        public float moveSpeedX = 1f; //每秒X轴移动速度
        public float moveSpeedY = 1f; //移动Y轴移动速度


        [FormerlySerializedAs("m_Animator")] public Animator mAnimator;

        protected virtual void Awake()
        {
            currentAttack = maxAttack;
            currentDefence = maxDefence;
            mAnimator = GetComponent<Animator>();
        }

        protected virtual void Start()
        {
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            OnMove();
        }

        protected virtual void FixedUpdate()
        {
        }

        protected virtual void OnMove()
        {
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
            if (attackType1Range > 0)
            {
                Handles.color = new Color(1.0f, 0, 0, 0.1f);
                Handles.DrawSolidDisc(transform.position, Vector3.back, attackType1Range);
            }
        }
#endif
    }
}