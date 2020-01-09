using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        private void FixedUpdate()
        {
            // 是否按下左边Ctrl
            bool crouch = Input.GetKey(KeyCode.LeftControl);

            //水平移动 值域：[-1,1]
            float h = CrossPlatformInputManager.GetAxis("Horizontal");

           // print("====================crouch:" + crouch + " h:" + h + " m_Jump:" + m_Jump);
            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
