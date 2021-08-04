# MateSQL

## Usage

Creating a class and instantiating Autenticable
To any other classes extends from MateSQL.DBModel

```csharp
public class User : MateSQL.Autenticable
{
    public String Name { get; set; }
    public String Email { get; set; }
    public String Password { get; set; }
}
```

Initializing DBConfig

```csharp
DBConfig Conf = new DBConfig();
Conf.Database = "database";
Conf.Password = "password";
Conf.User = "username";
Conf.Server = "serverip";
```

Instantiating object and definig DBConfig
```csharp
User User = new User();
User.SetDbConfig(Conf);
```

Listing all objects from a table
```csharp
List<User> users = User.Select<User>();
```

Using Autenticate method
```csharp
User user = User.Autenticate<User>(txtUsername.Text, txtPassword.Text);
```

Inserting User on database

```csharp
User.Name = "Jose";
User.Email = "jose@gmail.com";
User.Password = "secret";
User.Insert();
```

