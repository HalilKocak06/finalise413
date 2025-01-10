using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;

namespace BLL.Models
{
    public class EvaluatedModel
    {
        public Evaluated Record { get; set; }


        public string Name => Record.Name;


        public string Surname => Record.Surname;

        public string NameAndSurname => Record.Name + " " +  Record.Surname;



    }
}
