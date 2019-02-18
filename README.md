# ERA File Creator

The ERA File Creator is a WPF program written with C#. It's design is to write asterisk delimited segments to fit the requirements of the EDI 835 ANSI spec.

The Program manages this by Taking End User input and storing them in in memory to be retrieved when the user wants to generate the file.
Once they are retrieved they are built using a stringbuilder, once built the string is written to a text file using StreamWriter.

### To Run
To run this program you can either install through the executable in the publish folder. This looks for updates within the folder on github and is deployed using clickonce.
Alternatively you can open the solution in Visual Studio and select EFC.View as the startup project

### Dependencies
* program uses Newtonsoft.Json

### Instructions
Patient - The entity you are associating the encounter to

Patient Encounters - Each service has at least one service description. The service description can have 0 to many adjustments and/or 0 to many Additional Service Descriptions

Adjustment - Ideally you want each adjustment to be unique, a Service Description can have more than one adjustment of different types.

Additional Service Description - Each Encounter can have more than one service Additional Service Descriptions keep a primary and additional service descriptions tied together. These will need to be added before you add the charge to the patient.

** V1.3
Contains the following Changes

Features
* Can Now Add Authorizations to primary and additional service descriptions
* Can Now Add Copay to additional service descriptions
* Can Now Dictate the number of units shown. 
		Note : Units cannot be 0 or less currently

Fixes
* Fixed bug that made additional service description adjustments always CO-45
* Fixed bug that calculated Patient responsibility only based on Copay
* Fixed bug that that always added a copay even of $0.00

** V1.2.2
Contains the following fixes
* Fixed bug that prevented Update Insurance Company Window when closed using the X

** V1.2.1
Contains the following fixes
* Removed publish name that prevents update error

** V1.2.0.0
Contains the following feature
* Stores Payer list in the settings
* Can now add payers to saved list 
* Replaced Payer name field with dropdown

**V1.0.1.0
**Contains the following feature
* Claim Status in the CLP segment is now changeable
* Claim Status will tell you its plain text reason when you hover

**V 1.0.0.23
**Contains  the following fixes
* Check Date and Check Number will no longer default back to today before creating the file
* Library update to .net 4.6.2
* Settings are now usable without needing to exit and restart
