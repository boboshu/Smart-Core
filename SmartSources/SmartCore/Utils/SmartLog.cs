using UnityEngine;

namespace Smart.Utils
{
    public static class SmartLog
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public static bool condition;

        //--------------------------------------------------------------------------------------------------------------------------

        private static string _preffix;

        public static void SetPreffix(string value)
        {
            _preffix = "[" + value + "]: ";
        }

        public static void ClearPreffix()
        {
            _preffix = "";
        }

        //--------------------------------------------------------------------------------------------------------------------------

        private const int MAX_NOTIFICATIONS_AT_ONCE = 10;

        //--------------------------------------------------------------------------------------------------------------------------

        private static float _errorTime;
        private static int _errorCounter;
        private static bool _suppressNextError;

        public static void ErrorConditional(string message, bool usePreffix = true)
        {
            if (condition) Error(message, usePreffix);          
        }

        public static void ErrorAndSuppressNext(string message, bool usePreffix = true)
        {
            Error(message, usePreffix);
            _suppressNextError = true;
        }

        public static void Error(string message, bool usePreffix = true)
        {
            if (_suppressNextError)
            {
                _suppressNextError = false;
                return;
            }

            if (_errorCounter < MAX_NOTIFICATIONS_AT_ONCE) // avoid floodfill of messages, show only first 10
            {
                Debug.LogError(usePreffix ? _preffix + message : message);
                if (_errorCounter == 0) _errorTime = Time.unscaledTime;
                _errorCounter++;
            }
            else
            {
                if (Time.unscaledTime - _errorTime > 1) // after one second show more errors if any
                    _errorCounter = 0;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------

        private static float _warningTime;
        private static int _warningCounter;

        public static void WarningConditional(string message, bool usePreffix = true)
        {
            if (condition) Warning(message, usePreffix);
        }

        public static void Warning(string message, bool usePreffix = true)
        {
            if (_warningCounter < MAX_NOTIFICATIONS_AT_ONCE) // avoid floodfill of messages, show only first 10
            {
                Debug.LogWarning(usePreffix ? _preffix + message : message);
                if (_warningCounter == 0) _warningTime = Time.unscaledTime;
                _warningCounter++;
            }
            else
            {
                if (Time.unscaledTime - _warningTime > 1) // after one second show more errors if any
                    _warningCounter = 0;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------

        private static float _messageTime;
        private static int _messageCounter;

        public static void MessageConditional(string message, bool usePreffix = true)
        {
            if (condition) Message(message, usePreffix);
        }

        public static void Message(string message, bool usePreffix = true)
        {
            if (_messageCounter < MAX_NOTIFICATIONS_AT_ONCE) // avoid floodfill of messages, show only first 10
            {
                Debug.Log(usePreffix ? _preffix + message : message);
                if (_messageCounter == 0) _messageTime = Time.unscaledTime;
                _messageCounter++;
            }
            else
            {
                if (Time.unscaledTime - _messageTime > 1) // after one second show more errors if any
                    _messageCounter = 0;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
