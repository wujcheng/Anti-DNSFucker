# Anti-DNSFucker

A Toolkit for Defending against DNS Cache Pollution.

## Introduction
Thanks for the [Great Firewall], we have to suffer the DNS cache pollution, also referred to as DNS spoofing. You see,  the [Great Firewall] is a DNSFucker, sometimes. Yeah, the word "sometimes" is exactly right, because the [Great Firewall], most of its time, fucks something else.

One day, I found that [this web site] can give the real IP addresses. When the DNS is fucked, I ask for the IP addresses from [this web site], and I add them into my hosts file. However, the IP addresses of web sites are often changed. I have to modify my hosts file over and over, time after time, again and again. You see, the Anti-DNSFucker is designed to liberate myself from this ewwwwww thing. And I hope it can help you, too.

## Usage

1. Install [Framework 4.0].
2. Run `.\Debug\Anti-DNSFucker.exe`.
3. The interface of Anti-DNSFucker is shown as following figure.<br>
   <img src = ".\Screenshots\MainInterface.png">
4. You will see that the list is empty. You can click <img src = ".\Debug\Icons\Add.ico" height = 15 title = "add button" style="margin-bottom:-3px"> to add a new blank item in the list. Because, the software cannot resolve an empty domain name, so its backcolor is red, and the two following checkboxes are disbled.<br>
   <img src = ".\Screenshots\AddAnEmptyItem.png">
5. You can input the domain name of a web site which is polluted by DNSFucker. For example, the IP address of [www.google.com] is not correct, you can input `www.google.com` into the textbox. Then the software resolve this domain name automatically. The backcolor is changed to green, and the two checkboxes are enabled.<br>
   <img src = ".\Screenshots\InputGoogleDomainName.png">
6. If you want to add two kinds of IP address, IPv4 and IPv6, into the hosts file, you need to select these two checkboxes.<br>
   <img src = ".\Screenshots\IPv4AndIPv6.png">
7. You can click <img src = ".\Debug\Icons\Run.ico" height = 12 title = "run button"> to overwrite the hosts file. Then you can counterfuck the [Great Firewall].
8. One day, you want to obtain IP address of [www.google.com] from DNS, you can unselect the enable checkbox which is in front of the textbox. Then click <img src = ".\Debug\Icons\Run.ico" height = 12 title = "run button"> to recover the hosts file.<br>
   <img src = ".\Screenshots\DisableGoogleItem.png">
9. Day after day, the list is bigger and bigger. The operation of this list is harder and harder.<br>
   <img src = ".\Screenshots\BigList.png">
10. Don't worry.

[this web site]:http://geoip.neu.edu.cn/
[Great Firewall]:https://en.wikipedia.org/wiki/Great_Firewall
[Framework 4.0]:https://www.microsoft.com/en-us/download/details.aspx?id=17718
[www.google.com]:www.google.com
