# AppSmith
C# MS-SQL Database table modeler.  

![SQLView](https://mmeents.github.io/files/AppSmithSqlTable.png)

[See Wiki](https://github.com/mmeents/AppSmith/wiki/Index) Index for more on Install and UI details.
App using TreeView Control that Reads/Saves as a Table/Dictionary via [MessagePack](https://github.com/MessagePack-CSharp/MessagePack-CSharp) to and from a file.    

This app is a port of [DBWorkshop](https://github.com/mmeents/DBWorkshop) a code generator that connects to a MSSql DB and generates code from objects found.  
  - Difference being this app does not connect to any database.  
  - Stores the model as a file in the file system using asm extension.

Tree is a model of a Server, Can have many APIs and DBs.  
  - Design Tables, Stored Procedues, Api(namespace), Controllers and Classes headers with right click in tree and Add menu item.
  - So far code parsing of SQL Tables and SQL Stored Procs into the tree model from pastable Input tab. 
    - Stored Procs Once defigned, generates a regular CSharp Repo Class methods. 
  - As Far as Generation, 
    - On Sql Table side, 
      - Standard Add Update stored procedure generation, 
      - foreach Cursor procedure generator. 
    - On CSharp Sql side, 
      - Entity Class for table
      - Repository Class output, Standard Dapper wrappers for
        - Get All 
        - Get Single by Id
        - Update via AddUpdate stored proc call
        - Delete row call 
      - FileTable Class generator in CSharp to showcase [FileTable](https://github.com/mmeents/FileTable) package.
      - Standard Add Update stored procedure for user defigned modeled stored procedures.
    - On Api side, Use tree to design c# output generation 
      - Right click on tree and Add Menu will reveal what types are available at the current level of the tree.
      - Generates Controller Classes output 
      - Design Generate Classes in general.  
      - Design Properties, Methods, Method Parameters. 
      - Interface generation for all classes, controllers. 
  - Code generation from the tree on the api side works for designing Api.  The Constructor and Di needs work in code gen part. 

Anyway it's a starter UML designer.  Full source included.  Clone a copy and modify to your own style add tabs, code is extendable.   

  - Project target Framework net48 showcases the following components 
    - Visual Studio Standard Tree View control, splitter, and tab controls.
  - Package FCTB version 2.16.24  - For the syntax highlighting text editors.
  - package MessagePack version 2.5.140 - For the high-performance object serialization  
  - package PropertyGridEx version 1.0.0 - Property editor to modify the data. 

Building and running the code yourself is the best way to ensure it works as intended. Downloading Visual Studio Community edition is a free and easy way to achieve this

Cheers


