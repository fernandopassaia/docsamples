#!/bin/sh -e
#ATIVANDO SYN COOKIES PROTECAO DO KERNEL
if [ -e /proc/sys/net/ipv4/tcp_syncookies ]
then
echo 0 > /proc/sys/net/ipv4/tcp_syncookies
fi
#printf "."
#CONFIGURACAO DE VARIÁVEIS
IF=pppoe
IP_range=192.168.1.0/24
LAN_iface=eth0
LOG="iplog -i $IF -w -d -l /var/log/iplogs"
#prinft "."
if [ -e /proc/sys/net/ipv4/all/rp_filter ]; then
for f in /proc/sys/net/ipv4/*/rp_filter; do
echo 1 >$f
done
fi
#printf "."
#Setando o kernel para dinamico IP masquerado
if [ -e /proc/sys/net/ipv4/ip_dynaddr ]
then
echo 1 > /proc/sys/net/ipv4/ip_dynaddr
fi
#printf "."
echo "#Limpando regras."
iptables -F
iptables -X
iptables -Z
iptables -F INPUT
iptables -F OUTPUT
iptables -F -t nat
iptables -X -t nat
iptables -F -t mangle
iptables -X -t mangle
echo "Limpando Regras................[ OK ]"
#Redirecionamento de portas
iptables -t nat -A PREROUTING -i eth0 -p tcp --dport 80 -j REDIRECT --to-port 7727
#
#Bloqueio das demais portas com LOG
iptables -A INPUT -p tcp -m multiport --dport 22,25,53,67,80,110,137,139,138,445,1512 -j LOG
iptables -A INPUT -p tcp -i $IF -m multiport --dport 22,25,53,67,80,110,137,139,138,445,1512 -j LOG
iptables -A INPUT -p tcp -i $IF -m multiport --dport 22,25,53,67,80,110,137,139,138,445,1512 -j REJECT
echo "Bloqueio de Portas com LOG.....[ OK ]"
#
#Politica Anti-Spoofings
iptables -A INPUT -j DROP -s 10.0.0.0/8 -i $IF
iptables -A INPUT -j DROP -s 127.0.0.0/8 -i $IF
iptables -A INPUT -j DROP -s 172.16.0.0/16 -i $IF
iptables -A INPUT -j DROP -s 192.168.1.0/24 -i $IF
#
#Spoofing com IP
iptables -N syn-flood
#iptables -A INPUT -i $WAN_iface -p tcp --syn -j syn-flood
iptables -A INPUT -i $IF -p tcp --syn -j syn-flood
iptables -A syn-flood -m limit --limit 1/s --limit-burst 4 -j RETURN
iptables -A syn-flood -j DROP
echo "Anti Spoofings..............[ OK ]"
#
#Bloqueando Multicast
iptables -A INPUT -s 224.0.0.0/8 -d 0/0 -j DROP
iptables -A INPUT -s 0/0 -d 224.0.0.0/8 -j DROP
echo "Anti MultCast...............[ OK ]"
#
#Bloqueando Syn-flood via modulo limit
iptables -A FORWARD -p tcp --syn -m limit --limit 1/s -j ACCEPT
echo "Anti Syn-Flood..............[ OK ]"
#
#Bloqueando Scanners Ocultos (Shealt Scan)
iptables -A FORWARD -p tcp --tcp-flags SYN,ACK,FIN,RST RST -m limit --limit 1/s -j ACCEPT
echo "Anti Shealt Scan............[ OK ]"
#
#Bloqueando Traceroute
#iptables -A INPUT -p udp -s 0/0 -i $IF --dport 33435:33525 -j DROP
echo "Anti Traceroute.............[ OK ]"
#
#Bloqueando pacotes suspeitos ou danificados
#iptables -A FORWARD -m unclean -j DROP
#echo "Anti Pacotes Suspeitos ou Danificados....[ OK ]"

#Bloqueando ICMP via tabela (inserido na tablea apenas quando o mudulo no kernel setando 1 for alterado)
iptables -A INPUT -p icmp --icmp-type echo-request -j DROP
echo "Bloqueio icmp...............[ OK ]"
exit 0;
