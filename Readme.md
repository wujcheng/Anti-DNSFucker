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
4. You will see that the list is empty. You can click <img src = ".\Debug\Icons\Add.ico" height = 15 title = "add button"> to add a new blank item in the list. You can input the domain name of a web site which is polluted by DNSFucker.
5. 

[this web site]:http://geoip.neu.edu.cn/
[Great Firewall]:https://en.wikipedia.org/wiki/Great_Firewall
[Framework 4.0]:https://www.microsoft.com/en-us/download/details.aspx?id=17718