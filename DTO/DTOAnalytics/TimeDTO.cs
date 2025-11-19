using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDatas.DTO.DTOAnalytics
{
    public class TimeDTO
    {
        public int TimePK { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Anio { get; set; }
        public int? Mes { get; set; }
        public string NombreMes { get; set; }
        public int? Dia { get; set; }
        public string NombreDiaSemana { get; set; }
        public int? DiaSemanaNumero { get; set; }
        public int? Trimestre { get; set; }
        public int? SemanaDelAnio { get; set; }
        public int? DiaDelAnio { get; set; }
        public bool? EsFinDeSemana { get; set; }
        public bool? EsFeriado { get; set; }
        public bool? EsAnioBisiesto { get; set; }
        public DateTime? ETLLoad { get; set; }
        public int? ETLIdExecution { get; set; }
    }

}
