# C# Coding Standards and Naming Conventions


| Object Name               | Notation   | Length | Plural | Prefix | Suffix | Abbreviation | Char Mask          | Underscores |
|:--------------------------|:-----------|-------:|:-------|:-------|:-------|:-------------|:-------------------|:------------|
| Class name                | PascalCase |    128 | No     | No     | Yes    | No           | [A-z][0-9]         | No          |
| Constructor name          | PascalCase |    128 | No     | No     | Yes    | No           | [A-z][0-9]         | No          |
| Method name               | camelCase  |    128 | Yes    | No     | No     | No           | [A-z][0-9]         | No          |
| Method arguments          | camelCase  |    128 | Yes    | No     | No     | Yes          | [A-z][0-9]         | No          |
| Local variables           | lower_case |     50 | Yes    | No     | No     | Yes          | [A-z][0-9]         | No          |
| Constants name            | UPPERCASE  |     50 | No     | No     | No     | No           | [A-z][0-9]         | No          |
| Field name                | lower_case |     50 | Yes    | No     | No     | Yes          | [A-z][0-9]         | Yes         |
| Properties name           | PascalCase |     50 | Yes    | No     | No     | Yes          | [A-z][0-9]         | No          |
| Inspector name            | PascalCase |     50 | Yes    | No     | No     | Yes          | [A-z][0-9]         | No          |
| Delegate name             | PascalCase |    128 | No     | No     | Yes    | Yes          | [A-z]              | No          |
| Enum type name            | PascalCase |    128 | Yes    | No     | No     | No           | [A-z]              | No          |
| Enum type value           | UPPERCASE  |    128 | Yes    | No     | No     | No           | [A-z]              | No          |
| Source file name          | lowercase  |    128 | No     | No     | No     | No           | [A-z]              | No          |

#### 1. Do use PascalCasing for class names:

```csharp
public class ClientActivity
{
  public void clearStatistics()
  {
    //...
  }
  public void calculateStatistics()
  {
    //...
  }
}
```

#### 2. Do use camelCasing for method arguments:

```csharp
public class UserLog
{
  public void add(LogEvent logEvent)
  {
    int item_count = logEvent.Items.Count;
    // ...
  }
}
```

#### 3. Do not use Hungarian notation or any other type identification in identifiers

```csharp
// Correct
int counter;
string name;    
// Avoid
int i_counter;
string str_name;
```

***Why: Visual Studio IDE makes determining types very easy (via tooltips). In general you want to avoid type indicators in any identifier.***

#### 4. Do use Screaming Caps for constants or readonly variables:

```csharp
// Correct
public const string SHIPPINGTYPE = "DropShip";
// Avoid
public const string ShippingType = "DropShip";
```

#### 5. Use meaningful names for variables. The following example uses seattle_customers for customers who are located in Seattle:

```csharp
var seattle_customers = from customer in customers
  where customer.City == "Seattle" 
  select customer.Name;
```

#### 6. Avoid using Abbreviations. Exceptions: abbreviations commonly used as names, such as Id, Xml, Ftp, Uri.

```csharp    
// Correct
UserGroup user_group;
Assignment employee_assignment;     
// Avoid
UserGroup usr_grp;
Assignment emp_assignment; 
// Exceptions
CustomerId customer_id;
XmlDocument xml_document;
FtpHelper ftp_helper;
UriPart uri_part;
```

***Prevents inconsistent abbreviations.***

#### 7. Do not use Underscores in identifiers. Exception: you can prefix private fields with an underscore:

```csharp 
// Correct
public DateTime clientAppointment;
public TimeSpan timeLeft;    
// Avoid
public DateTime client_Appointment;
public TimeSpan time_Left; 
// Exception (Class field)
private DateTime _registrationDate;
```

***Why: consistent with the Microsoft's .NET Framework and makes code more natural to read (without 'slur'). Also avoids underline stress (inability to see underline).***

#### 8. Do use predefined type names (C# aliases) like `int`, `float`, `string` for local, parameter and member declarations. Do use .NET Framework names like `Int32`, `Single`, `String` when accessing the type's static members like `Int32.TryParse` or `String.Join`.

