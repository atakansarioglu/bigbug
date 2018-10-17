# **BigBug: Remote printf Tool**
## Makes debugging of embedded projects easy yet no need for debugger!
<a href="https://github.com/atakansarioglu/bigbug/releases/latest" alt="Download Latest"><img src="https://img.shields.io/badge/download-latest-brightgreen.svg" /></a>

## **Introduction**
Sooner or later, every embedded engineer faces the insufficient debugging capabilities of microcontrollers. Unless connecting a debugger, or interrupting/stopping the execution, it is challenging to collect information about what is going on in the box. You can use serial port to print messages but communication line has limited speed, especially when the environment is noisy. The speed limit of the communication also limits how often and how long you can printf.

Imagine you are using RTOS and have many tasks running asynchronously. The best way of debugging on this environment is printing as many as possible messages on function entry, exit, measurement, event handlers, overflow of some timers etc. But honestly, every message slows down the system more. You can't send a sentence of 10 letters long only at entry of a method.

Fortunately, BigBug comes to help! You can tell many things only by sending 2-letter codes and BigBug translates into human language. 

## **How Can BigBug Help You?**
BigBug is a tool for displaying comprehensive debug messages on PC by sending only 2 letters (plus newline '\n') from microcontroller via serial port (uart). The messages are interpreted on the PC and decoded using a lookup table that is obtained directly from your project source code. The biggest benefit is keeping the mcu busy for the real tasks rather than spending time for printing debug messages.

Consider the example:
~~~~c
puts("HE");//@BB Hello World!
~~~~
Here you only send `HE\n` and BigBug prints `Hello World!` on PC screen.

## **Requirements**
### **Hardware**
BigBug can be used on-site, operating on a single UART TX line, supports any baud-rate. BigBug doesn't send any data to your device currently.

### **Supported Languages**
Virtually any language is supported. BigBug descriptors can be embedded into comments on your program, or can be supplied in a sepetrate text file. All will be scanned by BigBug and the descriptors will be parsed.
Descriptor format enables *any programming language* to send messaged to BigBug. Even when you use la language like python where the comment starts with `#` isntead of `//`, you can still write BigBug descriptor like `#...`.
# **Usage in Details**
## Where to Start
BigBug works by scanning the source code of your project folder. Let's explain the usage with very basis examples about what to write into your project to enable BigBug.

1. Download BigBug (and compile yourself if you want), no installation required.
2. Write BigBug descriptors similar to the given examples, refer to Examples and Descriptor Format sections. Descriptors can be embedded into your source code or list all descriptors into a dedicated txt file in your project folder.
3. Write your embedded project so that it sends serial port (UART) messages that correspond to your descriptors, refer to Serial Message Format section.
4. Show your project folder to BigBug by *opening project*.
5. Connect BigBug to your device over serial port, selecting a COMx port and proper Baud rate.
6. Filter the incoming messages,if you need.

## **Examples**
**1) No payloads** (string literal only).

The very basic string output function `puts()` can be used for the simplest message as *string literal*.
~~~~c++
puts("HE");//@BB[HE] Hello World!
~~~~
Here BigBug scans this source line and knows `HE` means `Hello World!`. Whenever it receives `HE\n` from the serial port, `Hello World!` will be displayed.

**2) Single payload** (replacement type).

This example shows another example that uses `printf()` and message has a *replacement payload*. The (0th) payload that is sent by the MCU during runtime, will be substituted to `{0}` and displayed on the screen as i.e. `New measurement x=-3.14` or `New measurement x=Low`. 
~~~~c++
printf("Me%i\n", 32);//@BB[Me] New measurement x={0}
~~~~
The line above sends `mX32\n` and `32` is evaluated as 0th payload. Finally, BigBug displays `New measurement x=32`

**3) Multiple payloads** (replacement, replacement, explicit enumerated, implicit enumerated).

In this example there are 4 payloads that are appended to the message sent by MCU.
~~~~c++
//@BB[TM] Time is {0}:{1} {2:(0),(1)AM,(2)PM} and today is {3:Sun,Mon,Tue,Wed,Thu,Fri,Sat}
~~~~
The example message descriptor tells BigBug to look for UART messages starting with "TM" letters, then expects 4 payloads, `{0}` to `{3}`. First 2 payloads (`{0}` and `{1}`) are replacement type and interpreted the same way as 2nd example. Next payload `{2}` is a enumeration type and gets the enumeration index from MCU and replaces it with the corresponding text from the description. 

Here `{2:(0),(1)AM,(2)PM}` tells BigBug that this block will be replaced by:

* `""` if 2nd payload value is equal to 0,

* `"AM"` if it is 1,

* `"PM"` if it is 2.

3rd payload `{3}` is also enumerated but it doesn't provide enumeration index numbers. In this case, BigBug will enumerate the items automatically starting from 0.
This means `{3:Sun,Mon,Tue,Wed,Thu,Fri,Sat}` will be replaced by `"Sun"` if received corresponding payload is 0, `"Mon"` if 1 and so on.


An example code line that sends message of this type is given below.
~~~~c++
cout << "TM" << "12" << " " << "34" << " " << "1" << " " << "5" << endl;
~~~~
The message above will be displayed as `Time is 12:34 AM and today is Fri` on BigBug.


