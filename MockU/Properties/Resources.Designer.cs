﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MockU.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MockU.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mock type has already been initialized by accessing its Object property. Adding interfaces must be done before that..
        /// </summary>
        internal static string AlreadyInitialized {
            get {
                return ResourceManager.GetString("AlreadyInitialized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value cannot be an empty string..
        /// </summary>
        internal static string ArgumentCannotBeEmpty {
            get {
                return ResourceManager.GetString("ArgumentCannotBeEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Matcher &apos;{0}&apos; is unmatchable: An implicit conversion operator will convert arguments of type &apos;{1}&apos; to the parameter&apos;s type &apos;{2}&apos;, which is assignment-incompatible..
        /// </summary>
        internal static string ArgumentMatcherWillNeverMatch {
            get {
                return ResourceManager.GetString("ArgumentMatcherWillNeverMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can only add interfaces to the mock..
        /// </summary>
        internal static string AsMustBeInterface {
            get {
                return ResourceManager.GetString("AsMustBeInterface", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CallBase cannot be used with Delegate mocks..
        /// </summary>
        internal static string CallBaseCannotBeUsedWithDelegateMocks {
            get {
                return ResourceManager.GetString("CallBaseCannotBeUsedWithDelegateMocks", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can&apos;t set return value for void method {0}..
        /// </summary>
        internal static string CantSetReturnValueForVoid {
            get {
                return ResourceManager.GetString("CantSetReturnValueForVoid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Constructor arguments cannot be passed for delegate mocks..
        /// </summary>
        internal static string ConstructorArgsForDelegate {
            get {
                return ResourceManager.GetString("ConstructorArgsForDelegate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Constructor arguments cannot be passed for interface mocks..
        /// </summary>
        internal static string ConstructorArgsForInterface {
            get {
                return ResourceManager.GetString("ConstructorArgsForInterface", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A matching constructor for the given arguments was not found on the mocked type..
        /// </summary>
        internal static string ConstructorNotFound {
            get {
                return ResourceManager.GetString("ConstructorNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Delays have to be greater than zero to ensure an async callback is used..
        /// </summary>
        internal static string DelaysMustBeGreaterThanZero {
            get {
                return ResourceManager.GetString("DelaysMustBeGreaterThanZero", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expression {0} involves a field access, which is not supported. Use properties instead..
        /// </summary>
        internal static string FieldsNotSupported {
            get {
                return ResourceManager.GetString("FieldsNotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid callback. This overload of the &quot;Callback&quot; method only accepts &quot;void&quot; (C#) or &quot;Sub&quot; (VB.NET) delegates with parameter types matching those of the set up method..
        /// </summary>
        internal static string InvalidCallbackNotADelegateWithReturnTypeVoid {
            get {
                return ResourceManager.GetString("InvalidCallbackNotADelegateWithReturnTypeVoid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid callback. Setup on method with {0} parameter(s) cannot invoke callback with different number of parameters ({1})..
        /// </summary>
        internal static string InvalidCallbackParameterCountMismatch {
            get {
                return ResourceManager.GetString("InvalidCallbackParameterCountMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid callback. Setup on method with parameters ({0}) cannot invoke callback with parameters ({1})..
        /// </summary>
        internal static string InvalidCallbackParameterMismatch {
            get {
                return ResourceManager.GetString("InvalidCallbackParameterMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid callback. Setup on method with return type &apos;{0}&apos; cannot invoke callback with return type &apos;{1}&apos;..
        /// </summary>
        internal static string InvalidCallbackReturnTypeMismatch {
            get {
                return ResourceManager.GetString("InvalidCallbackReturnTypeMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot retrieve a mock with the given object type {0} as it&apos;s not the main type of the mock or any of its additional interfaces.
        ///Please cast the argument to one of the supported types: {1}.
        ///Remember that there&apos;s no generics covariance in the CLR, so your object must be one of these types in order for the call to succeed..
        /// </summary>
        internal static string InvalidMockGetType {
            get {
                return ResourceManager.GetString("InvalidMockGetType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid callback. This overload of the &quot;Returns&quot; method only accepts non-&quot;void&quot; (C#) or &quot;Function&quot; (VB.NET) delegates with parameter types matching those of the set up method..
        /// </summary>
        internal static string InvalidReturnsCallbackNotADelegateWithReturnType {
            get {
                return ResourceManager.GetString("InvalidReturnsCallbackNotADelegateWithReturnType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The return type of the last member shown above is not mockable..
        /// </summary>
        internal static string LastMemberHasNonInterceptableReturnType {
            get {
                return ResourceManager.GetString("LastMemberHasNonInterceptableReturnType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The equals (&quot;==&quot; or &quot;=&quot; in VB) and the conditional &apos;and&apos; (&quot;&amp;&amp;&quot; or &quot;AndAlso&quot; in VB) operators are the only ones supported in the query specification expression. Unsupported expression: {0}.
        /// </summary>
        internal static string LinqBinaryOperatorNotSupported {
            get {
                return ResourceManager.GetString("LinqBinaryOperatorNotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to LINQ method &apos;{0}&apos; not supported..
        /// </summary>
        internal static string LinqMethodNotSupported {
            get {
                return ResourceManager.GetString("LinqMethodNotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expression contains a call to a method which is not virtual (overridable in VB) or abstract. Unsupported expression: {0}.
        /// </summary>
        internal static string LinqMethodNotVirtual {
            get {
                return ResourceManager.GetString("LinqMethodNotVirtual", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not determine the correct positions for all argument matchers ({0} in total) used in a call to this method: {1}.
        ///This could be caused by an unrecognized type conversion, coercion, narrowing, or widening, and is most likely a bug in MockU. Please report your use case to the MockU team..
        /// </summary>
        internal static string MatcherAssignmentFailedDuringExpressionReconstruction {
            get {
                return ResourceManager.GetString("MatcherAssignmentFailedDuringExpressionReconstruction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Member {0}.{1} does not exist..
        /// </summary>
        internal static string MemberMissing {
            get {
                return ResourceManager.GetString("MemberMissing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Method {0}.{1} is public. Use strong-typed Expect overload instead:
        ///mock.Setup(x =&gt; x.{1}());
        ///.
        /// </summary>
        internal static string MethodIsPublic {
            get {
                return ResourceManager.GetString("MethodIsPublic", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No protected method {0}.{1} found whose signature is compatible with the provided arguments ({2})..
        /// </summary>
        internal static string MethodMissing {
            get {
                return ResourceManager.GetString("MethodMissing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot set up {0}.{1} because it is not accessible to the proxy generator used by MockU:
        ///{2}.
        /// </summary>
        internal static string MethodNotVisibleToProxyFactory {
            get {
                return ResourceManager.GetString("MethodNotVisibleToProxyFactory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Minimum delay has to be lower than maximum delay..
        /// </summary>
        internal static string MinDelayMustBeLessThanMaxDelay {
            get {
                return ResourceManager.GetString("MinDelayMustBeLessThanMaxDelay", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} invocation failed with mock behavior {1}.
        ///{2}.
        /// </summary>
        internal static string MockExceptionMessage {
            get {
                return ResourceManager.GetString("MockExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The next member after the last one shown above is non-virtual, sealed, or not visible to the proxy factory..
        /// </summary>
        internal static string NextMemberNonInterceptable {
            get {
                return ResourceManager.GetString("NextMemberNonInterceptable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No constructor call could be found..
        /// </summary>
        internal static string NoConstructorCallFound {
            get {
                return ResourceManager.GetString("NoConstructorCallFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No invocations performed..
        /// </summary>
        internal static string NoInvocationsPerformed {
            get {
                return ResourceManager.GetString("NoInvocationsPerformed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected invocation on the mock at least {0} times, but was {2} times: .
        /// </summary>
        internal static string NoMatchingCallsAtLeast {
            get {
                return ResourceManager.GetString("NoMatchingCallsAtLeast", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected invocation on the mock at least once, but was never performed: .
        /// </summary>
        internal static string NoMatchingCallsAtLeastOnce {
            get {
                return ResourceManager.GetString("NoMatchingCallsAtLeastOnce", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected invocation on the mock at most {1} times, but was {2} times: .
        /// </summary>
        internal static string NoMatchingCallsAtMost {
            get {
                return ResourceManager.GetString("NoMatchingCallsAtMost", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected invocation on the mock at most once, but was {2} times: .
        /// </summary>
        internal static string NoMatchingCallsAtMostOnce {
            get {
                return ResourceManager.GetString("NoMatchingCallsAtMostOnce", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected invocation on the mock between {0} and {1} times (Exclusive), but was {2} times: .
        /// </summary>
        internal static string NoMatchingCallsBetweenExclusive {
            get {
                return ResourceManager.GetString("NoMatchingCallsBetweenExclusive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected invocation on the mock between {0} and {1} times (Inclusive), but was {2} times: .
        /// </summary>
        internal static string NoMatchingCallsBetweenInclusive {
            get {
                return ResourceManager.GetString("NoMatchingCallsBetweenInclusive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected invocation on the mock exactly {0} times, but was {2} times: .
        /// </summary>
        internal static string NoMatchingCallsExactly {
            get {
                return ResourceManager.GetString("NoMatchingCallsExactly", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected invocation on the mock should never have been performed, but was {2} times: .
        /// </summary>
        internal static string NoMatchingCallsNever {
            get {
                return ResourceManager.GetString("NoMatchingCallsNever", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected invocation on the mock once, but was {2} times: .
        /// </summary>
        internal static string NoMatchingCallsOnce {
            get {
                return ResourceManager.GetString("NoMatchingCallsOnce", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to All invocations on the mock must have a corresponding setup..
        /// </summary>
        internal static string NoSetup {
            get {
                return ResourceManager.GetString("NoSetup", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Object instance was not created by MockU..
        /// </summary>
        internal static string ObjectInstanceNotMock {
            get {
                return ResourceManager.GetString("ObjectInstanceNotMock", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Out expression must evaluate to a constant value..
        /// </summary>
        internal static string OutExpressionMustBeConstantValue {
            get {
                return ResourceManager.GetString("OutExpressionMustBeConstantValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Performed invocations:.
        /// </summary>
        internal static string PerformedInvocations {
            get {
                return ResourceManager.GetString("PerformedInvocations", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Property {0}.{1} does not have a getter..
        /// </summary>
        internal static string PropertyGetNotFound {
            get {
                return ResourceManager.GetString("PropertyGetNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Property {0}.{1} does not have a setter..
        /// </summary>
        internal static string PropertySetNotFound {
            get {
                return ResourceManager.GetString("PropertySetNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type {0} does not have matching protected member: {1}.
        /// </summary>
        internal static string ProtectedMemberNotFound {
            get {
                return ResourceManager.GetString("ProtectedMemberNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ref expression must evaluate to a constant value..
        /// </summary>
        internal static string RefExpressionMustBeConstantValue {
            get {
                return ResourceManager.GetString("RefExpressionMustBeConstantValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invocation needs to return a value and therefore must have a corresponding setup that provides it..
        /// </summary>
        internal static string ReturnValueRequired {
            get {
                return ResourceManager.GetString("ReturnValueRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expression is not an event add: {0}.
        /// </summary>
        internal static string SetupNotEventAdd {
            get {
                return ResourceManager.GetString("SetupNotEventAdd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expression is not an event remove: {0}.
        /// </summary>
        internal static string SetupNotEventRemove {
            get {
                return ResourceManager.GetString("SetupNotEventRemove", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expression is not a property access: {0}.
        /// </summary>
        internal static string SetupNotProperty {
            get {
                return ResourceManager.GetString("SetupNotProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expression is not a setter: {0}.
        /// </summary>
        internal static string SetupNotSetter {
            get {
                return ResourceManager.GetString("SetupNotSetter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type {0} does not have a default (public parameterless) constructor..
        /// </summary>
        internal static string TypeHasNoDefaultConstructor {
            get {
                return ResourceManager.GetString("TypeHasNoDefaultConstructor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type matchers may not be used as the type for &apos;Callback&apos; or &apos;Returns&apos; parameters, because no argument will never have that precise type. Consider using type &apos;object&apos; instead..
        /// </summary>
        internal static string TypeMatchersMayNotBeUsedWithCallbacks {
            get {
                return ResourceManager.GetString("TypeMatchersMayNotBeUsedWithCallbacks", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type {0} does not implement required interface {1}.
        /// </summary>
        internal static string TypeNotImplementInterface {
            get {
                return ResourceManager.GetString("TypeNotImplementInterface", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type to mock ({0}) must be an interface, a delegate, or a non-sealed, non-static class..
        /// </summary>
        internal static string TypeNotMockable {
            get {
                return ResourceManager.GetString("TypeNotMockable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to To specify a setup for public property {0}.{1}, use the typed overloads, such as:
        ///mock.Setup(x =&gt; x.{1}).Returns(value);
        ///mock.SetupGet(x =&gt; x.{1}).Returns(value); //equivalent to previous one
        ///mock.SetupSet(x =&gt; x.{1}).Callback(callbackDelegate);
        ///.
        /// </summary>
        internal static string UnexpectedPublicProperty {
            get {
                return ResourceManager.GetString("UnexpectedPublicProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unexpected translation of a member access: {0}.
        /// </summary>
        internal static string UnexpectedTranslationOfMemberAccess {
            get {
                return ResourceManager.GetString("UnexpectedTranslationOfMemberAccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unhandled binding type: {0}.
        /// </summary>
        internal static string UnhandledBindingType {
            get {
                return ResourceManager.GetString("UnhandledBindingType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unhandled expression type: {0}.
        /// </summary>
        internal static string UnhandledExpressionType {
            get {
                return ResourceManager.GetString("UnhandledExpressionType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}:
        ///This setup was not matched..
        /// </summary>
        internal static string UnmatchedSetup {
            get {
                return ResourceManager.GetString("UnmatchedSetup", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unsupported expression: {0}.
        /// </summary>
        internal static string UnsupportedExpression {
            get {
                return ResourceManager.GetString("UnsupportedExpression", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unsupported expression: {0}
        ///{1}.
        /// </summary>
        internal static string UnsupportedExpressionWithHint {
            get {
                return ResourceManager.GetString("UnsupportedExpressionWithHint", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Extension methods (here: {0}) may not be used in setup / verification expressions..
        /// </summary>
        internal static string UnsupportedExtensionMethod {
            get {
                return ResourceManager.GetString("UnsupportedExtensionMethod", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Member {0} is not supported for protected mocking..
        /// </summary>
        internal static string UnsupportedMember {
            get {
                return ResourceManager.GetString("UnsupportedMember", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Non-overridable members (here: {0}) may not be used in setup / verification expressions..
        /// </summary>
        internal static string UnsupportedNonOverridableMember {
            get {
                return ResourceManager.GetString("UnsupportedNonOverridableMember", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Static members (here: {0}) may not be used in setup / verification expressions..
        /// </summary>
        internal static string UnsupportedStaticMember {
            get {
                return ResourceManager.GetString("UnsupportedStaticMember", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}:
        ///This mock failed verification due to the following unverified invocations:.
        /// </summary>
        internal static string UnverifiedInvocations {
            get {
                return ResourceManager.GetString("UnverifiedInvocations", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Use ItExpr.IsNull&lt;TValue&gt; rather than a null argument value, as it prevents proper method lookup..
        /// </summary>
        internal static string UseItExprIsNullRatherThanNullArgumentValue {
            get {
                return ResourceManager.GetString("UseItExprIsNullRatherThanNullArgumentValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to It is impossible to call the provided strongly-typed predicate due to the use of a type matcher. Provide a weakly-typed predicate with two parameters (object, Type) instead..
        /// </summary>
        internal static string UseItIsOtherOverload {
            get {
                return ResourceManager.GetString("UseItIsOtherOverload", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}:.
        /// </summary>
        internal static string VerificationErrorsOfInnerMock {
            get {
                return ResourceManager.GetString("VerificationErrorsOfInnerMock", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}:
        ///This mock failed verification due to the following:.
        /// </summary>
        internal static string VerificationErrorsOfMock {
            get {
                return ResourceManager.GetString("VerificationErrorsOfMock", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The mock repository failed verification due to the following:.
        /// </summary>
        internal static string VerificationErrorsOfMockRepository {
            get {
                return ResourceManager.GetString("VerificationErrorsOfMockRepository", resourceCulture);
            }
        }
    }
}
