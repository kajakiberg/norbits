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

## Besvarelse

I denne besvarelsen benyttes SQLite som databasemotor. Applikasjonen er utvidet med følgende: 

* Vise alle bilar tilknyttet verkstedet
* Vise alle data fra biltabellen på oppsøkt bil
* Legge til ny bil og slette bil
* Redigere data på en bil

Søkefeltet på index-siden lar brukeren søke opp en bil basert på bilskilt, og viser bilens detaljer. Søket viser bare biler som tilhører verkstedet man er logget inn hos. På CarsList-siden (‘Cars’ i navigasjonsmenyen) finner man en oversikt over alle bilene til det gjeldende verkstedet. Her ser man bilens detaljer, og kan redigere eller slette bilen. I tillegg kan du legge til nye biler på denne siden. Ved opprettelse av ny bil valideres registreringsnummeret for å sikre at bilen ikke eksiterer i databasen fra før, uavhengig av selskap. I tillegg må registreringsnummeret oppfylle kravene til format. 

# Videre forbedringer

Ved opprettelse av ny bil, vises en feilmelding hvis registreringsnummeret er opptatt eller ugyldig. For øvrige felt kan det være nyttig å legge til en validering som tillater tomme felt for noen av attributtene, som beskrivelse, modell og merke, så lenge de ikke er obligatoriske for registreringen. 

Søk og validering under opprettelse av bil er case-insensitivt. Alle reg.nr. burde nok uansett lagres med store bokstaver, da dette gir et mer ryddig visuelt inntrykk. 

For å forbedre brukeropplevelsen og effektiviteten, bør bilmerker og eventuelt modeller legges inn som forhåndsdefinerte lister. Det vil gjøre det mulig å bruke en nedtrekksliste e.l. ved opprettelse av nye biler. Det vil også være mulig å implementere merkesøk, og å filtrere søk og billister etter merke. 

Før applikasjonen kan anvendes må det implementeres en innloggingsmekanisme med brukerautentisering. Passord og eventuelle følsomme data må beskyttet med hashing og salt.

Det kan være fornuftig å introdusere rollebasert tilgangskontroll, som styrer hvilke handlinger brukerne kan utføre (f.eks. legge til, redigere eller slette biler, eller endre verkstedets innstillinger). Her må databasen modifiseres for å støtte lagring av brukerroller og tilhørende tilganger.

Dersom applikasjonen vokser i antall brukere og biler, så burde SQLite erstattes med en mer skalerbar databaseløsning. I tillegg burde det implementeres funksjoner som paginasjon for å håndtere store mengder bildata og gjøre det lettere for brukeren å navigere i listen med biler. 

Ytterligere funksjonalitet som vedlikeholdshistorikk, avtalebok og kundedashbord er eksempler på aktuelle utvidelser av applikasjonen. 

# Sikkerhet
Noe beskyttelse er implementert mot sikkerhetstrusler som SQL-injeksjon, XXS og CSRF.

I søkefunksjonen brukes registreringsnummeret til å søke etter biler i databasen. Et ubeskyttet inndatafelt kan føre til at en angriper kan manipulere SQL-spørringen for å få tilgang til, endre eller slette data som de ikke har tillatelse til å gjøre. Et innloggingsskjema vil også være en typisk angrepsoverflate for SQL-injeksjon. For å beskytte mot dette brukes parameteriserte spørringer, som gjør at brukerinput behandles som data og ikke som en del av SQL-kommandoen. I tillegg vil ytterligere validering og sanitisering bidra til å beskytte mot SQL-injeksjon ved å fjerne uønskede tegn eller koder. 

XSS er også en potensiell trussel i denne applikasjonen, og kan oppstå hvis brukerens input (som reg.nr. eller bilbeskrivelser) blir direkte vist uten riktig sanitisering. Hvis en angriper skriver input til bilens beskrivelse som inneholder JavaScript-kode, kan det kjøres i nettleseren til den som ser på bilens detaljer, og angriperen kan utføre handlinger på vegne av brukeren eller skade applikasjonen. Derfor må all brukerinput renses, for eksempel ved å bruke HTML-escaping og biblioteker som AntiXSS.

Anti-forgery tokens er brukt i alle sensitive POST-forespørsler for å beskytte applikasjonen mot CSRF-angrep, ved å sørge for at forespørsler kommer fra den autentiserte brukeren.

Logging og overvåking mangler fortsatt. Dette burde implementeres for å fange opp feil, hendelser og mistenkelig aktivitet.  