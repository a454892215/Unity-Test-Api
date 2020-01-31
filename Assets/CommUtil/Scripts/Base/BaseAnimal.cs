using CommUtil.Scripts.comm;
using UnityEngine;
using UnityEngine.Serialization;

namespace CommUtil.Scripts.Base
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class BaseAnimal : MonoBehaviour
    {
        private Hp _hp;

        public Hp Hp => _hp;

        public int attackType1Range = 2; //攻击类型1范围
        public float moveSpeedX = 1f; //每秒X轴移动速度

        [FormerlySerializedAs("m_Animator")] public Animator mAnimator;

        private Transform _damagerTransform;
        public Transform DamagerTransform => _damagerTransform;

        protected virtual void Awake() 
        {
            //   currentAttack = maxAttack;
            //   currentDefence = maxDefence;
            mAnimator = GetComponent<Animator>();
            _hp = transform.Find(Cv.PathPh).GetComponent<Hp>();
            _damagerTransform = transform.Find(Cv.PathDamager);
          //  print("========Awake==========gameObject:" + _hp.transform.parent);
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


#if UNITY_EDITOR
        protected void OnDrawGizmosSelected()
        {
            //绘制攻击类型1的范围
            if (attackType1Range > 0)
            {
                /*Handles.color = new Color(1.0f, 0, 0, 0.1f);
                Handles.DrawSolidDisc(transform.position, Vector3.back, attackType1Range);*/
            }
        }
#endif
    }
}