#### Check Example/ folder for an example project.

# **Detailed Reference**

## **Supported Payload Types**
1. String Literal
2. Replacement payload (numeric or string)
3. Enumerated payload (Explicit or Implicit)

## **Descriptor Format**
Parts of the descriptor is shown below. (1) is BigBug descriptor tag, (2) is message shortcode, (3) is format string.

~~~~bash
    (1)     (3)
    vvv     vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
    @BB[TM] Time is {0}:{1} {2:(0),(1)AM,(2)PM} and today is {3:Sun,Mon,Tue,Wed,Thu,Fri,Sat}
        ^^
        (2)
~~~~

And below are shown the payload fields. (4) is string literal, (5) is replacement payload, (6) is explicitly enumerated payload and (7) is implicitly enumerated payload.

~~~~bash
            (4)                                              (7)
            vvvvvvvv                                         vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
    @BB[TM] Time is {0}:{1} {2:(0),(1)AM,(2)PM} and today is {3:Sun,Mon,Tue,Wed,Thu,Fri,Sat}
                    ^^^     ^^^^^^^^^^^^^^^^^^^
                    (5)                     (6)
~~~~

* You don't need to write the same descriptor more than once in a project. You can describe once and use th described format in as many as needed parts of your project.
* Scanned file types are: txt as mx ada ads adb asm asp au3 bc cln bash sh bsh csh bash_profile bashrc profile bat cmd nt c lex ml mli sml thy cmake cbl cbd cdb cdc cob litcoffee h hpp hxx cpp cxx cc ino cs css d diff patch f for f90 f95 f2k f23 f77 hs lhs las html htm shtml shtm xhtml xht hta ini inf reg url wer iss hex java js jsm jsx ts tsx json jsp kix lsp lisp lua mak m nfo nsi nsh mm pas pp p inc lpr pl pm plx php php3 php4 php5 phps phpt phtml ps ps1 psm1 properties py pyw r s splus rc rb rbw scm smd ss st sql mot srec swift tcl tek tex vb vbs v sv vh svh vhd vhdl xml xaml xsl xslt xsd xul kml svg mxml xsml wsdl xlf xliff xbl sxbl sitemap gml gpx plist yml yaml.
* You can override the file types by defining `FileExtensions` under `General` section in settings.ini file (in BigBug.exe working directory). List format is `c cpp hpp` etc.

## **Serial Message Format**
Below is an example serial message sent by microcontroller. (1) is shortcode of the message, (2) payloads and (3) is line terminator. Terminator can be either `\n` or `\r\n`.

~~~~bash
        (1)        (3)    
        vv         vv     
        TM12 34 1 5\n     
          ^^^^^^^^^       
                (2)       
~~~~

And payloads are shown below with respective index numbers. Seperator character is space and values (payloads) can be empty (in this case two consequtive spaces).
~~~~bash
          {0}  {2}        
          vv    v         
        TM12 34 1 5\n     
             ^^   ^       
            {1}  {3}      
~~~~

 * There is no limit for number of payloads but hundreds of them will impact performance for sure.
 * Space and backslash characters in values should be escaped with backslash (like `"\ "` or `"\\"`).

## **Message Display**
BigBug is tested under 1MBaud continuous serial data rate and maximum number of displayed (remembered) message lines is limited to 100k by default. User can override that limit by defining `MaxDataRows` under `General` section of settings.ini file (in BigBug.exe working directory).
Filtering is possible for messages. As an example entering `igb` into filter box will match message lines containing text `BigBug` and `|` character can be used for or'ing the filter confitions e.g. `filter1|filter2`.

# **Screenshots**
1) Open a project. BigBug scans every file in the selected folder and every line of them to learn BigBug descriptors. After succesfully opening, number of descriptors found is shown next to the project name.

<img src="https://github.com/atakansarioglu/bigbug/raw/master/Example/ScreenShots/BigBug-ScreenShot%20(1).png" width="598" alt="Open a project">

2) Connect to serial port (comport)

<img src="https://github.com/atakansarioglu/bigbug/raw/master/Example/ScreenShots/BigBug-ScreenShot%20(3).png" width="598" alt="Connect to serial port comport">

3) Messages are coming from UART will be displayed on the screen.

<img src="https://github.com/atakansarioglu/bigbug/raw/master/Example/ScreenShots/BigBug-ScreenShot%20(4).png" width="598" alt="Messages are coming">

4) Filter messages to see specific events of interest. Filter is case-insensitive and wildcard by-default. Different filters can be combined by or operator `|` character.

<img src="https://github.com/atakansarioglu/bigbug/raw/master/Example/ScreenShots/BigBug-ScreenShot%20(5).png" width="598" alt="Filter messages">

5) Save the received messages to file. To save, port should be closed.

<img src="https://github.com/atakansarioglu/bigbug/raw/master/Example/ScreenShots/BigBug-ScreenShot%20(8).png" width="598" alt="Save the received messages to file">

# **Notes**

## **For simulation/test on PC**
Recommended tool: https://sourceforge.net/projects/com0com/

