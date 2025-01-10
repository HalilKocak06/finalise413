using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;
using Microsoft.AspNetCore.SignalR;

namespace BLL.Models
{
    public class EvaluationModel
    {
        public Evaluation Record { get; set; }

        public string Title => Record.Title;

        public decimal Score => Record.Score;

        public DateTime Date => Record.Date;

        public string Description => Record.Description; 

        public User User => Record.User;

        public string Evaluateds => string.Join(",", Record.EvaluatedEvaluations?.Select(ee => ee.Evaluated?.Name + " " + ee.Evaluated?.Surname)); // BURAYA ACİL BAK SINIFTAYIM.. NAME+Surname
        [DisplayName("Evaluated")]

        public List<int> EvaluatedIds
        {
            get => Record.EvaluatedEvaluations?.Select(bg => bg.EvaluatedId).ToList();
            set => Record.EvaluatedEvaluations = value.Select(v => new EvaluatedEvaluation() { EvaluatedId = v }).ToList();  //Bu ikisi edit için işimize lazım.
        }


    }
}
