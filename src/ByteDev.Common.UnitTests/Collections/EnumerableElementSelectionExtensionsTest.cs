using System;
using System.Collections.Generic;
using ByteDev.Common.Collections;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests.Collections
{
    [TestFixture]
    public class EnumerableElementSelectionExtensionsTest
    {
        private List<string> _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new List<string>();
        }

        [TestFixture]
        public class Second : EnumerableElementSelectionExtensionsTest
        {
            [Test]
            public void WhenIsNull_ThenThrowException()
            {
                IEnumerable<object> sut = null;

                Assert.Throws<ArgumentNullException>(() => sut.Second());
            }

            [Test]
            public void WhenIsEmpty_ThenThrowException()
            {
                var ex = Assert.Throws<InvalidOperationException>(() => _sut.Second());
                Assert.That(ex.Message, Is.EqualTo("Sequence contains no elements."));
            }

            [Test]
            public void WhenHasOneElement_ThenThrowException()
            {
                _sut.Add("John");

                var ex = Assert.Throws<InvalidOperationException>(() => _sut.Second());
                Assert.That(ex.Message, Is.EqualTo("Sequence contains no second element."));
            }

            [Test]
            public void WhenHasTwoElements_ThenReturnSecondElement()
            {
                const string expected = "Peter";

                _sut.Add("John");
                _sut.Add(expected);

                var result = _sut.Second();

                Assert.That(result, Is.EqualTo(result));
            }
        }

        [TestFixture]
        public class Third : EnumerableElementSelectionExtensionsTest
        {
            [Test]
            public void WhenHasTwoElements_ThenThrowException()
            {
                _sut.Add("John");
                _sut.Add("Jack");

                var ex = Assert.Throws<InvalidOperationException>(() => _sut.Third());
                Assert.That(ex.Message, Is.EqualTo("Sequence contains no third element."));
            }

            [Test]
            public void WhenHasThreeElements_ThenReturnThirdElement()
            {
                const string expected = "Peter";

                _sut.Add("John");
                _sut.Add("Jack");
                _sut.Add(expected);

                var result = _sut.Third();

                Assert.That(result, Is.EqualTo(result));
            }
        }

        [TestFixture]
        public class Fourth : EnumerableElementSelectionExtensionsTest
        {
            [Test]
            public void WhenHasThreeElements_ThenThrowException()
            {
                _sut.Add("John");
                _sut.Add("Jack");
                _sut.Add("Peter");

                var ex = Assert.Throws<InvalidOperationException>(() => _sut.Fourth());
                Assert.That(ex.Message, Is.EqualTo("Sequence contains no fourth element."));
            }

            [Test]
            public void WhenHasFourElements_ThenReturnFourthElement()
            {
                const string expected = "Bill";

                _sut.Add("John");
                _sut.Add("Jack");
                _sut.Add("Peter");
                _sut.Add(expected);

                var result = _sut.Fourth();

                Assert.That(result, Is.EqualTo(result));
            }
        }

        [TestFixture]
        public class Fifth : EnumerableElementSelectionExtensionsTest
        {
            [Test]
            public void WhenHasFourElements_ThenThrowException()
            {
                _sut.Add("John");
                _sut.Add("Jack");
                _sut.Add("Peter");
                _sut.Add("Bill");

                var ex = Assert.Throws<InvalidOperationException>(() => _sut.Fifth());
                Assert.That(ex.Message, Is.EqualTo("Sequence contains no fifth element."));
            }

            [Test]
            public void WhenHasFiveElements_ThenReturnFifthElement()
            {
                const string expected = "Mike";

                _sut.Add("John");
                _sut.Add("Jack");
                _sut.Add("Peter");
                _sut.Add("Bill");
                _sut.Add(expected);

                var result = _sut.Fifth();

                Assert.That(result, Is.EqualTo(result));
            }
        }
    }
}