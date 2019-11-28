using System;
using System.Collections.Generic;

namespace ByteDev.Common.Creation
{
    /// <summary>
    /// Represents a generic builder base class.
    /// </summary>
    /// <typeparam name="TBuilder">Type of builder specialization.</typeparam>
    /// <typeparam name="TEntity">Type to build.</typeparam>
    public abstract class Builder<TBuilder, TEntity> where TBuilder : Builder<TBuilder, TEntity>
    {
        private readonly List<Action<TEntity>> _mutations = new List<Action<TEntity>>();

        /// <summary>
        /// Implicit converter to type <typeparamref name="TBuilder" />.
        /// </summary>
        /// <param name="builder">Builder to convert from.</param>
        public static implicit operator TEntity(Builder<TBuilder, TEntity> builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Adds a mutation.
        /// </summary>
        /// <param name="mutation">Mutation to add.</param>
        /// <returns>Current class instance.</returns>
        public TBuilder With(Action<TEntity> mutation)
        {
            _mutations.Add(mutation);
            return this as TBuilder;
        }

        /// <summary>
        /// Builds new instance of <typeparamref name="TEntity" />.
        /// </summary>
        /// <returns>New instance of <typeparamref name="TEntity" />.</returns>
        public virtual TEntity Build()
        {
            return ApplyMutations(CreateEntity());
        }

        /// <summary>
        /// Create a new instance of <typeparamref name="TEntity" />.
        /// </summary>
        /// <returns>A new instance of <typeparamref name="TEntity" />.</returns>
        protected abstract TEntity CreateEntity();

        /// <summary>
        /// Clear all mutations.
        /// </summary>
        /// <returns>Current class instance.</returns>
        protected TBuilder ClearMutations()
        {
            _mutations.Clear();
            return this as TBuilder;
        }

        private TEntity ApplyMutations(TEntity item)
        {
            _mutations.ForEach(action => action(item));
            return item;
        }
    }
}