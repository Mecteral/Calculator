namespace ImperialAndMetricConverter
{
    static class ExpressionExtension
    {
            public static void Parent(this IConversionExpression self, IConversionExpression newParent)
                => ((AnConversionExpression)self).Parent = newParent;

    }
}
