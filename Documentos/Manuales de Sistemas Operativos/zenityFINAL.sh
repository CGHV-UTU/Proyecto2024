opcion=$(zenity --list --title="Modificaciones de servidor" --ok-label="Aceptar" --cancel-label="Cancelar" --text="Selecciona una operación" --radiolist --column="" --column="Opción"  1 "Alta de usuario" 2 "Baja de usuario" 3 "Modificación de usuario" 4 "Listado de usuarios" 5 "Alta de grupo" 6 "Baja de grupo" 7 "Modificación de grupo" 8 "Listado de grupo")
if [ "$opcion" = "Alta de usuario" ];
then
	nombre=$(zenity --entry --title="Alta de usuario" --ok-label="Aceptar" --cancel-label="Cancelar" --text="¿Qué nombre desea ponerle al usuario?")
	if [ -n "$nombre" ];
	then
		contrasenia=$(zenity --entry --title="Alta de usuario" --ok-label="Aceptar" --cancel-label="Cancelar" --text="¿Cuál será su contraseña?")
		if [ -n "$contrasenia" ]
		then
			sudo useradd $nombre
			sudo echo -e "$contrasenia\n$contrasenia" | sudo passwd "$nombre"
			if [ $? -eq 0 ];
			then
				zenity --info --title="Alta de usuario" --text="Alta exitosa"
				echo "$nombre">>usuarios.txt
			else
				zenity --error --title="Alta de usuario" --text="Alta incorrecta"
			fi
		fi
	fi
fi

if [ "$opcion" = "Baja de usuario" ];
then
	nombre=$(zenity --entry --title="Baja de usuario" --ok-label="Aceptar" --cancel-label="Cancelar" --text="¿A qué usuario desea eliminar?")
	if [ $(id -u "$nombre") -gt 0 ];
	then
		zenity --question --title="Baja de usuario" --text="¿Seguro que deseas eliminar al usuario $nombre?" --ok-label="Si" --cancel-label="No"
		if [ $? -eq 0 ];
		then
			sudo userdel -r "$nombre"
			if [ $? -eq 0 ];
			then
				sudo groupdel "$nombre"
				zenity --info --title="Baja de usuario" --text="Baja correcta"
				grep -v "^$nombre$" usuarios.txt>u.txt
				cat u.txt>usuarios.txt
			else
				zenity --error --title="Baja de usuario" --text="Baja incorrecta"
			fi
		fi
	fi
fi

if [ "$opcion" = "Modificación de usuario" ];
then
	nombre=$(zenity --entry --title="Modificacion de usuario" --text="Ingrese el nombre del usuario a modificar")
	if [ $(id -u "$nombre") -gt 0 ];
	then
		opcion2=$(zenity --list --title="Modificacion de usuario" --text="¿Que quiere modificar?" --radiolist --column="" --column="Opciones" 1 "Nombre de usuario" 2 "Contraseña")
		if [ "$opcion2" = "Nombre de usuario" ];
		then
			nombreNuevo=$(zenity --entry --title="Modificacion de usuario" --text="Ingrese el nuevo nombre para el usuario")
			if [ -n "$nombreNuevo" ];
			then
				sudo usermod -l "$nombreNuevo" "$nombre"
				sudo usermod -d /home/"$nombreNuevo" -m "$nombreNuevo"
				zenity --info --title="Modificacion de usuario" --text="Nombre modificado con éxito"
				grep -v "^$nombre$" usuarios.txt>u.txt
				echo "$nombreNuevo">>u.txt
				cat u.txt>usuarios.txt
			fi
		fi
		if [ "$opcion2" = "Contraseña" ];
		then
			nuevaContraseña=$(zenity --entry --title="Modificacion de usuario" --text="Ingrese la nueva contraseña para el usuario")
			if [ -n "$nuevaContraseña" ];
			then
				sudo echo -e "$nuevaContraseña\n$nuevaContrasenia" | sudo passwd "$nombre"
				zenity --info --title="Modificacion de usuario" --text="Contraseña modificada con éxito"
			fi
		fi
	else
		zenity --error --title="Modificacion de usuario" --text="El usuario no existe"
	fi
fi

if [ "$opcion" = "Listado de usuarios" ];
then
	zenity --text-info --title="Lista de usuarios" --filename="usuarios.txt"
fi

