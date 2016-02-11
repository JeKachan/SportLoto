//create table Drawings(
//ID int primary key identity(1,1),
//DrawingNo nvarchar(50),
//WinNo nvarchar(30) DEFAULT NULL
//)

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportLoto.DbModels
{
    public class Drawing
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(60)]
        public string WinNo { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}