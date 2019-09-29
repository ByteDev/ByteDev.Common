using System;
using System.Linq;
using ByteDev.Collections;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests
{
    [TestFixture]
    public class UriExtensionsTest
    {
        [TestFixture]
        public class GetQueryAsDictionary
        {
            [Test]
            public void WhenHasNoQueryString_ThenReturnEmpty()
            {
                var sut = new Uri("http://www.somewhere.com/");

                var result = sut.GetQueryAsDictionary();

                Assert.That(result.Count, Is.EqualTo(0));
            }

            [Test]
            public void WhenHasOneParamAndValue_ThenReturnOneNameValuePair()
            {
                var sut = new Uri("http://www.somewhere.com/?s=hello");

                var result = sut.GetQueryAsDictionary();

                Assert.That(result.Single().Key, Is.EqualTo("s"));
                Assert.That(result.Single().Value, Is.EqualTo("hello"));
            }

            [Test]
            public void WhenHasTwoParamsAndValues_ThenReturnTwoNameValuePairs()
            {
                var sut = new Uri("http://www.somewhere.com/?s=hello&w=WORLD");

                var result = sut.GetQueryAsDictionary();

                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result.First().Key, Is.EqualTo("s"));
                Assert.That(result.First().Value, Is.EqualTo("hello"));
                Assert.That(result.Second().Key, Is.EqualTo("w"));
                Assert.That(result.Second().Value, Is.EqualTo("WORLD"));
            }

            [Test]
            public void WhenHasParamWithNoValue_ThenReturnOneValuePair()
            {
                var sut = new Uri("http://www.somewhere.com/?s=");

                var result = sut.GetQueryAsDictionary();

                Assert.That(result.Single().Key, Is.EqualTo("s"));
                Assert.That(result.Single().Value, Is.EqualTo(string.Empty));
            }

            [Test]
            public void WhenHasTwoParamsWithNoValues_ThenReturnTwoNameValuePairs()
            {
                var sut = new Uri("http://www.somewhere.com/?s=&w=");

                var result = sut.GetQueryAsDictionary();

                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result.First().Key, Is.EqualTo("s"));
                Assert.That(result.First().Value, Is.EqualTo(string.Empty));
                Assert.That(result.Second().Key, Is.EqualTo("w"));
                Assert.That(result.Second().Value, Is.EqualTo(string.Empty));
            }
        }

        [TestFixture]
        public class AddOrModifyQueryStringParams : UriExtensionsTest
        {
            private ObjNoProperties _objNoProperties;
            private ObjOneProperty _objOneProperty;
            private ObjTwoProperties _objTwoProperties;

            [SetUp]
            public void SetUp()
            {
                _objNoProperties = new ObjNoProperties();
                _objOneProperty = new ObjOneProperty { Name = "Peter" };
                _objTwoProperties = new ObjTwoProperties {Name = "John", Age = 20};
            }

            [Test]
            public void WhenUriIsNull_ThenThrowException()
            {
                Uri sut = null;
                Assert.Throws<ArgumentNullException>(() => sut.AddOrModifyQueryStringParams(_objTwoProperties));
            }

            [Test]
            public void WhenObjectIsNull_ThenThrowException()
            {
                var sut = new Uri("http://localhost/myapp");

                Assert.Throws<ArgumentNullException>(() => sut.AddOrModifyQueryStringParams(null));
            }

            [Test]
            public void WhenObjectHasNoProperties_ThenDontAppendToQueryString()
            {
                var sut = new Uri("http://localhost/myapp");

                var result = sut.AddOrModifyQueryStringParams(_objNoProperties);

                Assert.That(result, Is.EqualTo(sut));
            }

            [Test]
            public void WhenObjectOnePropertySet_ThenReturnWithQueryString()
            {
                var sut = new Uri("http://localhost/myapp");

                var result = sut.AddOrModifyQueryStringParams(_objOneProperty);

                Assert.That(result, Is.EqualTo(new Uri($"http://localhost/myapp?Name={_objOneProperty.Name}")));
            }

            [Test]
            public void WhenObjectOnePropertySetToEmpty_ThenReturnWithQueryString()
            {
                _objOneProperty.Name = string.Empty;

                var sut = new Uri("http://localhost/myapp");

                var result = sut.AddOrModifyQueryStringParams(_objOneProperty);

                Assert.That(result, Is.EqualTo(new Uri($"http://localhost/myapp?Name=")));
            }

            [Test]
            public void WhenObjectTwoPropertiesSet_ThenReturnWithQueryString()
            {
                var sut = new Uri("http://localhost/myapp");

                var result = sut.AddOrModifyQueryStringParams(_objTwoProperties);

                Assert.That(result, Is.EqualTo(new Uri($"http://localhost/myapp?Name={_objTwoProperties.Name}&Age={_objTwoProperties.Age}")));
            }

            [Test]
            public void WhenUriAlreadyHasProperties_ThenReplace()
            {
                var sut = new Uri("http://localhost/myapp?Name=Dave&Age=50&Children=2");

                var result = sut.AddOrModifyQueryStringParams(_objTwoProperties);

                Assert.That(result, Is.EqualTo(new Uri($"http://localhost/myapp?Name={_objTwoProperties.Name}&Age={_objTwoProperties.Age}&Children=2")));
            }

            [Test]
            public void WhenPropertyIsNull_ThenDoNotAddToQueryString()
            {
                _objTwoProperties.Name = null;

                var sut = new Uri("http://localhost/myapp");

                var result = sut.AddOrModifyQueryStringParams(_objTwoProperties);

                Assert.That(result, Is.EqualTo(new Uri($"http://localhost/myapp?Age={_objTwoProperties.Age}")));
            }

            public class ObjNoProperties
            {
            }

            public class ObjOneProperty
            {
                public string Name { get; set; }
            }

            public class ObjTwoProperties
            {
                public string Name { get; set; }
                public int Age { get; set; }
            }
        }

        [TestFixture]
        public class AddOrModifyQueryStringParam
        {
            [Test]
            public void WhenUriIsNull_ThenThrowException()
            {
                Uri sut = null;
                Assert.Throws<ArgumentNullException>(() => sut.AddOrModifyQueryStringParam("name", "value"));
            }

            [Test]
            public void WhenNameIsNull_ThenThrowException()
            {
                var sut = new Uri("http://localhost/myapp");

                Assert.Throws<ArgumentException>(() => sut.AddOrModifyQueryStringParam(null, "value"));
            }

            [Test]
            public void WhenNameIsEmpty_ThenThrowException()
            {
                var sut = new Uri("http://localhost/myapp");

                Assert.Throws<ArgumentException>(() => sut.AddOrModifyQueryStringParam(string.Empty, "value"));
            }

            [Test]
            public void WhenValueIsNull_AndParamExists_ThenRemoveParam()
            {
                var expected = new Uri("http://localhost/myapp");
                var sut = new Uri("http://localhost/myapp?name=value");

                var result = sut.AddOrModifyQueryStringParam("name", null);

                Assert.That(result, Is.EqualTo(expected));
            }

            [Test]
            public void WhenValueIsNull_AndParamNotExists_ThenNotRemoveParam()
            {
                var expected = new Uri("http://localhost/myapp");
                var sut = new Uri("http://localhost/myapp");

                var result = sut.AddOrModifyQueryStringParam("name", null);

                Assert.That(result, Is.EqualTo(expected));
            }

            [Test]
            public void WhenValueIsNotNull_AndParamDoesNotExist_ThenAddParam()
            {
                var expected = new Uri("http://localhost/myapp?name=value");
                var sut = new Uri("http://localhost/myapp");

                var result = sut.AddOrModifyQueryStringParam("name", "value");

                Assert.That(result, Is.EqualTo(expected));                
            }

            [Test]
            public void WhenValueIsNotNull_AndParamExists_ThenModifyParamValue()
            {
                var expected = new Uri("http://localhost/myapp?name=value2");
                var sut = new Uri("http://localhost/myapp?name=value");

                var result = sut.AddOrModifyQueryStringParam("name", "value2");
                
                Assert.That(result, Is.EqualTo(expected));                
            }
        }

        [TestFixture]
        public class RemoveQueryStringParam
        {
            [Test]
            public void WhenUriIsNull_ThenThrowException()
            {
                Uri sut = null;
                Assert.Throws<ArgumentNullException>(() => sut.RemoveQueryStringParam("name"));
            }

            [Test]
            public void WhenNameIsNull_ThenThrowException()
            {
                var sut = new Uri("http://localhost/myapp");

                Assert.Throws<ArgumentException>(() => sut.RemoveQueryStringParam(null));
            }

            [Test]
            public void WhenNameIsEmpty_ThenThrowException()
            {
                var sut = new Uri("http://localhost/myapp");

                Assert.Throws<ArgumentException>(() => sut.RemoveQueryStringParam(string.Empty));
            }

            [Test]
            public void WhenNameExists_ThenRemoveNamedPair()
            {
                var expected = new Uri("http://localhost/myapp");
                var sut = new Uri("http://localhost/myapp?name=value");

                var result = sut.RemoveQueryStringParam("name");

                Assert.That(result, Is.EqualTo(expected));
            }

            [Test]
            public void WhenNameExists_AndValueIsEmpty_ThenRemoveName()
            {
                var expected = new Uri("http://localhost/myapp");
                var sut = new Uri("http://localhost/myapp?name=");

                var result = sut.RemoveQueryStringParam("name");

                Assert.That(result, Is.EqualTo(expected));
            }

            [Test]
            public void WhenNamesNotExist_ThenNotRemoveName()
            {
                var expected = new Uri("http://localhost/myapp?name=value");
                var sut = new Uri(expected.ToString());

                var result = sut.RemoveQueryStringParam("anothername");

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class ClearQueryString
        {
            [Test]
            public void WhenUriIsNull_ThenThrowException()
            {
                Uri sut = null;
                Assert.Throws<ArgumentNullException>(() => sut.ClearQueryString());
            }

            [Test]
            public void WhenUriHasNoQueryString_ThenReturnUri()
            {
                var expected = new Uri("http://localhost/myapp");
                var sut = new Uri("http://localhost/myapp");

                var result = sut.ClearQueryString();

                Assert.That(result, Is.EqualTo(expected));
            }

            [Test]
            public void WhenUrlHasQuestionMarkOnly_ThenReturnWithoutQuestionMark()
            {
                var expected = new Uri("http://localhost/myapp");
                var sut = new Uri("http://localhost/myapp?");

                var result = sut.ClearQueryString();

                Assert.That(result, Is.EqualTo(expected));
            }

            [Test]
            public void WhenQueryStringHasName_AndNoValue_ThenReturnWithoutName()
            {
                var expected = new Uri("http://localhost/myapp");
                var sut = new Uri("http://localhost/myapp?name");

                var result = sut.ClearQueryString();

                Assert.That(result, Is.EqualTo(expected));
            }

            [Test]
            public void WhenQueryStringHasName_AndValue_ThenReturnWithoutNameAndValue()
            {
                var expected = new Uri("http://localhost/myapp");
                var sut = new Uri("http://localhost/myapp?name=value");

                var result = sut.ClearQueryString();

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class HasNoPathAndQuery
        {
            private const string UrlRoot = "http://localhost/";
            private const string UrlWithPathAndNoEndSlash = "http://localhost/path";
            private const string UrlWithPathAndEndSlash = "http://localhost/path/";
            private const string UrlWithNoPathAndQuery = "http://www.google.com/?something";

            [Test]
            public void WhenHasNoPathAndQuery_ThenReturnTrue()
            {
                var sut = new Uri(UrlRoot);

                var result = sut.HasNoPathAndQuery();

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenHasPath_ThenReturnFalse()
            {
                var sut = new Uri(UrlWithPathAndNoEndSlash);

                var result = sut.HasNoPathAndQuery();

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenHasPathWithEndSlash_ThenReturnFalse()
            {
                var sut = new Uri(UrlWithPathAndEndSlash);

                var result = sut.HasNoPathAndQuery();

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenHasQuery_ThenReturnFalse()
            {
                var sut = new Uri(UrlWithNoPathAndQuery);

                var result = sut.HasNoPathAndQuery();

                Assert.That(result, Is.False);
            }
        }

        [TestFixture]
        public class HasQuery
        {
            [Test]
            public void WhenHasQuery_ThenReturnTrue()
            {
                var sut = new Uri("http://local/app?name");

                var result = sut.HasQuery();

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenHasEmptyQuery_ThenReturnFalse()
            {
                var sut = new Uri("http://local/app?");

                var result = sut.HasQuery();

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenHasNoQuery_ThenReturnFalse()
            {
                var sut = new Uri("http://local/app");

                var result = sut.HasQuery();

                Assert.That(result, Is.False);
            }
        }

        [TestFixture]
        public class HasFragment
        {
            [Test]
            public void WhenHasFragment_ThenReturnTrue()
            {
                var sut = new Uri("http://local/app#fragment");

                var result = sut.HasFragment();

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenHasEmptyFragment_ThenReturnFalse()
            {
                var sut = new Uri("http://local/app#");

                var result = sut.HasFragment();

                Assert.That(result, Is.False);
            }
            
            [Test]
            public void WhenHasNoFragment_ThenReturnFalse()
            {
                var sut = new Uri("http://local/app");

                var result = sut.HasFragment();

                Assert.That(result, Is.False);
            }
        }

        [TestFixture]
        public class AddPath
        {
            [Test]
            public void WhenPathIsNull_ThenThrowException()
            {
                var sut = new Uri("http://www.google.com/");

                Assert.Throws<ArgumentException>(() => sut.AddPath(null));
            }

            [Test]
            public void WhenUriHasPath_ThenThrowException()
            {
                var sut = new Uri("http://www.google.com/path");

                Assert.Throws<InvalidOperationException>(() => sut.AddPath("/path/"));
            }

            [Test]
            public void WhenUriAndPathHaveSlash_ThenAppendPathWithOneSlash()
            {
                var sut = new Uri("http://www.google.com/");

                var result = sut.AddPath("/path");

                Assert.That(result.ToString(), Is.EqualTo("http://www.google.com/path"));
            }
        }
    }
}
