# Extension Methods

The library ships two sets of extension methods for the most common follow-up operations after parsing a CF or working with the municipality dataset.

## 1. FiscalCodeExtensions

These are extension methods on `FiscalCodeData`, so you can call them directly on whatever `Parse` or `TryParse` returns.

### 1.1 `GetAge`

Returns how old the person is today, in full years.

```csharp
FiscalCodeData data = FiscalCodeParser.Parse("RSSMRA80A01H501U");
int age = data.GetAge(); // e.g. 45
```

> **Heads-up:** the current implementation compares year and month, but not the exact day. If today happens to fall in the same month as the birthday but before the day, the result will be 1 year too high.

### 1.2 `IsAdult`

Returns `true` if the person is 18 or older — just a shorthand for `GetAge() >= 18`.

```csharp
bool adult = data.IsAdult();
```

## 2. MunicipalityExtensions

These let you query the ~7 896 Italian municipalities embedded in the library. All lookups hit an in-memory list loaded once on first use.

### 2.1 `GetMunicipalityByBelfiore`

Find a municipality by its Belfiore cadastral code. **The comparison is case-sensitive** — always use the uppercase form.

```csharp
Municipality? comune = "H501".GetMunicipalityByBelfiore(); // Roma
```

### 2.2 `GetMunicipalityByName`

Find a municipality by name — case-insensitive, so `"roma"` and `"Roma"` both work.

```csharp
Municipality? comune = "milano".GetMunicipalityByName(); // Milano
```

### 2.3 `GetMunicipalityByCAP`

Find a municipality by its postal code (CAP).

```csharp
Municipality? comune = "00186".GetMunicipalityByCAP(); // Roma
```

### 2.4 `GetMunicipalityByCode`

Find a municipality by its ISTAT code.

```csharp
Municipality? comune = "058091".GetMunicipalityByCode(); // Roma
```

### 2.5 `GetAllByProvince`

Get all municipalities in a province — case-insensitive.

```csharp
IEnumerable<Municipality>? comuni = "Roma".GetAllByProvince(); // 121 results
```

### 2.6 `GetAll`

Get the full dataset.

```csharp
IEnumerable<Municipality> all = MunicipalityExtensions.GetAll(); // ~7 896
```

## 3. Putting it all together

A typical pattern: parse a CF and look up where the person was born.

```csharp
FiscalCodeData data  = FiscalCodeParser.Parse("RSSMRA80A01H501U");
Municipality?     place = data.BelfioreCode.GetMunicipalityByBelfiore();

Console.WriteLine($"Born in: {place?.Name}, {place?.Province.Name}");
// Born in: Roma, Roma
```
