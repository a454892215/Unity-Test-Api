using UnityEngine;

namespace CommUtil.Scripts.comm
{
    public class Hp : MonoBehaviour
    {
        private int _maxHp = 100; //最大血量
        private int _currentHp;
        private float _originScaleX;
        private Transform _transform;

        public int CurrentHp => _currentHp;

        private void Awake()
        {
            _currentHp = _maxHp;
            _transform = transform;
        }

        public void OnHpChange(int value)
        {
            _currentHp += value;
            _currentHp = Mathf.Clamp(_currentHp, 0, _maxHp);
            var hpTransformLocalScale = _transform.localScale;
            _originScaleX = hpTransformLocalScale.x;
            hpTransformLocalScale.x = _currentHp / (float) _maxHp * _originScaleX;
            _transform.localScale = hpTransformLocalScale;
        }
        
    }
}