```csharp
// Correct
string firstName;
int lastIndex;
bool isSaved;
string commaSeparatedNames = String.Join(", ", names);
int index = Int32.Parse(input);
// Avoid
String firstName;
Int32 lastIndex;
Boolean isSaved;
string commaSeparatedNames = string.Join(", ", names);
int index = int.Parse(input);
```

***Why: consistent with the Microsoft's .NET Framework and makes code more natural to read.*** 

#### 9. Avoid using implicit type var for local variable declarations. Exception: primitive types (int, string, double, etc) use predefined names. 

```csharp 
//Correct
int index = 100;
string timeSheet;
bool isCompleted;
//Avoid
var stream = File.Create(path);
var customers = new Dictionary();
```

***Why: removes clutter, particularly with complex generic types. Type is easily detected with Visual Studio tooltips.***

#### 10. Do use noun or noun phrases to name a class. 

```csharp 
public class Employee
{
}
public class BusinessLocation
{
}
public class DocumentCollection
{
}
```

***Why: consistent with the Microsoft's .NET Framework and easy to remember.***

#### 11. Do prefix interfaces with the letter I. Interface names are noun (phrases) or adjectives.

```csharp     
public interface IShape
{
}
public interface IShapeCollection
{
}
public interface IGroupable
{
}
```

***Why: consistent with the Microsoft's .NET Framework.***

#### 12. Do name source files according to their main classes but make them lowercase. Exception: file names with partial classes reflect their source or purpose, e.g. designer, generated, etc. 

```csharp 
// Located in task.cs
public partial class Task
{
}
// Located in task.generated.cs
public partial class Task
{
}
```

***Why: Files are alphabetically sorted and partial classes remain adjacent.***

#### 13. Do organize namespaces with a clearly defined structure: 

```csharp 
// Examples
namespace Company.Product.Module.SubModule
{
}
namespace Product.Module.Component
{
}
namespace Product.Layer.Module.Group
{
}
```

***Why: Maintains good organization of your code base.***

#### 14. Do vertically align curly brackets: 

```csharp 
// Correct
class Program
{
  static void Main(string[] args)
  {
    //...
  }
}
```

***Why: Microsoft has a different standard, but developers have overwhelmingly preferred vertically aligned brackets.***

#### 15. Do declare all member variables at the top of a class, with static variables at the very top.

```csharp 
// Correct
public class Account
{
  public static string BACKNAME;
  public static decimal RESERVES;
  
  public string Number { get; set; }
  public DateTime DateOpened { get; set; }
  public DateTime DateClosed { get; set; }
  public decimal Balance { get; set; }    
  
  // Constructor
  public Account()
  {
    // ...
  }
}
```

***Why: generally accepted practice that prevents the need to hunt for variable declarations.***

#### 16. Do use singular names for enums. Exception: bit field enums.

```csharp 
// Correct
public enum Color
{
  RED,
  GREEN,
  BLUE,
  YELLOW,
  MAGENTA,
  CYAN
} 
// Exception
[Flags]
public enum Dockings
{
  NONE = 0,
  TOP = 1,
  RIGHT = 2, 
  BOTTOM = 4,
  LEFT = 8
}
```

***Why: consistent with the Microsoft's .NET Framework and makes the code more natural to read. Plural flags because enum can hold multiple values (using bitwise 'OR').***

#### 17. Do not explicitly specify a type of an enum or values of enums (except bit fields):

```csharp 
// Don't
public enum Direction : long
{
  NORTH = 1,
  EAST = 2,
  SOUTH = 3,
  WEST = 4
} 
// Correct
public enum Direction
{
  NORTH,
  EAST,
  SOUTH,
  WEST
}
```

***Why: can create confusion when relying on actual types and values.***

#### 18. Do not use an "Enum" suffix in enum type names:

```csharp     
// Don't
public enum CoinEnum
{
  PENNY,
  NICKEL,
  DIME,
  QUARTER,
  DOLLAR
} 
// Correct
public enum Coin
{
  PENNY,
  NICKEL,
  DIME,
  QUARTER,
  DOLLAR
}
```

