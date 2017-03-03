# FaxHelper
Small commandline tool to use the Windows Fax (local or remote) to send a fax

    Arguments:
    -s | -server     The IP or Hostname of the remote faxserver (or local machine)
    -d | -document   The document, that should be transmitted. (.txt or .tif)
    -r | -receiver   The number to dial of the fax of the receiver
    -rn | -receivername   The Displayname of the Receiver
    -sd | -senderdisplay  The Displayname of the Sender

Usage examples:

    FaxHelper.exe -s faxservername -d c:\mydocs\fax.tif -r 04121849465489 -rn "My Client" -sd "Good IT Company"
Sends a fax with the content of the document "C:\mydocs\fax.tif" to the number "04121849465489" and is displayed as "MyClient". The sender is displayed as "Good IT Company". The DNS-Name "faxservername" is used as the remote faxserver.

    FaxHelper.exe -s 192.168.0.222 -d fax.txt -r 04121849465489 -rn "My Client" -sd "Good IT Company"
Sends a fax with the content of fax.txt, that is in the same folder as the FaxHelper.exe (by default), to the number "04121849465489" and is displayed as "MyClient". The sender is displayed as "Good IT Company". The IP address "192.168.0.222" is used as the remote faxserver.

    FaxHelper.exe -s 127.0.0.1 -d fax.txt -r 04121849465489 -rn "My Client" -sd "Good IT Company"
Same example like before but is using the locally configured Fax