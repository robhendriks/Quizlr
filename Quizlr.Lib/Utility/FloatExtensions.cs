namespace Quizlr.Lib.Utility
{
    public static class FloatExtensions
    {
        public static float Lerp(this float start, float end, float amount)
        {
            var diff = end - start;
            var adj = diff*amount;
            return start + adj;
        }
    }
}