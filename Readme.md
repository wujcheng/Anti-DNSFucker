# Anti-DNSFucker <img src = "/Publish/Icons/Logo.png" height = 120 align="right"> 

A Toolkit for Defending against DNS Cache Pollution.

## Introduction
Thanks for the [Great Firewall], we have to suffer the DNS cache pollution, also referred to as DNS spoofing. You see,  the [Great Firewall] is a DNSFucker, sometimes. Yeah, the word "sometimes" is exactly right, because the [Great Firewall], most of its time, fucks something else.

One day, I found that [this web site] can give the real IP addresses. When the DNS is fucked, I ask for the IP addresses from [this web site], and I add them into my hosts file. However, the IP addresses of web sites are often changed. I have to modify my hosts file over and over, time after time, again and again. You see, the Anti-DNSFucker is designed to liberate myself from this ewwwwww thing. And I hope it can help you, too.

## How to Use Anti-DNSFucker

1. Install [Framework 4.0].
2. Run `.\Publish\Anti-DNSFucker.exe`.
3. The interface of Anti-DNSFucker is shown as following figure.<br>
   ![Main Interface]
4. You will see that the list is empty. You can click <img src = "/Publish/Icons/Add.ico" height = 15 title = "add button"> to add a new blank item in the list. Because, the software cannot resolve an empty domain name, so its backcolor is red, and the two following checkboxes are disbled.<br>
   ![Add an Empty Item]
5. You can input the domain name of a web site which is polluted by DNSFucker. For example, the IP address of [www.google.com] is not correct, you can input `www.google.com` into the textbox. Then the software resolve this domain name automatically. The backcolor is changed to green, and the two checkboxes are enabled.<br>
   ![Input Google Domain Name]
6. If you want to add two kinds of IP address, IPv4 and IPv6, into the hosts file, you need to select these two checkboxes.<br>
   ![IPv4 And IPv6]
7. You can click <img src = "/Publish/Icons/Run.ico" height = 15 title = "run button"> to overwrite the hosts file. Then you can counterfuck the [Great Firewall].<br>
   ![Done]
8. The program first tries to overwrite the hosts file, if the result is permission denied, it will ask for the administrator rights. Please click `yes` button.<br>
   ![User Account Control]
9. One day, you want to obtain IP address of [www.google.com] from DNS, you can unselect the enable checkbox which is in front of the textbox. Then click <img src = "/Publish/Icons/Run.ico" height = 15 title = "run button"> to recover the hosts file.<br>
   ![Disable Google Item]
10. Day after day, the list is bigger and bigger. The operation of this list is harder and harder.<br>
   ![Big List]
11. Don't worry. Anti-DNSFucker provides batch operations. You may notice that on the leftmost, there are a column of checkboxes. You can select a group of domain names, and use the buttons on the toolbar to modify their configurations.
12. If you select a group of domain names, the disabled buttons on the toolbar are enabled.
  * You can click <img src = "/Publish/Icons/Remove.ico" height = 15 title = "remove button"> to remove the selected items.
  * You can click <img src = "/Publish/Icons/Enable.ico" height = 15 title = "enable button"> to enable the selected items.
  * You can click <img src = "/Publish/Icons/Disable.ico" height = 15 title = "disable button"> to disable the selected items.
  * You can click <img src = "/Publish/Icons/IPV4Enable.ico" height = 15 title = "get IPv4 button"> to select their `Get IPv4` checkboxes.
  * You can click <img src = "/Publish/Icons/IPV4Disable.ico" height = 15 title = "get IPv4 button"> to unselect their `Get IPv4` checkboxes.
  * You can click <img src = "/Publish/Icons/IPV6Enable.ico" height = 15 title = "get IPv6 button"> to select their `Get IPv6` checkboxes.
  * You can click <img src = "/Publish/Icons/IPV6Disable.ico" height = 15 title = "get IPv6 button"> to unselect their `Get IPv6` checkboxes.
13. If you want to re-resolve all domain names in the list, you can click <img src = "/Publish/Icons/Refresh.ico" height = 15 title = "refresh button">.<br>
   ![Refresh]
