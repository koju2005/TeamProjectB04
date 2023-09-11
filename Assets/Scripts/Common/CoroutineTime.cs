using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Common
{
    public class CoroutineTime
    {
        private static Dictionary<float, WaitForSecondsRealtime> realtimePool = new Dictionary<float, WaitForSecondsRealtime>();
        private static Dictionary<float, WaitForSeconds> timePool = new Dictionary<float, WaitForSeconds>();
        public static WaitForSecondsRealtime GetWaitForSecondsRealtime(float time)
        {
            WaitForSecondsRealtime seconds;
            if (!realtimePool.TryGetValue(time, out seconds))
            {
                seconds = new WaitForSecondsRealtime(time);
            }

            return seconds;
        }
        
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