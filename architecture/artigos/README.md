# Architecture Annotations:

# Architecture Annotations:

* Abstract Classes: That one's who represent a base for a class, and will not be instanced, by inherited.
* Static Class: The one's who i can use and call without create an instance it.
* public sealed class Person: No one can inheritance or extends it. Just Use it.
* Virtual Method: When i put a virtual method on a abstract class (or a normal one), i will always have to
"overwrite" it on the class, so, the "implementation" will be on the class who make inheritance of that.
* Internal Method: When i define something with "internal", just the mother-class can call. So if i have an
"order" and "orderItem" and on otherItem i will have a method "internal void AssociateOrder(Guid order)",
this method internal on orderItem can just be called by mother ORDER.


------------------------------------------------------------------------------------------------------------