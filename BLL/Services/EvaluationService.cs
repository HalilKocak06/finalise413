using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class EvaluationService : Service, IService<Evaluation, EvaluationModel>
    {
        public EvaluationService(Db db) : base(db)
        {
        }

        public IQueryable<EvaluationModel> Query()
        {
            return _db.Evaluations
                .Include(b => b.EvaluatedEvaluations) //Many to many için eklenildi.
                .ThenInclude(bg => bg.Evaluated) //Many to many için ... BookModel'de eklediğimiz için.
                .OrderBy(b => b.Title)
                .Select(b => new EvaluationModel()
                {
                    Record = b
                });
        }

        public Service Create(Evaluation record)
        {
            if (_db.Evaluations.Any(p => p.Title.ToUpper() == record.Title.ToUpper().Trim()))
                return Error("Evaluation with the same name exists!");

            record.Title = record.Title?.Trim();
            _db.Evaluations.Add(record);
            _db.SaveChanges();
            return Success("Evaluation created successfully.");

        }

        public Service Delete(int id)
        {
            var entity = _db.Evaluations.Include(p => p.EvaluatedEvaluations).SingleOrDefault(p => p.Id == id);
            if (entity is null)
                return Error("Evaluations not found!");

            _db.EvaluatedEvaluations.RemoveRange(entity.EvaluatedEvaluations);

            _db.Evaluations.Remove(entity);
            _db.SaveChanges();
            return Success("Evaluation deleted successfully.");
        }

        

        public Service Update(Evaluation record)
        {
            if (_db.Evaluations.Any(p => p.Id != record.Id && p.Title.ToUpper() == record.Title.ToUpper().Trim()))
                return Error("Evaluation with the same name exists!");

            var entity = _db.Evaluations.Include(b => b.EvaluatedEvaluations).SingleOrDefault(b => b.Id == record.Id);

            if (entity == null)
                return Error("Evaluation not found!");

            _db.EvaluatedEvaluations.RemoveRange(entity.EvaluatedEvaluations);
            entity.Title = record.Title.Trim();
            entity.Score = record.Score;
            entity.Date = record.Date;
            entity.Description = record.Description;
            entity.EvaluatedEvaluations = record.EvaluatedEvaluations;
            entity.UserId = record.UserId;

            _db.Evaluations.Update(entity);
            _db.SaveChanges();

            return Success("Evaluation is updated successfully");


        }
    }
}
