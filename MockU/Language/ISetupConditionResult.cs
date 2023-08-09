using System.ComponentModel;
using System.Linq.Expressions;

using MockU.Language.Flow;

namespace MockU.Language;

/// <summary>
/// Implements the fluent API.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ISetupConditionResult<T> where T : class
{
    /// <summary>
    /// The expectation will be considered only in the former condition.
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    ISetup<T> Setup(Expression<Action<T>> expression);

    /// <summary>
    /// The expectation will be considered only in the former condition.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    ISetup<T, TResult> Setup<TResult>(Expression<Func<T, TResult>> expression);

    /// <summary>
    /// Setups the get.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    ISetupGetter<T, TProperty> SetupGet<TProperty>(Expression<Func<T, TProperty>> expression);

    /// <summary>
    /// Setups the set.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    /// <param name="setterExpression">The setter expression.</param>
    /// <returns></returns>
    ISetupSetter<T, TProperty> SetupSet<TProperty>(Action<T> setterExpression);

    /// <summary>
    /// Setups the set.
    /// </summary>
    /// <param name="setterExpression">The setter expression.</param>
    /// <returns></returns>
    ISetup<T> SetupSet(Action<T> setterExpression);
}
