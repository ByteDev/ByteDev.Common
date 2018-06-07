using System;
using ByteDev.Common.Reflection;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests.Reflection
{
    [TestFixture]
    public class ReflectionAttributeExtensionsTest
    {
        [TestFixture]
        public class HasAttribute
        {
            [Test]
            public void WhenClassMethodHasAttribute_ThenReturnTrue()
            {
                var methodUnderTest = typeof(DummyWithMethods).GetMethod("MethodWithAttribute");

                var result = methodUnderTest.HasAttribute<UsedAttribute>();

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenClassMethodDoesntHaveAttribute_ThenReturnFalse()
            {
                var methodUnderTest = typeof(DummyWithMethods).GetMethod("MethodWithNoAttribute");

                var result = methodUnderTest.HasAttribute<UsedAttribute>();

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenObjectHasAttribute_ThenReturnTrue()
            {
                var typeUnderTest = new DummyWithClassAttribute();

                var result = typeUnderTest.HasAttribute<UsedAttribute>();

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenObjectHasNoAttribute_ThenReturnFalse()
            {
                var typeUnderTest = new DummyWithMethods();

                var result = typeUnderTest.HasAttribute<UsedAttribute>();

                Assert.That(result, Is.False);
            }
        }
        
        public class UsedAttribute : Attribute
        {
        }

        public class DummyWithMethods
        {
            [Used]
            public void MethodWithAttribute()
            {
            }

            public void MethodWithNoAttribute()
            {
            }
        }

        [Used]
        public class DummyWithClassAttribute
        {
        }
    }
}
