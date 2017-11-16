ERA File Creator

The ERA File Creator is a WPF program written with C#. It's design is to write asterisk delimited segments to fit the requirements of the EDI 835 ANSI spec.

The Program manages this by Taking End User input and storing them in repositories to be retrieved when the end user wants to generate the file.
Once they are retrieved they are built using a stringbuilder, once built the string is written to a text file using StreamWriter.

This project was started as a way for me to learn how to program and create applications using a subject matter I was familiar with but that would be a challenge to implement.
It can be used for training on how to read 835 files or to test that a system can accept an 835 file without exposing any PHI.

This project is open source through MIT licensure.
