﻿using System;
using System.Collections.Generic;
using System.Linq;
using ByteDev.Common.Collections;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests.Collections
{
    [TestFixture]
    public class EnumerableExtensionsTest
    {
        [TestFixture]
        public class NullToEmpty
        {
            [Test]
            public void WhenIsNull_ThenReturnEmpty()
            {
                IEnumerable<string> sut = null;

                var result = sut.NullToEmpty();

                Assert.That(result.Count(), Is.EqualTo(0));
            }

            [Test]
            public void WhenIsNotNull_ThenReturnSame()
            {
                IEnumerable<string> sut = new List<string>();

                var result = sut.NullToEmpty();

                Assert.That(result, Is.SameAs(sut));
            }
        }

        [TestFixture]
        public class Find
        {
            private IEnumerable<string> _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = new[] { "Hello", "John", "Smith" };
            }

            [Test]
            public void WhenPredicateIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.Find(null));
            }

            [Test]
            public void WhenItemFound_ThenReturnItem()
            {
                var result = _sut.Find(x => x == "John");

                Assert.That(result, Is.SameAs(_sut.Second()));
            }

            [Test]
            public void WhenItemNotFound_ThenReturnItemDefault()
            {
                var result = Act(x => x == "Peter");

                Assert.That(result, Is.EqualTo(default(string)));
            }

            private string Act(Predicate<string> predicate)
            {
                return _sut.Find(predicate);
            }
        }

        [TestFixture]
        public class ForEach
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowNullException()
            {
                var counter = 0;
                int[] sut = null;

                Assert.Throws<ArgumentNullException>(() => sut.ForEach(i => counter += + i));
            }

            [Test]
            public void WhenActionIsNull_ThenThrowException()
            {
                var sut = new[] { 1, 2, 3 };

                Assert.Throws<ArgumentNullException>(() => sut.ForEach(null));
            }

            [Test]
            public void WhenNoItemsExist_ThenNotCall()
            {
                var counter = 0;
                var sut = new int[0];

                sut.ForEach(i => counter += + i);

                Assert.That(counter, Is.EqualTo(0));
            }

            [Test]
            public void WhenItemExist_ThenCallForEachItem()
            {
                var counter = 0;
                var sut = new[] { 1, 2, 3 };

                sut.ForEach(x => counter = counter + x);

                Assert.That(counter, Is.EqualTo(6));
            }
        }
        
        internal class Dummy
        {
            private readonly string _name;

            public Dummy(string name)
            {
                _name = name;
            }

            public override string ToString()
            {
                return _name;
            }
        }
    }
}
