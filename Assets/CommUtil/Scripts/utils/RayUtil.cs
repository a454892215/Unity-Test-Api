using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGameComm
{
    public class RayUtil
    {

        public static RaycastHit2D CastLine(Vector3 start,Vector3 end ,String targetLayerName)
        {
            Debug.DrawLine(start, end, Color.red);
            return Physics2D.Linecast(start, end, LayerMask.GetMask(targetLayerName));
        }

    }
}
