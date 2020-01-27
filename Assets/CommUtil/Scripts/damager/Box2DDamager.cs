using System;
using CommUtil.Scripts.Base;
using UnityEngine;

namespace CommUtil.Scripts.damager
{
    public class Box2DDamager : MonoBehaviour
    {
        private void Start()
        {
            throw new NotImplementedException();
        }

        private float _lastValidAttackTime;

        private void OnTriggerStay2D(Collider2D other)
        {
            var baseAnimal = other.gameObject.GetComponent<BaseAnimal>();
            if (baseAnimal == null || gameObject.tag.Equals(other.tag)) return;
            var dTime = Time.time - _lastValidAttackTime;
            if (!(dTime > 1)) return;
            print("==============:" + baseAnimal + "   dTime:" + dTime);
            baseAnimal.hp.OnHpChange(-10);
            _lastValidAttackTime = Time.time;
        }
    }
}