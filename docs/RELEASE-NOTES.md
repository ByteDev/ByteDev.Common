# Release Notes

## 8.0.0 - ?? April 2020

Breaking changes:
- Removed Base64 class (see ByteDev.Base64 package)
- Removed Base64Serializer class (see ByteDev.Base64 package)
- Removed all types in Time namespace (see ByteDev.Time package)

New features:
- (None)

Bug fixes / internal changes:
- StringExtensions.Obscure now uses the provided obscure character.
- StringExtensions.Obscure now uses overloaded method rather than optional param.
- DateTimeExtensions.ToStringSortable now uses overloaded method rather than optional param.


## 7.0.0 - 28 Nov 2019

Breaking changes:
* A number of public method renames in DateTimeExtensions.
* Removed DateTimeExtensions.ConvertUtcToLocalDateTime().
* Removed DateTimeExtensions.ConvertLocalToUtcDateTime().
* FuncTimer is now in ByteDev.Common.Threading namespace.
* ReflectionAttributeExtensions now has clearer methods for Type, Object and MemberInfo for HasAttribute method.
* TakeFirstWithEllipsis is now LeftWithEllipsis.
* InnerTruncate is now LeftWithInnerEllipsis.

New features:
* (None)

Bug fixes / internal changes:
* Added XML documentation.

## 6.0.1 - 29 Sep 2019

Breaking changes:
* (None)

New features:
* (None)

Bug fixes / internal changes:
* ByteDev.Common no longer dependent on ByteDev.Collections.

## 6.0.0 - 29 Sep 2019

Breaking changes:
* Removed Collections namespace classes. Classes are now in the ByteDev.Collections nuget package.

New features:
* (None)

Bug fixes / internal changes:
* (None)
