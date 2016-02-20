using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportLoto.DbModels
{
    public class Drawing
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(60)]
        public string WinNo { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateDate { get; set; }

        public DateTime? EndDate { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        [Description("Розыгрыш уже окончен")]
        public bool IsCompleted { get; set; }
    }
}