using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ByteDev.Common.Threading;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests.Threading
{
    [TestFixture]
    public class TaskHelperTests
    {
        [TestFixture]
        public class WhenAllTasksAsync : TaskHelperTests
        {
            [Test]
            public async Task WhenNoTaskThrowsException_ThenDoNotThrowException()
            {
                int counter = 0;

                var tasks = new List<Task>
                {
                    Task.Run(() => counter++),
                    Task.Run(() => counter++)
                };

                await TaskHelper.WhenAllTasksAsync(tasks);

                Assert.That(counter, Is.EqualTo(2));
            }

            [Test]
            public void WhenOneTaskThrowsException_ThenThrowAggregateException()
            {
                int counter = 0;

                var tasks = new List<Task>
                {
                    Task.Run(() => throw new Exception("Exception 1")),
                    Task.Run(() => counter++)
                };

                var ex = Assert.ThrowsAsync<AggregateException>(() => TaskHelper.WhenAllTasksAsync(tasks));

                Assert.That(ex.InnerExceptions[0].Message, Is.EqualTo("Exception 1"));
                Assert.That(counter, Is.EqualTo(1));
            }

            [Test]
            public void WhenTwoTaskThrowException_ThenThrowAggregateException()
            {
                var tasks = new List<Task>
                {
                    Task.Run(() => throw new Exception("Exception 1")),
                    Task.Run(() => throw new Exception("Exception 2"))
                };

                var ex = Assert.ThrowsAsync<AggregateException>(() => TaskHelper.WhenAllTasksAsync(tasks));

                Assert.That(ex.InnerExceptions[0].Message, Is.EqualTo("Exception 1"));
                Assert.That(ex.InnerExceptions[1].Message, Is.EqualTo("Exception 2"));
            }
        }

        [TestFixture]
        public class WhenAllTasksAsync_WithReturnType : TaskHelperTests
        {
            [Test]
            public async Task WhenNoTaskThrowsException_ThenDoNotThrowException()
            {
                var tasks = new List<Task<int>>
                {
                    Task.Run(() => 1),
                    Task.Run(() => 2)
                };

                var result = await TaskHelper.WhenAllTasksAsync(tasks);

                Assert.That(result.Length, Is.EqualTo(2));
                Assert.That(result[0], Is.EqualTo(1));
                Assert.That(result[1], Is.EqualTo(2));
            }

            [Test]
            public void WhenOneTaskThrowsException_ThenThrowAggregateException()
            {
                int counter = 0;

                var tasks = new List<Task<int>>
                {
                    Task.Run(() =>
                    {
                        throw new Exception("Exception 1");
                        return 1;
                    }),
                    Task.Run(() =>
                    {
                        counter++;
                        return 2;
                    })
                };

                var ex = Assert.ThrowsAsync<AggregateException>(() => TaskHelper.WhenAllTasksAsync(tasks));

                Assert.That(ex.InnerExceptions[0].Message, Is.EqualTo("Exception 1"));
                Assert.That(counter, Is.EqualTo(1));
            }

            [Test]
            public void WhenTwoTaskThrowException_ThenThrowAggregateException()
            {
                var tasks = new List<Task<int>>
                {
                    Task.Run(() =>
                    {
                        throw new Exception("Exception 1");
                        return 1;
                    }),
                    Task.Run(() =>
                    {
                        throw new Exception("Exception 2");
                        return 2;
                    })
                };

                var ex = Assert.ThrowsAsync<AggregateException>(() => TaskHelper.WhenAllTasksAsync(tasks));

                Assert.That(ex.InnerExceptions[0].Message, Is.EqualTo("Exception 1"));
                Assert.That(ex.InnerExceptions[1].Message, Is.EqualTo("Exception 2"));
            }
        }
    }
}