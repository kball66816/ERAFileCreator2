ERA File Creator

The ERA File Creator is a WPF program written with C#. It's design is to write asterisk delimited segments to fit the requirements of the EDI 835 ANSI spec.

The Program manages this by Taking End User input and storing them in in memory to be retrieved when the user wants to generate the file.
Once they are retrieved they are built using a stringbuilder, once built the string is written to a text file using StreamWriter.

There are no 3rd party dependencies

Instructions
Patients - Each patient represents one encounter. Each encounter can have more than one charge but they share the same claim/bill Id

Charge - Each charge can have 0 or more adjustments. If you want to attach an adjustment to the charge you must first add an adjustment to the charge

Adjustment - Ideally you want each adjustment to be unique, a charge can have more than one adjustment of different types. Duplicate adjustments are not currently enforced.

Addon Charges - Each Charge within an encounter can theoretically have an addon charge. These will need to be added before you add the charge to the patient

Addon Adjustment - Just like a Charge adjustment, you want each adjustment to be unique, an addon charge can have more than one adjustment of different types. Duplicate adjustment checks are not currently enforced.
You will want to add the Addon Adjustment before you add the Addon Charge to the Charge.


