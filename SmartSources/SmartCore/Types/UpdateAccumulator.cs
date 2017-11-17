using UnityEngine;

namespace Smart.Types
{
    public struct UpdateAccumulator
    {
        //--------------------------------------------------------------------------------------------------------------------------
        
        private float _lastTime;

        //--------------------------------------------------------------------------------------------------------------------------

        public bool this[float delay]
        {
            get
            {
                var newTime = Time.time;
                var canUpdate = newTime - _lastTime > delay;
                if (canUpdate) _lastTime = newTime; // reset time
                return canUpdate;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public void Reset(bool postponeNextUpdate)
        {
            _lastTime = postponeNextUpdate ? Time.time : 0;
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }

    public struct UpdateAccumulatorUnscaled
    {
        //--------------------------------------------------------------------------------------------------------------------------

        private float _lastTime;

        //--------------------------------------------------------------------------------------------------------------------------

        public bool this[float delay]
        {
            get
            {
                var newTime = Time.unscaledTime;
                var canUpdate = newTime - _lastTime > delay;
                if (canUpdate) _lastTime = newTime; // reset time
                return canUpdate;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public void Reset(bool postponeNextUpdate)
        {
            _lastTime = postponeNextUpdate ? Time.unscaledTime : 0;
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
