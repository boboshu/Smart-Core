using Smart.Types;
using UnityEngine;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Type Converter")]
    public class TypeConverterHelper : MonoBehaviour
    {
        public enum ConvertToType { Integer, Float, Boolean, String }
        public ConvertToType convertToType;

        public UnityEventInt applyInteger = new UnityEventInt();
        public UnityEventFloat applyFloat = new UnityEventFloat();
        public UnityEventBoolean applyBoolean = new UnityEventBoolean();
        public UnityEventString applyString = new UnityEventString();

        public void Convert(int value)
        {
            switch (convertToType)
            {
                case ConvertToType.Integer: applyInteger.Invoke(value); return;
                case ConvertToType.Float: applyFloat.Invoke(value); return;
                case ConvertToType.Boolean: applyBoolean.Invoke(value != 0); return;
                case ConvertToType.String: applyString.Invoke(value.ToString()); return;
            }
        }

        public void Convert(float value)
        {
            switch (convertToType)
            {
                case ConvertToType.Integer: applyInteger.Invoke((int)value); return;
                case ConvertToType.Float: applyFloat.Invoke(value); return;
                case ConvertToType.Boolean: applyBoolean.Invoke(value > 0); return;
                case ConvertToType.String: applyString.Invoke(value.ToString("0.####")); return;
            }
        }

        public void Convert(bool value)
        {
            switch (convertToType)
            {
                case ConvertToType.Integer: applyInteger.Invoke(value ? 1 : 0); return;
                case ConvertToType.Float: applyFloat.Invoke(value ? 1 : 0); return;
                case ConvertToType.Boolean: applyBoolean.Invoke(value); return;
                case ConvertToType.String: applyString.Invoke(value.ToString()); return;
            }
        }

        public void Convert(string value)
        {
            switch (convertToType)
            {
                case ConvertToType.Integer:
                    int iv;
                    applyInteger.Invoke(int.TryParse(value, out iv) ? iv : 0);
                    return;

                case ConvertToType.Float:
                    float fv;
                    applyFloat.Invoke(float.TryParse(value, out fv) ? fv : 0f);
                    return;

                case ConvertToType.Boolean:
                    bool bv;
                    applyBoolean.Invoke(bool.TryParse(value, out bv) && bv);
                    return;

                case ConvertToType.String:
                    applyString.Invoke(value);
                    return;
            }
        }
    }
}
