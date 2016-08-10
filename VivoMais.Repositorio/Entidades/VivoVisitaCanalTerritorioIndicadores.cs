using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VivoMais.Repositorio.Entidades
{
    public class VivoVisitaCanalTerritorioIndicadores
    {
        public int Id { get; set; }
        public string ResultadosIndicadoresAvaliarPerformanceCorrenteSimNao { get; set; }
        public string ResultadosIndicadoresAvaliarPerformanceCorrenteObs { get; set; }
        public string ResultadosIndicadoresIndicadoresTempoSimNao { get; set; }
        public string ResultadosIndicadoresIndicadoresTempoObs { get; set; }
        public string ResultadosIndicadoresAcompanharAlgunsAtendimentosSimNao { get; set; }
        public string ResultadosIndicadoresAcompanharAlgunsAtendimentosObs { get; set; }
        public string ResultadosIndicadoresRentabilidadeSimNao { get; set; }
        public string ResultadosIndicadoresRentabilidadeObs { get; set; }
    }
}