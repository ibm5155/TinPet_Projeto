﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TinPet_Projeto.Models
{
    public interface IDataBase
    {
        SQLiteConnection GetConexaoLocal(string NomeDB);
    }
}
