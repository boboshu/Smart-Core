using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smart.Evaluator
{
    public class Functions : Dictionary<string, Function>
    {
        private static Functions defaultFunctions;
        public static Functions Defaults 
        {
            get {
                if (defaultFunctions == null)
                {
                    defaultFunctions = new Functions();
                    defaultFunctions.InitDefaults();
                }
                return defaultFunctions;
            }
        }

        public void InitDefaults()
        {
            

            Add("about", new StaticFunction("About", delegate { return "@TinyExe - a Tiny Expression Evaluator v1.0\r\nby Herre Kuijpers - Copyright © 2011 under the CPOL license"; }, 0, 0));
            Add("help", new StaticFunction("Help", Help, 0, 0));

            // high precision functions
            Add("abs", new StaticFunction("Abs", delegate(object[] ps) { return Math.Abs(Convert.ToDouble(ps[0])); }, 1, 1));
            Add("acos", new StaticFunction("Acos", delegate(object[] ps) { return Math.Acos(Convert.ToDouble(ps[0])); }, 1, 1));
            Add("asin", new StaticFunction("Asin", delegate(object[] ps) { return Math.Asin(Convert.ToDouble(ps[0])); }, 1, 1));

            Add("atan", new StaticFunction("Atan", delegate(object[] ps) { return Math.Atan(Convert.ToDouble(ps[0])); }, 1, 1));
            Add("atan2", new StaticFunction("Atan2", delegate(object[] ps) { return Math.Atan2(Convert.ToDouble(ps[0]), Convert.ToDouble(ps[1])); }, 2, 2));
            Add("ceiling", new StaticFunction("Ceiling", delegate(object[] ps) { return Math.Ceiling(Convert.ToDouble(ps[0])); }, 1, 1));
            Add("cos", new StaticFunction("Cos", delegate(object[] ps) { return Math.Cos(Convert.ToDouble(ps[0])); }, 1, 1));
            Add("cosh", new StaticFunction("Cosh", delegate(object[] ps) { return Math.Cosh(Convert.ToDouble(ps[0])); }, 1, 1));
            Add("exp", new StaticFunction("Exp", delegate(object[] ps) { return Math.Exp(Convert.ToDouble(ps[0])); }, 1, 1));
            Add("int", new StaticFunction("int", delegate(object[] ps) { return (int) Math.Floor(Convert.ToDouble(ps[0])); }, 1, 1));
            Add("fact", new StaticFunction("Fact", Fact, 1, 1)); // factorials 1*2*3*4...
            Add("floor", new StaticFunction("Floor", delegate(object[] ps) { return Math.Floor(Convert.ToDouble(ps[0])); }, 1, 1));
            Add("log", new StaticFunction("Log", Log, 1, 2)); // log allows 1 or 2 parameters
            Add("ln", new StaticFunction("Ln", delegate(object[] ps) { return Math.Log(Convert.ToDouble(ps[0])); }, 1, 1));

            Add("pow", new StaticFunction("Pow", delegate(object[] ps) { return Math.Pow(Convert.ToDouble(ps[0]), Convert.ToDouble(ps[1])); }, 2, 2));
            Add("round", new StaticFunction("Round", delegate(object[] ps) { return Math.Round(Convert.ToDouble(ps[0])); }, 1, 1));
            Add("sign", new StaticFunction("Sign", delegate(object[] ps) { return Math.Sign(Convert.ToDouble(ps[0])); }, 1, 1));
            Add("sin", new StaticFunction("Sin", delegate(object[] ps) { return Math.Sin(Convert.ToDouble(ps[0])); }, 1, 1));
            Add("sinh", new StaticFunction("Sinh", delegate(object[] ps) { return Math.Sinh(Convert.ToDouble(ps[0])); }, 1, 1));
            Add("sqr", new StaticFunction("Sqr", delegate(object[] ps) { return Convert.ToDouble(ps[0]) * Convert.ToDouble(ps[0]); }, 1, 1));
            Add("sqrt", new StaticFunction("Sqrt", delegate(object[] ps) { return Math.Sqrt(Convert.ToDouble(ps[0])); }, 1, 1));
            Add("trunc", new StaticFunction("Trunc", delegate(object[] ps) { return Math.Truncate(Convert.ToDouble(ps[0])); }, 1, 1));

            // array functions
            Add("avg", new StaticFunction("Avg", Avg, 1, int.MaxValue));
            Add("stdev", new StaticFunction("StDev", StDev, 1, int.MaxValue));
            Add("var", new StaticFunction("Var", Var, 1, int.MaxValue));
            Add("max", new StaticFunction("Max", Max, 1, int.MaxValue));
            Add("median", new StaticFunction("Min", Median, 1, int.MaxValue));
            Add("min", new StaticFunction("Min", Min, 1, int.MaxValue));

            //boolean functions
            Add("not", new StaticFunction("Not", delegate(object[] ps) { return !Convert.ToBoolean(ps[0]); }, 1, 1));
            Add("if", new StaticFunction("If", delegate(object[] ps) { return Convert.ToBoolean(ps[0]) ? ps[1] : ps[2]; }, 3, 3));
            Add("and", new StaticFunction("And", delegate(object[] ps) { return Convert.ToBoolean(ps[0]) && Convert.ToBoolean(ps[1]); }, 2, 2));
            Add("or", new StaticFunction("Or", delegate(object[] ps) { return Convert.ToBoolean(ps[0]) || Convert.ToBoolean(ps[1]); }, 2, 2));
            
            // string functions
            Add("left", new StaticFunction("Left", delegate(object[] ps)
            {
                var len = Convert.ToInt32(ps[1]) < ps[0].ToString().Length ? Convert.ToInt32(ps[1]) : ps[0].ToString().Length;
                return ps[0].ToString().Substring(0, len);
            }, 2, 2));

            Add("right", new StaticFunction("Right", delegate(object[] ps)
            {
                var len = Convert.ToInt32(ps[1]) < ps[0].ToString().Length ? Convert.ToInt32(ps[1]) : ps[0].ToString().Length;
                return ps[0].ToString().Substring(ps[0].ToString().Length - len, len); 
            }, 2, 2));

            Add("mid", new StaticFunction("Mid", delegate(object[] ps)
            {
                var idx = Convert.ToInt32(ps[1]) < ps[0].ToString().Length ? Convert.ToInt32(ps[1]) : ps[0].ToString().Length;
                var len = Convert.ToInt32(ps[2]) < ps[0].ToString().Length - idx ? Convert.ToInt32(ps[2]) : ps[0].ToString().Length - idx;
                return ps[0].ToString().Substring(idx, len);
            }, 3, 3));

            Add("hex", new StaticFunction("Hex", delegate(object[] ps) { return String.Format("0x{0:X}", Convert.ToInt32(ps[0].ToString())); }, 1, 1));
            Add("format", new StaticFunction("Format", delegate(object[] ps) { return string.Format(ps[0].ToString(), ps[1]); }, 2, 2));
            Add("len", new StaticFunction("Len", delegate(object[] ps) { return Convert.ToDouble(ps[0].ToString().Length); }, 1, 1));
            Add("lower", new StaticFunction("Lower", delegate(object[] ps) { return ps[0].ToString().ToLowerInvariant(); }, 1, 1));
            Add("upper", new StaticFunction("Upper", delegate(object[] ps) { return ps[0].ToString().ToUpperInvariant(); }, 1, 1));
            Add("val", new StaticFunction("Val", delegate(object[] ps) { return Convert.ToDouble(ps[0]); }, 1, 1));
            Add("clear", new ClearFunction());

        }

        /// <summary>
        /// calculates the average over a list of numeric values
        /// </summary>
        /// <param name="ps">list of numeric values</param>
        /// <returns>the average value</returns>
        private static object Avg(object[] ps)
        {
            double total = 0;
            foreach (var o in ps)
                total += Convert.ToDouble(o);

            return total / ps.Length;
        }

        /// <summary>
        /// calculates the median over a list of numeric values
        /// </summary>
        /// <param name="ps">list of numeric values</param>
        /// <returns>the median value</returns>
        private static object Median(object[] ps)
        {
            var ordered = ps.OrderBy(o => Convert.ToDouble(o)).ToArray();

            if (ordered.Length % 2 == 1)
                return ordered[ordered.Length / 2];
            return (Convert.ToDouble(ordered[ordered.Length / 2]) + Convert.ToDouble(ordered[ordered.Length / 2-1]))/2;
        }

        /// <summary>
        /// calculates the statistical variance over a list of numeric values
        /// </summary>
        /// <param name="ps">list of numeric values</param>
        /// <returns>the variance</returns>
        private static object Var(object[] ps)
        {
            var avg = Convert.ToDouble(Avg(ps));
            double total = 0;
            foreach (var o in ps)
                total += (Convert.ToDouble(o) - avg) * (Convert.ToDouble(o) - avg);

            return total / (ps.Length-1);
        }

        /// <summary>
        /// calculates the statistical standard deviation over a list of numeric values
        /// </summary>
        /// <param name="ps">list of numeric values</param>
        /// <returns>the standard deviation</returns>
        private static object StDev(object[] ps)
        {
            var var = Convert.ToDouble(Var(ps));
            return Math.Sqrt(var);
        }

        /// <summary>
        /// generic Log implementation, allows 1 or 2 parameters
        /// </summary>
        /// <param name="ps">numeric values</param>
        /// <returns>Log of the value</returns>
        private static object Log(object[] ps)
        {
            if (ps.Length == 1)
                return Math.Log10(Convert.ToDouble(ps[0]));

            if (ps.Length == 2)
                return Math.Log(Convert.ToDouble(ps[0]), Convert.ToDouble(ps[1]));

            return null;
        }

        private static object Fact(object[] ps)
        {
            double total = 1;

            for (var i = Convert.ToInt32(ps[0]); i > 1; i--)
                total *= i;

            return total;
        }

        private static object Max(object[] ps)
        {
            var max = double.MinValue;

            foreach (var o in ps)
            {
                var val = Convert.ToDouble(o);
                if (val > max) 
                    max = val;
            }
            return max;
        }

        private static object Min(object[] ps)
        {
            var min = double.MaxValue;

            foreach (var o in ps)
            {
                var val = Convert.ToDouble(o);
                if (val < min)
                    min = val;
            }
            return min;
        }

        private static object Help(object[] ps)
        {
            var help = new StringBuilder();
            help.AppendLine("Tiny Expression Evalutator can evaluate expression containing the following functions:");
            var keys = Defaults.Keys.OrderBy(s => s).ToArray();
            foreach (var key in keys)
            {
                var func = Defaults[key];
                help.Append(func.Name + " ");
            }
            return help.ToString();
        }

    }

}
