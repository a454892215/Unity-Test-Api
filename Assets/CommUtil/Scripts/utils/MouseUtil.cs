using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGameComm
{
    public class MouseUtil
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
