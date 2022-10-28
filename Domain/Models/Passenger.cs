
using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Passenger_API.Models
{
    [BsonIgnoreExtraElements]
    public class Passenger
    {
        [Required]
        [MaxLength(14)]
        public string CPF { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public char Gender { get; set; }
        [MaxLength(14)]
        public string Phone { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime DtBirth { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime DtRegister { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public Address Address { get; set; }

    }
}
