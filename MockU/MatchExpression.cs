using System.Linq.Expressions;

namespace MockU;

internal sealed class MatchExpression : Expression
{
    public readonly Match Match;

    public MatchExpression(Match match)
    {
        Match = match;
    }

    public override ExpressionType NodeType => ExpressionType.Extension;

    public override Type Type => Match.RenderExpression?.Type;

    // This node type is irreducible in order to prevent compilation.
    // The best possible reduction would involve `RenderExpression`,
    // which isn't intended to be used for that purpose.
    public override bool CanReduce => false;

    protected override Expression VisitChildren(ExpressionVisitor visitor) => this;

    public override string ToString() => Match.RenderExpression?.ToString() ?? "";
}
