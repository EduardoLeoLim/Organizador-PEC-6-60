﻿namespace Organizador_PEC_6_60.TipoEstadistica.Application.Search;

public interface TipoEstadisticaByIdSearcherService
{
    Domain.Model.TipoEstadistica SearchTipoEstadisticaById(int id);
}