using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Common
{
    public class CoroutineTime
    {
        private static Dictionary<float, WaitForSeconds> timePool = new Dictionary<float, WaitForSeconds>();
        
        public static WaitForSeconds GetWaitForSeconds(float time)
        {
            WaitForSeconds seconds;
            if (!timePool.TryGetValue(time, out seconds))
            {
                seconds = new WaitForSeconds(time);
            }

            return seconds;
        }
    }
}