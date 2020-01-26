using System;
using UnityEngine;

namespace CommUtil.Scripts.utils
{
    public static class RayUtil
    {

        public static RaycastHit2D CastLine(Vector3 start,Vector3 end ,String targetLayerName)
        {
            Debug.DrawLine(start, end, Color.red);
            return Physics2D.Linecast(start, end, LayerMask.GetMask(targetLayerName));
        }

    }
}