if [ "$opcion" = "Alta de grupo" ];
then
	touch grupos.txt>2
	nombre=$(zenity --entry --title="Alta de grupo" --ok-label="Aceptar" --cancel-label="Cancelar" --text="Ingrese un nombre para el grupo")
	if [ -n "$nombre" ] && ! getent group "$nombre";
	then
		sudo groupadd "$nombre"
		echo $nombre>>grupos.txt
		zenity --info --title="Alta de grupo" --text="Grupo creado correctamente"
	else
		zenity --error --title="Alta de grupo" --text="Nombre repetido o erroneo, grupo no creado"
	fi
fi

if [ "$opcion" = "Baja de grupo" ];
then
	nombre=$(zenity --entry --title="Baja de grupo" --ok-label="Aceptar" --cancel-label="Cancelar" --text="Ingrese un nombre del grupo a eliminar")
	if [ -n "$nombre" ] && getent group "$nombre";
	then
		zenity --question --title="Baja de grupo" --text="¿Seguro de que desea eliminar el grupo?" --ok-label="Si" --cancel-label="No"
		if [ $? -eq 0 ];
		then
			sudo groupdel "$nombre"
			grep -v "^$nombre$" grupos.txt>g.txt
			cat g.txt>grupos.txt
			zenity --info --title="Baja de grupo" --text="Grupo eliminado correctamente"
		fi
	else
		zenity --error --title="Baja de grupo" --text="Nombre de grupo incorrecto"
	fi
fi

if [ "$opcion" = "Modificación de grupo" ];
then
	nombre=$(zenity --entry --title="Modificación de grupo" --text="Ingrese el nombre del grupo")
	if [ -n "$nombre" ] && getent group "$nombre";
	then
		opcion2=$(zenity --list --title="Modificación de grupo" --text="Elija una operación" --radiolist --column="" --column="Opciones" 1 "Cambiar nombre" 2 "Añadir integrante" 3 "Eliminar integrante")
		if [ "$opcion2" = "Cambiar nombre" ];
		then
			nuevoNombre=$(zenity --entry --title="Cambiar nombre del grupo" --text="Ingrese el nuevo nombre del grupo")
			if [ -n "$nuevoNombre" ] && ! getent group "$nuevoNombre" && ! [ "$nombre" = "$nuevoNombre" ];
			then
				zenity --info --title="Cambiar nombre del grupo" --text="Cambio de nombre exitoso"
				grep -v "^$nombre$" grupos.txt>g.txt
				echo "$nuevoNombre">>g.txt
				cat g.txt>grupos.txt
				sudo groupmod -n "$nuevoNombre" "$nombre"
			else
				zenity --error --title="Cambiar nombre del grupo" --text="Nuevo nombre incorrecto (ya existe, es igual al anterior o está mal el formato)"
			fi
		fi
		if [ "$opcion2" = "Añadir integrante" ];
		then
			nombreIntegrante=$(zenity --entry --title="Añadir integrante" --text="Ingrese el nombre del integrante a añadir")
			if [ -n "$nombreIntegrante" ] && [ $(id -u "$nombreIntegrante") -gt 0 ];
			then
				sudo usermod -aG "$nombre" "$nombreIntegrante"
				zenity --info --title="Añadir integrante" --text="Integrante $nombreIntegrante agregado al grupo $nombre"
			else
				zenity --error --title="Añadir integrante" --text="Nombre incorrecto"
			fi
		fi
		if [ "$opcion2" = "Eliminar integrante" ];
		then
			nombreIntegrante=$(zenity --entry --title="Eliminar integrante" --text="Ingrese el nombre del integrante a sacar del grupo")
                        if [ -n "$nombreIntegrante" ] && [ $(id -u "$nombreIntegrante") -gt 0 ] && id -nG "$nombreIntegrante" | grep -qw "$nombre";
                        then
                                sudo gpasswd -d "$nombreIntegrante" "$nombre"
				zenity --info --title="Eliminar integrante" --text="Integrante $nombreIntegrante fue eliminado del grupo $nombre"
                        else
                                zenity --error --title="Eliminar integrante" --text="Nombre incorrecto"
                        fi
		fi
	else
		zenity --error --title="Modificación de grupo" --text="Nombre incorrecto"
	fi
fi

if [ "$opcion" = "Listado de grupo" ];
then
	zenity --text-info --title="Lista de grupos" --filename="grupos.txt"
fi
