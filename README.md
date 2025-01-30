# NorbitsChallenge

NorbitsChallenge er ein .NET 6 webapplikasjon for Visual Studio. 
NorbitsChallenge er tiltenkt å late tilsette på verkstadar slå opp informasjon om bilar knytt til sin verkstad. 
Autentisering er utelatt frå denne versjonen for forenkling. 

Me ønskjer følgjande utviding av applikasjonen:

* Vise alle bilar knytt til verkstaden i ei liste
* Vise alle data frå biltabellen på oppsøkt bil
* Leggje til ny bil og slette bil
* Redigere data på ein bil

Me ønskjer også at du peikar på openbare problem med applikasjonen og løyser desse. Me vil ikkje leggje særleg vekt på det grafiske utover fornuftig bruk av html/javascript/css, så ikkje legg for mykje tid i det.

Visual Studio Community kan lastast ned gratis hjå Microsoft: https://visualstudio.microsoft.com/vs/community/
Database i denne løysinga er localdb, du peikar sjølv til rett stad i appsettings.json

Last ned kildekoden til din maskin og last opp til eigen github-profil eller send oss ein link når du er ferdig. Ikkje send pull request til dette repoet, då vil koden din vere tilgjengeleg for eventuelle andre kandidatar.

Lukke til!

Edit: Dersom du ikkje får brukt localdb-filene kan du nytte følgjande sql til å opprette tabellane til oppgåva:

```
CREATE TABLE [dbo].[Car] (
    [LicensePlate] VARCHAR (10) NOT NULL,
    [Description]  VARCHAR (50) NULL,
    [Model]        VARCHAR (50) NULL,
    [Brand]        VARCHAR (50) NULL,
    [TireCount]    INT          NULL,
    [CompanyId]    INT          NULL
);

GO;

CREATE TABLE [dbo].[Settings] (
    [id]           INT          NOT NULL,
    [companyId]    INT          NULL,
    [setting]      VARCHAR (50) NULL,
    [settingValue] VARCHAR (50) NULL
);

GO;

```
Du kan då nytte eigen databasemotor i botnen, til dømes SQLite.

## English

NorbitsChallenge is a .NET 6 web application for Visual Studio.
NorbitsChallenge allows employees at car workshops look up information on cars attached to their workshop.
Authentication is omitted in this version for simplification purposes.

We are looking for the following additions to the application:
* Show all cars connected to the workshop presented in a list
* Show all information from the Car table when a car is looked up
* Add new cars and delete existing cars
* Edit data on an existing car

We also want you to point out any obvious problems in the application and solve these. There's no need to put too much effort in design besides reasonable use of html/javascript/css.

Visual Studio Community can be downloaded for free from Microsoft: https://visualstudio.microsoft.com/vs/community/
The database for this solution is localdb. You need to point to the right file location in appsettings.json

Download the code to you computer and upload to your personal github profile or send us a link when you are done. Do not submit a pull request to this repository, as this will make your code available to any other applicants.

Good luck!

Edit:

If you have problems using the localdb-files you can use the following scripts to create the tables needed:

```
CREATE TABLE [dbo].[Car] (
    [LicensePlate] VARCHAR (10) NOT NULL,
    [Description]  VARCHAR (50) NULL,
    [Model]        VARCHAR (50) NULL,
    [Brand]        VARCHAR (50) NULL,
    [TireCount]    INT          NULL,
    [CompanyId]    INT          NULL
);

GO;

CREATE TABLE [dbo].[Settings] (
    [id]           INT          NOT NULL,
    [companyId]    INT          NULL,
    [setting]      VARCHAR (50) NULL,
    [settingValue] VARCHAR (50) NULL
);

GO;

```
You can then use your preferred database engine, e.g. SQLite.

