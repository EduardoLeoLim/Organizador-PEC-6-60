﻿using Organizador_PEC_6_60.Domain.Usuario.Exceptions;
using Organizador_PEC_6_60.Domain.Usuario.Repository;
using Organizador_PEC_6_60.Domain.Usuario.ValueObjects;

namespace Organizador_PEC_6_60.Application.Usuario.Create
{
    public class UsuarioCreator
    {
        private UsuarioRepository _repository;

        public UsuarioCreator(UsuarioRepository repository)
        {
            _repository = repository;
        }

        public void Create(
            UsuarioUsername username,
            UsuarioPassword password,
            UsuarioNombre nombre,
            UsuarioApellidos apellidos
        )
        {
            //Validation
            if (!IsValid(nombre))
                throw new InvalidNombre();
            if (!IsValid(apellidos))
                throw new InvalidApellidos();
            if (!IsValid(username))
                throw new InvalidUsername();
            if (!IsValid(password))
                throw new InvalidPassword();

            _repository.Insert(new Domain.Usuario.Model.Usuario(username, password, nombre, apellidos));
        }

        private bool IsValid(object obj)
        {
            if (obj is UsuarioNombre)
            {
                string nombre = ((UsuarioNombre)obj).Value;

                if (string.IsNullOrEmpty(nombre))
                    return false;
                if (nombre.Trim().Length == 0)
                    return false;
                //Add more validations here

                return true;
            }

            if (obj is UsuarioApellidos)
            {
                string apellidos = ((UsuarioApellidos)obj).Value;

                if (string.IsNullOrEmpty(apellidos))
                    return false;
                if (apellidos.Trim().Length == 0)
                    return false;
                //Add more validations here

                return true;
            }

            if (obj is UsuarioUsername)
            {
                string username = ((UsuarioUsername)obj).Value;

                if (string.IsNullOrEmpty(username))
                    return false;
                if (username.Trim().Length == 0)
                    return false;
                //Add more validations here

                return true;
            }

            if (obj is UsuarioPassword)
            {
                string password = ((UsuarioPassword)obj).Value;

                if (string.IsNullOrEmpty(password))
                    return false;
                if (password.Trim().Length == 0)
                    return false;
                //Add more validations here

                return true;
            }

            return false;
        }
    }
}