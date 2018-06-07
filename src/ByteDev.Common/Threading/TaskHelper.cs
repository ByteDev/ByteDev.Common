using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ByteDev.Common.Threading
{
    public class TaskHelper
    {
        /// <summary>
        /// Given a set of tasks performs WhenAll on it and throws an AggregateException
        /// with all exceptions thrown (if any were thrown).
        /// </summary>
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
        /// Given a set of tasks (that return a object) performs WhenAll on it and throws an AggregateException
        /// with all exceptions thrown (if any were thrown).
        /// </summary>
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