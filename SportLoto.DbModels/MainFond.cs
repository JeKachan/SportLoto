using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportLoto.DbModels
{
    public class MainFond
    {
        public int Id { get; set; }

        public decimal IncrementSum { get; set; }

        public int DrawingId { get; set; }

        [ForeignKey("DrawingId")]
        public virtual Drawing Drawing { get; set; }

        public DateTime DateCreate { get; set; }

    }
}
