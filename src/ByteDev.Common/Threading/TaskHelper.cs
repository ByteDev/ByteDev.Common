using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ByteDev.Common.Threading
{
    /// <summary>
    /// Represents a set of operations to help when using Tasks.
    /// </summary>
    public class TaskHelper
    {
        /// <summary>
        /// Performs WhenAll on collection of tasks and throws an AggregateException
        /// with all exceptions thrown (if any were thrown).
        /// </summary>
        /// <param name="tasks">Collection of tasks to WhenAll on.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public static async Task WhenAllTasksAsync(IEnumerable<Task> tasks)
        {
            var allTask = Task.WhenAll(tasks);

            try
            {
                await allTask;
            }
            catch (Exception)
            {
                // We don't want to throw the first exception 
                // (instead thrown the AggregateException of all exceptions thrown below)
            }

            if (allTask.Exception != null)
            {
                throw allTask.Exception;
            }
        }

        /// <summary>
        /// Performs WhenAll on collection of tasks (that return an object) and throws an AggregateException
        /// with all exceptions thrown (if any were thrown).
        /// </summary>
        /// <typeparam name="T">The type returned from the tasks.</typeparam>
        /// <param name="tasks">Collection of tasks to WhenAll on.</param>
        /// <returns>Task array of type <typeparamref name="T" />.</returns>
        public static async Task<T[]> WhenAllTasksAsync<T>(IEnumerable<Task<T>> tasks)
        {
            Task<T[]> allTask = Task.WhenAll(tasks);

            try
            {
                return await allTask;
            }
            catch (Exception)
            {
                // We don't want to throw the first exception 
                // (instead thrown the AggregateException of all exceptions thrown below)
            }

            throw allTask.Exception;
        }
    }
}