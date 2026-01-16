Arebis.Text
===========

.NET Standard text processing library.

## Unidecoder extension

### Unidecode

Converts a string to an ASCII representation by replacing accented and special characters with their closest ASCII equivalent.

```csharp
// Standard to ASCII:
var unidecoded = "Some string".Unidecode();
// or expliciting the level:
var unidecoded = "Some string".Unidecode(UnidecoderLevel.Ascii);
```

