using System;
using System.Linq;
using ByteDev.Common.Collections;
using NUnit.Framework;
using System.Collections.Generic;

namespace ByteDev.Common.UnitTests.Collections
{
    [TestFixture]
    public class ListExtensionsTest
    {
        [TestFixture]
        public class NullToEmpty
        {
            [Test]
            public void WhenIsNull_ThenReturnEmpty()
            {
                IList<string> sut = null;

                var result = sut.NullToEmpty();

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenIsNotNull_ThenReturnSame()
            {
                IList<string> sut = new List<string>();

                var result = sut.NullToEmpty();

                Assert.That(result, Is.SameAs(sut));
            }
        }

        [TestFixture]
        public class RemoveWhere
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                List<int> sut = null;

                Assert.Throws<ArgumentNullException>(() => sut.RemoveWhere(x => x == 1));
            }

            [Test]
            public void WhenConditionIsNull_ThenThrowException()
            {
                var sut = new List<int> {1, 2, 3, 1, 2};

                Assert.Throws<ArgumentNullException>(() => sut.RemoveWhere(null));
            }

            [Test]
            public void WhereMatchesCondition_ThenRemoveElements()
            {
                var sut = new List<int> {1, 2, 3, 1, 2};

                sut.RemoveWhere(x => x == 1 || x == 2);

                Assert.That(sut.Single(), Is.EqualTo(3));
            }
        }

        [TestFixture]
        public class MoveToFirst
        {
            private Customer _customer1;
            private Customer _customer2;

            [SetUp]
            public void SetUp()
            {
                _customer1 = new Customer {Name = "John"};
                _customer2 = new Customer {Name = "Peter"};
            }

            [Test]
            public void WhenListIsNull_ThenThrowException()
            {
                IList<Customer> sut = null;

                Assert.Throws<ArgumentNullException>(() => sut.MoveToFirst(_customer1));
            }

            [Test]
            public void WhenObjIsNull_ThenThrowException()
            {
                var sut = Createsut();

                Assert.Throws<ArgumentNullException>(() => sut.MoveToFirst(null));
            }

            [Test]
            public void WhenListIsEmpty_ThenLeaveListEmpty()
            {
                var sut = Createsut();

                sut.MoveToFirst(_customer1);

                Assert.That(sut.Count, Is.EqualTo(0));
            }

            [Test]
            public void WhenObjExists_ThenMoveToFirst()
            {
                var sut = Createsut(_customer1, _customer2);

                sut.MoveToFirst(_customer2);

                Assert.That(sut.First(), Is.SameAs(_customer2));
                Assert.That(sut.Second(), Is.SameAs(_customer1));
            }

            [Test]
            public void WhenObjDoesNotExist_ThenNotChangeList()
            {
                var sut = Createsut(_customer2, _customer2);

                sut.MoveToFirst(_customer1);

                Assert.That(sut.Count, Is.EqualTo(2));
                Assert.That(sut.First(), Is.SameAs(_customer2));
                Assert.That(sut.Second(), Is.SameAs(_customer2));
            }

            private static IList<Customer> Createsut(params Customer[] customers)
            {
                var list = new List<Customer>();
                list.AddRange(customers);
                return list;
            }

            public class Customer
            {
                public string Name { get; set; }
            }
        }
    }
}
    