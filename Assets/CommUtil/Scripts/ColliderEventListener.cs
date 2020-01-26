using UnityEngine;
using UnityEngine.Events;

namespace CommUtil.Scripts
{
    [System.Serializable]
    public class ColliderEventListener : MonoBehaviour
    {
        // Start is called before the first frame update


        public UnityEvent onCollisionEnter2D, onCollisionStay2D, onCollisionExit2D;

        // Update is called once per frame

        public void OnCollisionEnter2D(Collision2D collision)
        {
            onCollisionEnter2D.Invoke();
        }


        public void OnCollisionStay2D(Collision2D collision)
        {
            onCollisionStay2D.Invoke();
        }

        public void OnCollisionExit2D(Collision2D collision)
        {
            onCollisionExit2D.Invoke();
        }

    }
}
