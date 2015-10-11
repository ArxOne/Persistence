# Persistence
Allows to mark properties with a simple attribute and persist them in registry.

## How it works

Marking a property as persistent:
```csharp
[Persistent("MyPersistedProperty", DefaultValue=1)] // the DefaultValue is optionnal
public int MyProperty { get; set; }
```

Setting the application registry node:
```csharp
[assembly: RegistryPersistence(@"Software\MyApplication", CurrentUser = true)]
```

## Things to be considered

When two properties from different instances or different types use the same persistence name, their value always stays synchronized (changing a value in an instance changes it everywhere).