***Why: consistent with the Microsoft's .NET Framework and consistent with prior rule of no type indicators in identifiers.***

#### 19. Do not use "Flag" or "Flags" suffixes in enum type names:

```csharp 
// Don't
[Flags]
public enum DockingsFlags
{
  NONE = 0,
  TOP = 1,
  RIGHT = 2, 
  BOTTOM = 4,
  LEFT = 8
}
// Correct
[Flags]
public enum Dockings
{
  NONE = 0,
  TOP = 1,
  RIGHT = 2, 
  BOTTOM = 4,
  LEFT = 8
}
```

***Why: consistent with the Microsoft's .NET Framework and consistent with prior rule of no type indicators in identifiers.***

#### 20. Do use suffix EventArgs at creation of the new classes comprising the information on event:

```csharp 
// Correct
public class BarcodeReadEventArgs : System.EventArgs
{
}
```

***Why: consistent with the Microsoft's .NET Framework and easy to read.***

#### 21. Do name event handlers (delegates used as types of events) with the "EventHandler" suffix, as shown in the following example:

```csharp 
public delegate void ReadBarcodeEventHandler(object sender, ReadBarcodeEventArgs e);
```

***Why: consistent with the Microsoft's .NET Framework and easy to read.***

#### 22. Do not create names of parameters in methods (or constructors) which differ only by the register:

```csharp 
// Avoid
private void MyFunction(string name, string Name)
{
  //...
}
```

***Why: consistent with the Microsoft's .NET Framework and easy to read, and also excludes possibility of occurrence of conflict situations.***

#### 23. Do use two parameters named sender and e in event handlers. The sender parameter represents the object that raised the event. The sender parameter is typically of type object, even if it is possible to employ a more specific type.

```csharp
public void ReadBarcodeEventHandler(object sender, ReadBarcodeEventArgs e)
{
  //...
}
```

***Why: consistent with the Microsoft's .NET Framework***

***Why: consistent with the Microsoft's .NET Framework and consistent with prior rule of no type indicators in identifiers.***

#### 24. Do use suffix Exception at creation of the new classes comprising the information on exception:

```csharp 
// Correct
public class BarcodeReadException : System.Exception
{
}
```

***Why: consistent with the Microsoft's .NET Framework and easy to read.***

#### 25. Do use suffix Any, Is, Have or similar keywords for boolean identifier :

```csharp 
// Correct
public static bool IsNullOrEmpty(string value) {
    return (value == null || value.Length == 0);
}
```

***Why: consistent with the Microsoft's .NET Framework and easy to read.***

#### 26. Add a "region" preprocessor around Unity Messages

```csharp 
#region Unity Messages

private void Awake()
{
	// ...
}

#endregion
```

***Why: This will make it easier to distinguish between our implementation and Unity code.***

#### 27. Explicitly write the namespace if inherited classes have the same name but inside a different namespace

```csharp 
namespace Cosmo
{
	public class Spawner : Sophia.Platform.Spawner<Part>
	{
		// ...
	}
}
```

***Why: Avoid ambiguity.***

#### 28. Write a separator between functions

```csharp 
#region Unity Messages

//--------------------------------------------------------------------------------------
private void Awake()
{
	// ...
}
//--------------------------------------------------------------------------------------
private void Update()
{
	// ...
}

#endregion

//--------------------------------------------------------------------------------------
private void doSomething()
{
	// ...
}
//--------------------------------------------------------------------------------------
private void doSomethingElse()
{
	// ...
}

```

***Why: Increase readability of the code.***

#### 29. Always make Unity Messages private, Exception: when they need to be overriden use protected.

```csharp 
#region Unity Messages

//--------------------------------------------------------------------------------------
private void Awake()
{
	// ...
}
//--------------------------------------------------------------------------------------
private void Update()
{
	// ...
}

#endregion
```

***Why: Always shield you code.***

#### 30. Always make inspector members private, but specify a [SerializeField]

```csharp 
//Don't
public int SomeValue;
public int SomeOtherValue;
//Correct
[SerializeField]
private int SomeValue;
[SerializeField]
private int SomeOtherValue;
```

***Why: Always shield you code.***