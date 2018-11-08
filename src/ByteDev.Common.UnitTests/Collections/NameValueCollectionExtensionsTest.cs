﻿using System.Collections.Specialized;
using ByteDev.Common.Collections;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests.Collections
{
    [TestFixture]
    public class NameValueCollectionExtensionsTest
    {
        [TestFixture]
        public class AddOrUpdate
        {
            [Test]
            public void WhenCollectionDoesNotContainName_ThenAddPair()
            {
                var sut = new NameValueCollection();

                sut.AddOrUpdate("name1", "value1");

                Assert.That(sut["name1"], Is.EqualTo("value1"));
            }

            [Test]
            public void WhenCollectionContainsName_ThenUpdatePair()
            {
                const string expected = "newvalue";

                var sut = new NameValueCollection
                {
                    {"name1", "value1"}
                };

                sut.AddOrUpdate("name1", expected);

                Assert.That(sut["name1"], Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class ContainsKey
        {
            [Test]
            public void WhenCollectionIsEmpty_ThenReturnFalse()
            {
                var sut = new NameValueCollection();

                var result = sut.ContainsKey("name1");

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenKeyIsNull_ThenReturnFalse()
            {
                var sut = new NameValueCollection
                {
                    {"name1", "value1"}
                };

                var result = sut.ContainsKey(null);

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenKeyIsNotNull_AndDoesNotExist_ThenReturnFalse()
            {
                var sut = new NameValueCollection
                {
                    {"name1", "value1"}
                };

                var result = sut.ContainsKey("name2");

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenKeyExists_ThenReturnTrue()
            {
                var sut = new NameValueCollection
                {
                    {"name1", "value1"}
                };

                var result = sut.ContainsKey("name1");

                Assert.That(result, Is.True);
            }
        }
    }
}