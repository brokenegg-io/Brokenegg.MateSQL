# MateSQL


![GitHub License](https://img.shields.io/github/license/brokenegg-io/Brokenegg.MateSQL)
[![Version](https://img.shields.io/badge/version-1.1.0-brightgreen.svg)](https://semver.org)
![Actions](https://github.com/brokenegg-io/Brokenegg.MateSQL/actions/workflows/ci.yml/badge.svg)
![Actions](https://github.com/brokenegg-io/Brokenegg.MateSQL/actions/workflows/release.yml/badge.svg)
![GitHub branch checks state](https://img.shields.io/github/checks-status/brokenegg-io/Brokenegg.MateSQL/dev)
![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/brokenegg-io/Brokenegg.MateSQL)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Brokenegg.MateSQL.svg)](https://www.nuget.org/packages/Brokenegg.MateSQL/)

## Quick links

* [Installation](#installation)
* [Usage](#usage)
    * [Initializing](#initializing-dbconfig)  
    * [Instantiating object](#instantiating-object)
    * [Listing users](#listing-users)
    * [Authenticate](#authenticate)
    * [Inserting](#inserting)
* [Contributing](#contributing)
* [Roadmap](#roadmap)
* [License](#license)

## Installation

Our profile on the NuGet library [brokenegg.io](https://www.nuget.org/profiles/brokenegg.io)

Use the package manager [NuGet](https://www.nuget.org/) to install MateSQL.

```bash
nuget install brokenegg.matesql
```

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

### Initializing DBConfig

```csharp
DBConfig Conf = new DBConfig();
Conf.Database = "database";
Conf.Password = "password";
Conf.User = "username";
Conf.Server = "serverip";
```

## Instantiating object
```csharp
User User = new User();
User.SetDbConfig(Conf);
```

## Listing users
```csharp
List<User> users = User.Select<User>();
```

## Authenticate
```csharp
User user = User.Autenticate<User>(txtUsername.Text, txtPassword.Text);
```

## Inserting

```csharp
User.Name = "Jose";
User.Email = "jose@gmail.com";
User.Password = "secret";
User.Insert();
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

[get@brokenegg.io](mailto:get@brokenegg.io)

## Roadmap

- [ ] Port from .Net Framework 4.5.1 to DotNet 5. Planned to v2.0.0;

## License
[MIT](https://choosealicense.com/licenses/mit/)

