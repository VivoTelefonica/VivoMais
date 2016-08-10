using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Models
{
    public class VivoVisitaViewModel
    {
        public VivoVisitaPassagemAbertura Passagem { get; set; }

        public VivoVisitaCanalTerritorioAbertura CanalTerritorio { get; set; }
    }
}