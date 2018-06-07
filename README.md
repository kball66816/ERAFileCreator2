# ERA File Creator

The ERA File Creator is a WPF program written with C#. It's design is to write asterisk delimited segments to fit the requirements of the EDI 835 ANSI spec.

The Program manages this by Taking End User input and storing them in in memory to be retrieved when the user wants to generate the file.
Once they are retrieved they are built using a stringbuilder, once built the string is written to a text file using StreamWriter.

### To Run
To run this program you can either install through the executable in the publish folder. This looks for updates within the folder on github and is deployed using clickonce.
Alternatively you can open the solution in Visual Studio and select EFC.View as the startup project

There are no 3rd party dependencies

### Instructions
Patient - The entity you are associating the encounter to

Patient Encounters - Each service has at least one service description. The service description can have 0 to many adjustments and/or 0 to many Additional Service Descriptions

Adjustment - Ideally you want each adjustment to be unique, a Service Description can have more than one adjustment of different types.

Additional Service Description - Each Encounter can have more than one service Additional Service Descriptions keep a primary and additional service descriptions tied together. These will need to be added before you add the charge to the patient.

**V 1.0.0.23**
**Contains  the following fixes
* Check Date and Check Number will no longer default back to today before creating the file
* Library update to .net 4.6.2
* Settings are now usable without needing to exit and restart