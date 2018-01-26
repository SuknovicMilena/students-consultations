﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsConsultations.Data.EF
{
    public class Datum
    {
        [Key]
        public DateTime DatumKonsultacija { get; set; }

        public ICollection<Konsultacije> Konsultacije { get; set; } = new HashSet<Konsultacije>();
    }
}
