using System;

namespace Smart.Evaluator
{
    public class Expression
    {
        private string expression;
        private ParseTreeEvaluator tree;

        public ParseErrors Errors => tree?.Errors;

        public Expression(string exp)
        {
            expression = exp;
            var scanner = new Scanner();
            var parser = new Parser(scanner);
            tree = new ParseTreeEvaluator(Context.Default);
            tree = parser.Parse(expression, tree) as ParseTreeEvaluator;
        }

        public object Eval()
        {
            var result = tree.Eval(null);
            if (tree.Context.CurrentStackSize > 0)
                Errors.Add(new ParseError("Stacksize is not empty", 0, null));
            return result;
        }

        public static object Eval(string expression)
        {
            return Eval<object>(expression);
        }

        public static T Eval<T>(string expression) 
        {
            object result = null;
            try
            {
                var exp = new Expression(expression);
                
                if (exp.tree.Errors.Count > 0)
                    result = exp.tree.Errors[0].Message;
                else
                    result = exp.Eval();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result != null ? (T)result : default(T);
        }
    }
}
