using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;

namespace BLL.Services
{
    public class EvaluatedService : Service, IService<Evaluated, EvaluatedModel>
    {
        public EvaluatedService(Db db) : base(db)
        {
        }

        public IQueryable<EvaluatedModel> Query()
        {
            return _db.Evaluateds.OrderBy(g => g.Name).Select(g => new EvaluatedModel { Record = g });  //burada mutlaka Record = g yapmamız lazım aksi taktirde GenreModel boş olur.
        }

        public Service Create(Evaluated record)
        {
            if (_db.Evaluateds.Any(s => s.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Evaluated with the same name exists!");
            record.Name = record.Name?.Trim();
            _db.Evaluateds.Add(record);
            _db.SaveChanges();
            return Success("Evaluated is created successfully.");
        }

        public Service Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Service Update(Evaluated record)
        {
            throw new NotImplementedException();
        }
    }
}
