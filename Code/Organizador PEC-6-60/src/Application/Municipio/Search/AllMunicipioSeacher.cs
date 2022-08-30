﻿using System.Collections.Generic;
using Organizador_PEC_6_60.Domain.Municipio.Repository;

namespace Organizador_PEC_6_60.Application.Municipio.Search
{
    public class AllMunicipioSeacher
    {
        private readonly MunicipioRepository _repository;

        public AllMunicipioSeacher(MunicipioRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Domain.Municipio.Model.Municipio> SearchAllMunicipios(int idEntidadFederativa)
        {
            return _repository.SearchAll(idEntidadFederativa);
        }
    }
}