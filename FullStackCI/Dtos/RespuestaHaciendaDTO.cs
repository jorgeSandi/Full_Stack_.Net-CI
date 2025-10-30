namespace FullStackCI.Dtos
{
    public class RespuestaHaciendaDTO
    {
        public string Nombre { get; set; }
        public string TipoIdentificacion { get; set; }
        public RegimenDTO Regimen { get; set; }
        public SituacionDTO Situacion { get; set; }
        public List<ActividadesDTO> Actividades { get; set; }
    }
}