﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Domain
{
    public class Korisnik : OpstiEntitet
    {
        public string Ime { get; set; }

        public string Prezime { get; set; }
    }
}