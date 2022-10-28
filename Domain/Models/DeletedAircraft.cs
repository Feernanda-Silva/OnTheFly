using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace API_Aircraft.Models
{
    [BsonIgnoreExtraElements]
    public class DeletedAircraft
    {
        #region Property
        [Required(ErrorMessage = "Campo obrigatório!")]
        [StringLength(6, ErrorMessage = "Registro Aeronáutico Brasileiro inválido!")]
        public string RAB { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Formato de data inválido!")]
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime DtRegistry { get; set; }

        [Required(ErrorMessage = "Formato de data inválido!")]
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime? DtLastFlight { get; set; }

        //  [Required(ErrorMessage = "Este campo é obrigatório!"), StringLength(19, ErrorMessage = "CNPJ inválido!")]
        // public Company Company{ get; set; }
        //public string CNPJ { get; set; }
        #endregion

        #region Method
        //public AirCraft()
        //{
        //    DtRegistry = DateTime.Now;
        //}
        #endregion
    }
}
