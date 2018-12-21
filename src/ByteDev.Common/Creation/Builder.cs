using System;
using System.Collections.Generic;

namespace ByteDev.Common.Creation
{
    public abstract class Builder<TBuilder, TEntity> where TBuilder : Builder<TBuilder, TEntity>
    {
        private readonly List<Action<TEntity>> _mutations = new List<Action<TEntity>>();

        public static implicit operator TEntity(Builder<TBuilder, TEntity> builder)
        {
            return builder.Build();
        }

        public TBuilder With(Action<TEntity> mutation)
        {
            _mutations.Add(mutation);
            return this as TBuilder;
        }

        public virtual TEntity Build()
        {
            return ApplyMutations(CreateEntity());
        }

        protected abstract TEntity CreateEntity();

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