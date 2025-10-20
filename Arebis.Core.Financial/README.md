<a name='assembly'></a>
# Arebis.Core.Financial

## Contents

- [CreditCardHelper](#T-Arebis-Core-Financial-CreditCardHelper 'Arebis.Core.Financial.CreditCardHelper')
  - [IdentifyType()](#M-Arebis-Core-Financial-CreditCardHelper-IdentifyType-System-String- 'Arebis.Core.Financial.CreditCardHelper.IdentifyType(System.String)')
- [CreditCardType](#T-Arebis-Core-Financial-CreditCardType 'Arebis.Core.Financial.CreditCardType')
  - [AmericanExpress](#F-Arebis-Core-Financial-CreditCardType-AmericanExpress 'Arebis.Core.Financial.CreditCardType.AmericanExpress')
  - [DinersClub](#F-Arebis-Core-Financial-CreditCardType-DinersClub 'Arebis.Core.Financial.CreditCardType.DinersClub')
  - [Discover](#F-Arebis-Core-Financial-CreditCardType-Discover 'Arebis.Core.Financial.CreditCardType.Discover')
  - [Invalid](#F-Arebis-Core-Financial-CreditCardType-Invalid 'Arebis.Core.Financial.CreditCardType.Invalid')
  - [JCB](#F-Arebis-Core-Financial-CreditCardType-JCB 'Arebis.Core.Financial.CreditCardType.JCB')
  - [Maestro](#F-Arebis-Core-Financial-CreditCardType-Maestro 'Arebis.Core.Financial.CreditCardType.Maestro')
  - [Mastercard](#F-Arebis-Core-Financial-CreditCardType-Mastercard 'Arebis.Core.Financial.CreditCardType.Mastercard')
  - [Other](#F-Arebis-Core-Financial-CreditCardType-Other 'Arebis.Core.Financial.CreditCardType.Other')
  - [UnionPay](#F-Arebis-Core-Financial-CreditCardType-UnionPay 'Arebis.Core.Financial.CreditCardType.UnionPay')
  - [Unknown](#F-Arebis-Core-Financial-CreditCardType-Unknown 'Arebis.Core.Financial.CreditCardType.Unknown')
  - [VISA](#F-Arebis-Core-Financial-CreditCardType-VISA 'Arebis.Core.Financial.CreditCardType.VISA')
- [CreditorReference](#T-Arebis-Core-Financial-CreditorReference 'Arebis.Core.Financial.CreditorReference')
  - [AssertValid(reference,allowNullOrEmpty,countryCodeHint)](#M-Arebis-Core-Financial-CreditorReference-AssertValid-System-String,System-Boolean,System-String- 'Arebis.Core.Financial.CreditorReference.AssertValid(System.String,System.Boolean,System.String)')
  - [From(id,minLength,countryCode)](#M-Arebis-Core-Financial-CreditorReference-From-System-Int64,System-Byte,System-String- 'Arebis.Core.Financial.CreditorReference.From(System.Int64,System.Byte,System.String)')
  - [ReplaceLettersByNumbers(str)](#M-Arebis-Core-Financial-CreditorReference-ReplaceLettersByNumbers-System-String- 'Arebis.Core.Financial.CreditorReference.ReplaceLettersByNumbers(System.String)')
  - [Validate(reference,countryCodeHint)](#M-Arebis-Core-Financial-CreditorReference-Validate-System-String,System-String- 'Arebis.Core.Financial.CreditorReference.Validate(System.String,System.String)')
- [CreditorReferenceInfo](#T-Arebis-Core-Financial-CreditorReference-CreditorReferenceInfo 'Arebis.Core.Financial.CreditorReference.CreditorReferenceInfo')
  - [CountryCode](#P-Arebis-Core-Financial-CreditorReference-CreditorReferenceInfo-CountryCode 'Arebis.Core.Financial.CreditorReference.CreditorReferenceInfo.CountryCode')
  - [CreditorReference](#P-Arebis-Core-Financial-CreditorReference-CreditorReferenceInfo-CreditorReference 'Arebis.Core.Financial.CreditorReference.CreditorReferenceInfo.CreditorReference')
  - [ErrorCause](#P-Arebis-Core-Financial-CreditorReference-CreditorReferenceInfo-ErrorCause 'Arebis.Core.Financial.CreditorReference.CreditorReferenceInfo.ErrorCause')
  - [ErrorMessage](#P-Arebis-Core-Financial-CreditorReference-CreditorReferenceInfo-ErrorMessage 'Arebis.Core.Financial.CreditorReference.CreditorReferenceInfo.ErrorMessage')
  - [Id](#P-Arebis-Core-Financial-CreditorReference-CreditorReferenceInfo-Id 'Arebis.Core.Financial.CreditorReference.CreditorReferenceInfo.Id')
  - [IsValid](#P-Arebis-Core-Financial-CreditorReference-CreditorReferenceInfo-IsValid 'Arebis.Core.Financial.CreditorReference.CreditorReferenceInfo.IsValid')
- [IBAN](#T-Arebis-Core-Financial-IBAN 'Arebis.Core.Financial.IBAN')
  - [AssertValid(ibanNumber,allowNullOrEmpty,useAdvancedValidation)](#M-Arebis-Core-Financial-IBAN-AssertValid-System-String,System-Boolean,System-Boolean- 'Arebis.Core.Financial.IBAN.AssertValid(System.String,System.Boolean,System.Boolean)')
  - [Formatted(ibanNumber,includeSpaceSeparators)](#M-Arebis-Core-Financial-IBAN-Formatted-System-String,System-Boolean- 'Arebis.Core.Financial.IBAN.Formatted(System.String,System.Boolean)')
  - [IbanDataList()](#M-Arebis-Core-Financial-IBAN-IbanDataList 'Arebis.Core.Financial.IBAN.IbanDataList')
  - [ReplaceLettersByNumbers(str)](#M-Arebis-Core-Financial-IBAN-ReplaceLettersByNumbers-System-String- 'Arebis.Core.Financial.IBAN.ReplaceLettersByNumbers(System.String)')
  - [Validate(ibanNumber,useAdvancedValidation)](#M-Arebis-Core-Financial-IBAN-Validate-System-String,System-Boolean- 'Arebis.Core.Financial.IBAN.Validate(System.String,System.Boolean)')
- [IbanInfo](#T-Arebis-Core-Financial-IBAN-IbanInfo 'Arebis.Core.Financial.IBAN.IbanInfo')
  - [CountryCode](#P-Arebis-Core-Financial-IBAN-IbanInfo-CountryCode 'Arebis.Core.Financial.IBAN.IbanInfo.CountryCode')
  - [ErrorCause](#P-Arebis-Core-Financial-IBAN-IbanInfo-ErrorCause 'Arebis.Core.Financial.IBAN.IbanInfo.ErrorCause')
  - [ErrorMessage](#P-Arebis-Core-Financial-IBAN-IbanInfo-ErrorMessage 'Arebis.Core.Financial.IBAN.IbanInfo.ErrorMessage')
  - [IBAN](#P-Arebis-Core-Financial-IBAN-IbanInfo-IBAN 'Arebis.Core.Financial.IBAN.IbanInfo.IBAN')
  - [IsValid](#P-Arebis-Core-Financial-IBAN-IbanInfo-IsValid 'Arebis.Core.Financial.IBAN.IbanInfo.IsValid')
- [NProef](#T-Arebis-Core-Financial-NProef 'Arebis.Core.Financial.NProef')

<a name='T-Arebis-Core-Financial-CreditCardHelper'></a>
## CreditCardHelper `type`

##### Namespace

Arebis.Core.Financial

##### Summary

Helper methods to operate credit cards.

<a name='M-Arebis-Core-Financial-CreditCardHelper-IdentifyType-System-String-'></a>
### IdentifyType() `method`

##### Summary

Tries to identify the type of credit card given it's number.

##### Parameters

This method has no parameters.

<a name='T-Arebis-Core-Financial-CreditCardType'></a>
## CreditCardType `type`

##### Namespace

Arebis.Core.Financial

##### Summary

Type/brand of a credit card.

<a name='F-Arebis-Core-Financial-CreditCardType-AmericanExpress'></a>
### AmericanExpress `constants`

##### Summary

American Express.

<a name='F-Arebis-Core-Financial-CreditCardType-DinersClub'></a>
### DinersClub `constants`

##### Summary

Diners Club or Carte Blanche.

<a name='F-Arebis-Core-Financial-CreditCardType-Discover'></a>
### Discover `constants`

##### Summary

Discover.

<a name='F-Arebis-Core-Financial-CreditCardType-Invalid'></a>
### Invalid `constants`

##### Summary

An invalid cart type.

<a name='F-Arebis-Core-Financial-CreditCardType-JCB'></a>
### JCB `constants`

##### Summary

JCB.

<a name='F-Arebis-Core-Financial-CreditCardType-Maestro'></a>
### Maestro `constants`

##### Summary

Maestro.

<a name='F-Arebis-Core-Financial-CreditCardType-Mastercard'></a>
### Mastercard `constants`

##### Summary

Mastercard.

<a name='F-Arebis-Core-Financial-CreditCardType-Other'></a>
### Other `constants`

##### Summary

An other card type.

<a name='F-Arebis-Core-Financial-CreditCardType-UnionPay'></a>
### UnionPay `constants`

##### Summary

UnionPay.

<a name='F-Arebis-Core-Financial-CreditCardType-Unknown'></a>
### Unknown `constants`

##### Summary

Unknown credit card type.

<a name='F-Arebis-Core-Financial-CreditCardType-VISA'></a>
### VISA `constants`

##### Summary

VISA or Visa Electron.

<a name='T-Arebis-Core-Financial-CreditorReference'></a>
## CreditorReference `type`

##### Namespace

Arebis.Core.Financial

##### Summary

Generates and validates "Structured Creditor References" according to the IDO 11649 scheme or some national scheme.

<a name='M-Arebis-Core-Financial-CreditorReference-AssertValid-System-String,System-Boolean,System-String-'></a>
### AssertValid(reference,allowNullOrEmpty,countryCodeHint) `method`

##### Summary

Asserts the given value is a valid creditor reference.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| reference | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The reference to validate. |
| allowNullOrEmpty | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Whether a null or empty reference is considered valid. |
| countryCodeHint | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional alpha-2 ISO 3166 country code hint to help determine the right reference scheme. |

<a name='M-Arebis-Core-Financial-CreditorReference-From-System-Int64,System-Byte,System-String-'></a>
### From(id,minLength,countryCode) `method`

##### Summary

Returns a formatted creditor reference for the given id.
If minLength <= 10 and countryCode = "BE", then a Belgian OGM reference is returned.
Otherwise, an ISO 11649 formatted reference is returned.

##### Returns

A formatted creditor reference.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| id | [System.Int64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64 'System.Int64') | The id value. |
| minLength | [System.Byte](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte 'System.Byte') | The minimum length of the creditor reference. |
| countryCode | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional alpha-2 ISO 3166 country code for which to use scheme if possible. |

<a name='M-Arebis-Core-Financial-CreditorReference-ReplaceLettersByNumbers-System-String-'></a>
### ReplaceLettersByNumbers(str) `method`

##### Summary

Replaces "A" by "10", "B" by "11", etc.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| str | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Input string, assumed to contain only 0-9 and A-Z characters. |

<a name='M-Arebis-Core-Financial-CreditorReference-Validate-System-String,System-String-'></a>
### Validate(reference,countryCodeHint) `method`

##### Summary

Validates the given credit reference.

##### Returns

Information about, and about the validity, of the given credit reference.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| reference | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The reference to validate. |
| countryCodeHint | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional alpha-2 ISO 3166 country code hint to help determine the right reference scheme. |

<a name='T-Arebis-Core-Financial-CreditorReference-CreditorReferenceInfo'></a>
## CreditorReferenceInfo `type`

##### Namespace

Arebis.Core.Financial.CreditorReference

<a name='P-Arebis-Core-Financial-CreditorReference-CreditorReferenceInfo-CountryCode'></a>
### CountryCode `property`

##### Summary

Alpha-2 ISO 3166 country code, if creditor reference was identified as a country-specific scheme.

<a name='P-Arebis-Core-Financial-CreditorReference-CreditorReferenceInfo-CreditorReference'></a>
### CreditorReference `property`

##### Summary

The original creditor reference.

<a name='P-Arebis-Core-Financial-CreditorReference-CreditorReferenceInfo-ErrorCause'></a>
### ErrorCause `property`

##### Summary

Cause of error or NoError.

<a name='P-Arebis-Core-Financial-CreditorReference-CreditorReferenceInfo-ErrorMessage'></a>
### ErrorMessage `property`

##### Summary

Error or success message.

<a name='P-Arebis-Core-Financial-CreditorReference-CreditorReferenceInfo-Id'></a>
### Id `property`

##### Summary

The id value the creditor reference stands for, of this could be parsed.
(Only numerical creditor references can be reverted to an id).

<a name='P-Arebis-Core-Financial-CreditorReference-CreditorReferenceInfo-IsValid'></a>
### IsValid `property`

##### Summary

Whether the related IBAN number is valid.

<a name='T-Arebis-Core-Financial-IBAN'></a>
## IBAN `type`

##### Namespace

Arebis.Core.Financial

##### Summary

Formats and validates IBAN account numbers.

<a name='M-Arebis-Core-Financial-IBAN-AssertValid-System-String,System-Boolean,System-Boolean-'></a>
### AssertValid(ibanNumber,allowNullOrEmpty,useAdvancedValidation) `method`

##### Summary

Asserts the given IBAN number is valid.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ibanNumber | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The given IBAN number. |
| allowNullOrEmpty | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Whether null or empty values are considered valid. |
| useAdvancedValidation | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Whether to use advanced (country specific) validation rules. |

<a name='M-Arebis-Core-Financial-IBAN-Formatted-System-String,System-Boolean-'></a>
### Formatted(ibanNumber,includeSpaceSeparators) `method`

##### Summary

Cleans, capitalizes and formats the given IBAN account number.

##### Returns

Formatted IBAN number.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ibanNumber | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Input IBAN number. |
| includeSpaceSeparators | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Whether to include space separators. |

<a name='M-Arebis-Core-Financial-IBAN-IbanDataList'></a>
### IbanDataList() `method`

##### Summary

List of country specific IBAN validation rules.
Source: https://www.codeproject.com/Articles/55667/IBAN-Verification-in-C-Excel-Automation-Add-in-Wor, Sedar Altug, 2010
Check updates @ http://www.tbg5-finance.org/checkiban.js

##### Parameters

This method has no parameters.

<a name='M-Arebis-Core-Financial-IBAN-ReplaceLettersByNumbers-System-String-'></a>
### ReplaceLettersByNumbers(str) `method`

##### Summary

Replaces "A" by "10", "B" by "11", etc.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| str | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Input string, assumed to contain only 0-9 and A-Z characters. |

<a name='M-Arebis-Core-Financial-IBAN-Validate-System-String,System-Boolean-'></a>
### Validate(ibanNumber,useAdvancedValidation) `method`

##### Summary

Validates the given IBAN number.

##### Returns

Information about, and about the validity, of the given IBAN number.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ibanNumber | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The given IBAN number. |
| useAdvancedValidation | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Whether to use advanced (country specific) validation rules. |

<a name='T-Arebis-Core-Financial-IBAN-IbanInfo'></a>
## IbanInfo `type`

##### Namespace

Arebis.Core.Financial.IBAN

<a name='P-Arebis-Core-Financial-IBAN-IbanInfo-CountryCode'></a>
### CountryCode `property`

##### Summary

Alpha-2 ISO 3166 country code.

<a name='P-Arebis-Core-Financial-IBAN-IbanInfo-ErrorCause'></a>
### ErrorCause `property`

##### Summary

Cause of error or NoError.

<a name='P-Arebis-Core-Financial-IBAN-IbanInfo-ErrorMessage'></a>
### ErrorMessage `property`

##### Summary

Error or success message.

<a name='P-Arebis-Core-Financial-IBAN-IbanInfo-IBAN'></a>
### IBAN `property`

##### Summary

Fully formatted IBAN number.

<a name='P-Arebis-Core-Financial-IBAN-IbanInfo-IsValid'></a>
### IsValid `property`

##### Summary

Whether the related IBAN number is valid.

<a name='T-Arebis-Core-Financial-NProef'></a>
## NProef `type`

##### Namespace

Arebis.Core.Financial

##### Summary

Implements the Dutch "NegenProef", "ElfProef", ... "NProef" checksum algorithm.
