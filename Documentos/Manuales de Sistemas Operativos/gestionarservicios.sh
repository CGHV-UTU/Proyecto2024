1#!/bin/bash

# Directorio para guardar logs
log_dir=$HOME/log
mkdir -p $log_dir  # Crea el directorio de logs si no existe

# Funciones

function Iniciar-MySQL() {
  sudo systemctl start mysqld >$log_dir/Iniciar-MySQL.out 2>$log_dir/Iniciar-MySQL.err
  cat $log_dir/Iniciar-MySQL.out
  cat $log_dir/Iniciar-MySQL.err
  zenity --text-info --filename=$log_dir/Iniciar-MySQL.out --width=800 --height=300
  zenity --text-info --filename=$log_dir/Iniciar-MySQL.err --width=800 --height=300
}

function Detener-MySQL() {
  sudo systemctl stop mysqld >$log_dir/Detener-MySQL.out 2>$log_dir/Detener-MySQL.err
  cat $log_dir/Detener-MySQL.out
  cat $log_dir/Detener-MySQL.err
  zenity --text-info --filename=$log_dir/Detener-MySQL.out --width=800 --height=300
  zenity --text-info --filename=$log_dir/Detener-MySQL.err --width=800 --height=300
}

function Estado-MySQL() {
  sudo systemctl status mysqld >$log_dir/Estado-MySQL.out 2>$log_dir/Estado-MySQL.err
  cat $log_dir/Estado-MySQL.out
  cat $log_dir/Estado-MySQL.err
  zenity --text-info --filename=$log_dir/Estado-MySQL.out --width=800 --height=300
  zenity --text-info --filename=$log_dir/Estado-MySQL.err --width=800 --height=300
}

function Iniciar-SSH() {
  sudo systemctl start sshd >$log_dir/Iniciar-SSH.out 2>$log_dir/Iniciar-SSH.err
  cat $log_dir/Iniciar-SSH.out
  cat $log_dir/Iniciar-SSH.err
  zenity --text-info --filename=$log_dir/Iniciar-SSH.out --width=800 --height=300
  zenity --text-info --filename=$log_dir/Iniciar-SSH.err --width=800 --height=300
}

function Detener-SSH() {
  sudo systemctl stop sshd >$log_dir/Detener-SSH.out 2>$log_dir/Detener-SSH.err
  cat $log_dir/Detener-SSH.out
  cat $log_dir/Detener-SSH.err
  zenity --text-info --filename=$log_dir/Detener-SSH.out --width=800 --height=300
  zenity --text-info --filename=$log_dir/Detener-SSH.err --width=800 --height=300
}

function Estado-SSH() {
  sudo systemctl status sshd >$log_dir/Estado-SSH.out 2>$log_dir/Estado-SSH.err
  cat $log_dir/Estado-SSH.out
  cat $log_dir/Estado-SSH.err
  zenity --text-info --filename=$log_dir/Estado-SSH.out --width=800 --height=300
  zenity --text-info --filename=$log_dir/Estado-SSH.err --width=800 --height=300
}

function Iniciar-Apache() {
  sudo systemctl start httpd >$log_dir/Iniciar-Apache.out 2>$log_dir/Iniciar-Apache.err
  cat $log_dir/Iniciar-Apache.out
  cat $log_dir/Iniciar-Apache.err
  zenity --text-info --filename=$log_dir/Iniciar-Apache.out --width=800 --height=300
  zenity --text-info --filename=$log_dir/Iniciar-Apache.err --width=800 --height=300
}

function Detener-Apache() {
  sudo systemctl stop httpd >$log_dir/Detener-Apache.out 2>$log_dir/Detener-Apache.err
  cat $log_dir/Detener-Apache.out
  cat $log_dir/Detener-Apache.err
  zenity --text-info --filename=$log_dir/Detener-Apache.out --width=800 --height=300
  zenity --text-info --filename=$log_dir/Detener-Apache.err --width=800 --height=300
}

function Estado-Apache() {
  sudo systemctl status httpd >$log_dir/Estado-Apache.out 2>$log_dir/Estado-Apache.err
  cat $log_dir/Estado-Apache.out
  cat $log_dir/Estado-Apache.err
  zenity --text-info --filename=$log_dir/Estado-Apache.out --width=800 --height=300
  zenity --text-info --filename=$log_dir/Estado-Apache.err --width=800 --height=300
}

# Programa principal

while true; do
    SELECCION=$(zenity --list --title="Gestión de Servicios" --column="Acción" \
        "Iniciar Apache" \
        "Detener Apache" \
        "Estado Apache" \
        "Iniciar SSH" \
        "Detener SSH" \
        "Estado SSH" \
        "Iniciar MySQL" \
        "Detener MySQL" \
        "Estado MySQL" \
        "Salir")

    if [ "$?" == 1 ]; then
        exit
    fi

    case $SELECCION in
        "Iniciar Apache")
            Iniciar-Apache
            ;;
        "Detener Apache")
            Detener-Apache
            ;;
        "Estado Apache")
            Estado-Apache
            ;;
        "Iniciar SSH")
            Iniciar-SSH
            ;;
        "Detener SSH")
            Detener-SSH
            ;;
        "Estado SSH")
            Estado-SSH
            ;;
        "Iniciar MySQL")
            Iniciar-MySQL
            ;;
        "Detener MySQL")
            Detener-MySQL
            ;;
        "Estado MySQL")
            Estado-MySQL
            ;;
        "Salir")
            break
            ;;
        *)
            zenity --error --text="Seleccione una opción válida o Salir."
            ;;
    esac
done
