﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StudentsConsultations.Domain
{
    public class Datum
    {
        [Key]
        public DateTime DatumKonsultacija { get; set; }

        [InverseProperty("Datum")]
        public List<Zadatak> Zadaci { get; set; }

        [InverseProperty("Datum")]
        public ICollection<Konsultacije> Konsultacije { get; set; } = new HashSet<Konsultacije>();
    }
}
