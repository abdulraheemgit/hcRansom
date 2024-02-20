# xRan
A ransomeware with anti reverse technique (without CC server). This was built as part of a CTF challenge. This will force the participants to go to the memory dump to find the flag.

The way it's achived is that encrypting the soure code and attaching it to a portable executable which will decrypt the source code, compile and run it in the runtime of the portable executor.

The ransomware code is in the 'xRan' folder. This is the actual code of the ransomware.
The portable executable (PE) builder is in the 'e' folder


Change the necessary parameters in the below file,
xRan/e/bin/Release/z.cs

Then run the below executable to encrypt the source code and attach it the PE. The final output will be in the 'xRan.exe'.
xRan/e/bin/Release/e.exe (shortcut)

Once the 'xRan.exe' created, give it to the participent to run.
When ran (xRan.exe) it will create a file called 'flag.txt.xRan' in folder 'xRan'. This is the challenge for the participants of the CTF
