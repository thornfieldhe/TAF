// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Ectensions.SwichCaseExtension.cs" company="">
//   
// </copyright>
// <summary>
//   Switch/Case组扩展
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Utility
{
    using System;

    /// <summary>
    /// Switch/Case组扩展
    /// </summary>
    public static class SwichCaseExtension
    {
        #region Default

        /// <summary>
        /// The default.
        /// </summary>
        /// <param name="sc">
        /// The sc.
        /// </param>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <typeparam name="TCase">
        /// </typeparam>
        /// <typeparam name="TOther">
        /// </typeparam>
        public static void Default<TCase, TOther>(this SwithCase<TCase, TOther> sc, TOther other)
        {
            if (sc == null)
            {
                return;
            }

            sc.Action(other);
        }

        #endregion

        #region SwithCase

        /// <summary>
        /// The swith case.
        /// </summary>
        /// <typeparam name="TCase">
        /// </typeparam>
        /// <typeparam name="TOther">
        /// </typeparam>
        public class SwithCase<TCase, TOther>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="SwithCase{TCase,TOther}"/> class.
            /// </summary>
            /// <param name="value">
            /// The value.
            /// </param>
            /// <param name="action">
            /// The action.
            /// </param>
            public SwithCase(TCase value, Action<TOther> action)
            {
                Value = value;
                Action = action;
            }

            /// <summary>
            /// Gets the value.
            /// </summary>
            public TCase Value
            {
                get;
            }

            /// <summary>
            /// Gets the action.
            /// </summary>
            public Action<TOther> Action
            {
                get;
            }
        }

        #endregion

        #region Swith

        /// <summary>
        /// The switch.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <typeparam name="TCase">
        /// </typeparam>
        /// <typeparam name="TOther">
        /// </typeparam>
        /// <returns>
        /// The <see cref="SwithCase"/>.
        /// </returns>
        public static SwithCase<TCase, TOther> Switch<TCase, TOther>(this TCase t, Action<TOther> action)
            where TCase : IEquatable<TCase>
        {
            return new SwithCase<TCase, TOther>(t, action);
        }

        /// <summary>
        /// The switch.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        /// <param name="selector">
        /// The selector.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <typeparam name="TInput">
        /// </typeparam>
        /// <typeparam name="TCase">
        /// </typeparam>
        /// <typeparam name="TOther">
        /// </typeparam>
        /// <returns>
        /// The <see cref="SwithCase"/>.
        /// </returns>
        public static SwithCase<TCase, TOther> Switch<TInput, TCase, TOther>(
            this TInput t,
            Func<TInput, TCase> selector,
            Action<TOther> action) where TCase : IEquatable<TCase>
        {
            return new SwithCase<TCase, TOther>(selector(t), action);
        }

        #endregion

        #region Case

        /// <summary>
        /// The case.
        /// </summary>
        /// <param name="sc">
        /// The sc.
        /// </param>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <typeparam name="TCase">
        /// </typeparam>
        /// <typeparam name="TOther">
        /// </typeparam>
        /// <returns>
        /// The <see cref="SwithCase"/>.
        /// </returns>
        public static SwithCase<TCase, TOther> Case<TCase, TOther>(
            this SwithCase<TCase, TOther> sc,
            TCase option,
            TOther other) where TCase : IEquatable<TCase>
        {
            return Case(sc, option, other, true);
        }

        /// <summary>
        /// The case.
        /// </summary>
        /// <param name="sc">
        /// The sc.
        /// </param>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <param name="bBreak">
        /// The b break.
        /// </param>
        /// <typeparam name="TCase">
        /// </typeparam>
        /// <typeparam name="TOther">
        /// </typeparam>
        /// <returns>
        /// The <see cref="SwithCase"/>.
        /// </returns>
        public static SwithCase<TCase, TOther> Case<TCase, TOther>(
            this SwithCase<TCase, TOther> sc,
            TCase option,
            TOther other,
            bool bBreak) where TCase : IEquatable<TCase>
        {
            return Case(sc, c => c.Equals(option), other, bBreak);
        }

        /// <summary>
        /// The case.
        /// </summary>
        /// <param name="sc">
        /// The sc.
        /// </param>
        /// <param name="predict">
        /// The predict.
        /// </param>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <typeparam name="TCase">
        /// </typeparam>
        /// <typeparam name="TOther">
        /// </typeparam>
        /// <returns>
        /// The <see cref="SwithCase"/>.
        /// </returns>
        public static SwithCase<TCase, TOther> Case<TCase, TOther>(
            this SwithCase<TCase, TOther> sc,
            Predicate<TCase> predict,
            TOther other) where TCase : IEquatable<TCase>
        {
            return Case(sc, predict, other, true);
        }

        /// <summary>
        /// The case.
        /// </summary>
        /// <param name="sc">
        /// The sc.
        /// </param>
        /// <param name="predict">
        /// The predict.
        /// </param>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <param name="bBreak">
        /// The b break.
        /// </param>
        /// <typeparam name="TCase">
        /// </typeparam>
        /// <typeparam name="TOther">
        /// </typeparam>
        /// <returns>
        /// The <see cref="SwithCase"/>.
        /// </returns>
        public static SwithCase<TCase, TOther> Case<TCase, TOther>(
            this SwithCase<TCase, TOther> sc,
            Predicate<TCase> predict,
            TOther other,
            bool bBreak) where TCase : IEquatable<TCase>
        {
            if (sc == null)
            {
                return null;
            }

            if (predict(sc.Value))
            {
                sc.Action(other);
                return bBreak ? null : sc;
            }

            return sc;
        }

        #endregion
    }
}