14. If you want to export the list and send it to one of your friends, you can click <img src = "/Publish/Icons/SaveAs.ico" height = 15 title = "save configuration button"> to save the list to a `.cfg` file.<br>
   ![Save]<br>
   Your friend receives this `.cfg` file and clicks <img src = "/Publish/Icons/Open.ico" height = 15 title = "open configuration button"> and selects this `.cfg` file into his/her list.<br>
   ![Open]
15. Click <img src = "/Publish/Icons/About.ico" height = 15 title = "about button"> to show the about dialog.
16. Click <img src = "/Publish/Icons/Quit.ico" height = 15 title = "quit button"> to exit this program.
17. Drag the title to move the form.<br>
    ![Drag Title to Move]
18. One more thing, every button on the toolbar has a shortcut key.
  * <img src = "/Publish/Icons/Add.ico" height = 15 title = "add button"> `ctrl` + `N`,
  * <img src = "/Publish/Icons/Remove.ico" height = 15 title = "remove button"> `ctrl` + `X`,
  * <img src = "/Publish/Icons/Enable.ico" height = 15 title = "enable button"> `ctrl` + `E`,
  * <img src = "/Publish/Icons/Disable.ico" height = 15 title = "disable button"> `ctrl` + `D`,
  * <img src = "/Publish/Icons/IPV4Enable.ico" height = 15 title = "get IPv4 button"> `ctrl` + `4`,
  * <img src = "/Publish/Icons/IPV4Disable.ico" height = 15 title = "get IPv4 button"> `ctrl` + `shift` + `4`,
  * <img src = "/Publish/Icons/IPV6Enable.ico" height = 15 title = "get IPv6 button"> `ctrl` + `6`,
  * <img src = "/Publish/Icons/IPV6Disable.ico" height = 15 title = "get IPv6 button"> `ctrl` + `shift` + `6`,
  * <img src = "/Publish/Icons/Open.ico" height = 15 title = "open configuration button"> `ctrl` + `O`,
  * <img src = "/Publish/Icons/SaveAs.ico" height = 15 title = "save configuration button"> `ctrl` + `S`,
  * <img src = "/Publish/Icons/Run.ico" height = 15 title = "run button"> `F5`,
  * <img src = "/Publish/Icons/Refresh.ico" height = 15 title = "refresh button"> `ctrl` + `R`,
  * <img src = "/Publish/Icons/About.ico" height = 15 title = "about button"> `ctrl` + `H`,
  * <img src = "/Publish/Icons/Quit.ico" height = 15 title = "quit button"> `atl` + `Q`.
19. Cannot remember? If you hover your mouse on these buttons, the tooltips will appear.<br>
   ![Tooltip]

## Supported Operating Systems

- Windows XP,
- Windows Vista,
- Windows 7,
- Windows 8, and
- Windows 10.

## Built with

- Visual Basic .NET, and
- Visual Studio Community 2015.

## ToDo

- Add the function auto updating.

## License
This project is licensed under the MIT License. See the [License.md] file for details.

## Acknowledgments
I would like to thank the anonymous referees for their helpful comments and suggestions.

[this web site]:http://geoip.neu.edu.cn/
[Great Firewall]:https://en.wikipedia.org/wiki/Great_Firewall
[Framework 4.0]:https://www.microsoft.com/en-us/download/details.aspx?id=17718
[www.google.com]:www.google.com
[Main Interface]:Screenshots/MainInterface.png "Main Interface"
[Add an Empty Item]:/Screenshots/AddAnEmptyItem.png "Add an Empty Item"
[Input Google Domain Name]:/Screenshots/InputGoogleDomainName.png "Input Google Domain Name"
[IPv4 And IPv6]:/Screenshots/IPv4AndIPv6.png "IPv4 And IPv6"
[Disable Google Item]:/Screenshots/DisableGoogleItem.png "Disable Google Item"
[Big List]:/Screenshots/BigList.png "Big List"
[Tooltip]:/Screenshots/Tooltip.png "Tooltip"
[Done]:/Screenshots/Done.png "Done"
[User Account Control]:/Screenshots/UserAccountControl.png "User Account Control"
[Drag Title to Move]:/Screenshots/DragTitleToMove.png "Drag Title to Move"
[Refresh]:/Screenshots/Refresh.png "Refresh"
[Open]:/Screenshots/Open.png "Open"
[Save]:/Screenshots/Save.png "Save"
[License.md]:/License.md