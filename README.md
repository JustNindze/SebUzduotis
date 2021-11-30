# SebUzduotis

Exercise for SEB job interview.

## Description

I made API which has method called AddAgreementToUser. This method requires personalId parameter and writes new Agreement data to database (sqlite). After writing 
data this method returns (PersonalId, FirstName, LastName, LastAgreement, LastInterestRate, NewInterestRate, Difference) from database.
Next step would be to create Web application which will use this API to write new Agreements to database and also get required data and show that to application user.

## Getting Started

### Prerequisites

* [.NET SDK (5.0.0)](https://dotnet.microsoft.com/download/dotnet/5.0)
* [Microsoft.EntityFrameworkCore (5.0.0)](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/5.0.0?_src=template)
* [Microsoft.EntityFrameworkCore.Design (5.0.0)](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/5.0.0?_src=template)
* [Microsoft.EntityFrameworkCore.Sqlite (5.0.0)](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Sqlite/5.0.0?_src=template)
* [Swashbuckle.AspNetCore (5.6.3)](https://www.nuget.org/packages/Swashbuckle.AspNetCore/5.6.3?_src=template)

### Installation

1. Clone the repository 
   ```
   git clone https://github.com/JustNindze/SebUzduotis.git
   ```
2. Build application for .NET Version 5.0.0
   ```
   dotnet build --configuration Release SebUzduotis.sln
   ```
   
### Usage

1. Just go to Release folder and run SebUzduotisApi.exe
   ```
   >SebUzduotisApi.exe
   ```
2. Make post request
   ```
   POST /AddAgreementToUser?personalId=string
   ```
   Body (new Agreement)
   ```
   {
      "amount": 0,
      "baseRateCode": "string",
      "margin": 0,
      "duration": 0
   } 
   ```
3. After request AddAgreementToUser method will write new Agreement to database and will return
   ```
   {
       "personalId": string,
       "firstName": string,
       "lastName": string,
       "lastAgreement": {
           "amount": int,
           "baseRateCode": string,
           "margin": decimal,
           "duration": int
       },
       "interestRates": {
           "lastInterestRate": decimal,
           "newInterestRate": decimal,
           "difference": decimal
       }
   }
   ```
