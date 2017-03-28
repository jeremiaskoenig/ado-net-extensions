## ADO.NET Extensions

I'll be updating this with small extensions and helper classes I create for ADO.NET

### Usability Extensions

The [Usability Extensions](https://github.com/jeremiaskoenig/ado-net-extensions/blob/master/ExtensionMethods.cs) allow to mark a DataRow inside your class to represent the class inside your DataTable. 

```
class MyObject
{
    [DataRow]
    public DataRow DataRow { get; set; }

    [DataField("ID")]
    public Int32 ID { get; set; }

    [DataField("FirstName")]
    public String FirstName { get; set; }

    [DataField("LastName")]
    public String LastName { get; set; }
}

// Contains three Columns 'ID', 'FirstName', 'LastName'
DataTable Table { get; set; }

void Foo()
{
    MyObject myObj = new MyObject();
    myObj.DataRow = Table.NewRow();
    myObj.ID = 42;
    myObj.FirstName = "John";
    myObj.LastName = "Doe";
    
    // Update myObj.DataRow with the data of myObj's properties.
    myObj.UpdateDataRow();
}

void Bar()
{
    MyObject myObj = new MyObject();
    myObj.DataRow = Table.Rows[0];
    
    // Update myObj.ID, myObj.FirstName and myObj.LastName with
    // the data of myObj.DataRow
    myObj.UpdateProperties();
}
```
