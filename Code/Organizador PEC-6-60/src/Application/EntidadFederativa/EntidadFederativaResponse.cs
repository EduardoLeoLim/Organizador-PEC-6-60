﻿namespace Organizador_PEC_6_60.Application.EntidadFederativa
{
    public class EntidadFederativaResponse
    {
        public int Id { get; }
        public int Clave { get; }
        public string Nombre { get; }

        private EntidadFederativaResponse(int id, int clave, string nombre)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
        }

        public static EntidadFederativaResponse FromAggregate(Domain.EntidadFederativa.Model.EntidadFederativa entidadFederativa)
        {
            return new EntidadFederativaResponse(entidadFederativa.Id, entidadFederativa.Clave.Value,
                entidadFederativa.Nombre.Value);
        }

        public override string ToString()
        {
            return $"{Clave:00} - {Nombre}";
        }
    }
}