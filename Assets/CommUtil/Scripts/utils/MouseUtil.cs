using UnityEngine;

namespace CommUtil.Scripts.utils
{
    public static class MouseUtil
    {

        public static bool IsMouseLeftDown()
        {
            return (Input.GetMouseButtonDown(0));
        }

        public static bool IsMouseRightDown()
        {
            return (Input.GetMouseButtonDown(1));
        }

        public static bool IsMouseLeftUp()
        {
            return (Input.GetMouseButtonUp(0));
        }

        public static bool GetMouseButtonUp()
        {
            return (Input.GetMouseButtonDown(1));
        }
    }
}
