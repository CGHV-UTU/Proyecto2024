#!/bin/bash
#Esto es para que se interprete con Bash

set -eu -o pipefail # Es para que no siga si los comandos devuelven errores

#verifico permisos de sudo
if ! sudo -n true 2>/dev/null; then
	echo " Debes tener permisos de sudo para usar el script, usa su - "
	exit 1
fi

echo " Bienvenido al script de instalacion de Mysql,SSH y Apache web Server"
sleep 3
sudo dnf update -y
while read -r paquete ; do sudo dnf install $paquete -y ; done < <(cat << "FIN"
	mysql-server
	openssh-server
	httpd
FIN
)
echo Instalando prerequisitos
echo Espere a que se instale o utilize Ctrl+C para cancelar
sleep 5
echo Instalacion completa
sleep 5
echo Iniciar servicio ssh automaticamente al iniciar el sistema
sleep 5
sudo systemctl start sshd
sudo systemctl enable sshd
echo Iniciar servicio mysql automaticamente al iniciar el sistema
sleep 5
sudo systemctl start mysqld
sudo systemctl enable mysqld
echo Iniciar Apache Web server
sleep 5
sudo systemctl start httpd
sudo systemctl enable httpd
echo "Script finalizado puede verificar las descargas con sudo status sshd,mysqld,httpd"
sleep 10