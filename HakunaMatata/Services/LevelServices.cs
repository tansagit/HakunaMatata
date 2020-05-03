using HakunaMatata.Data;
using HakunaMatata.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HakunaMatata.Services
{
    public interface ILevelServices
    {
        IEnumerable<Level> GetListLevels();
        Level GetDetails(int id);
        void Create(Level level);
        void UpdateLevel(Level level);
        void DeleteLevel(int id);
        bool IsExist(int id);
    }
    public class LevelServices : ILevelServices
    {
        private readonly HakunaMatataContext _context;
        public LevelServices(HakunaMatataContext context)
        {
            _context = context;
        }


        public IEnumerable<Level> GetListLevels()
        {
            return _context.Level.ToList();
        }

        public Level GetDetails(int id)
        {
            return _context.Level.Find(id);
        }

        public void Create(Level level)
        {
            try
            {
                _context.Level.Add(level);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }

        }
        public void UpdateLevel(Level level)
        {
            try
            {
                var lv = _context.Level.Find(level.Id);
                if (lv != null)
                {
                    lv.LevelName = level.LevelName;
                    _context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteLevel(int id)
        {
            try
            {
                var a = _context.Level.Find(id);
                if (a != null)
                {
                    _context.Level.Remove(a);
                    _context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }



        public bool IsExist(int id)
        {
            return _context.Level.Any(f => f.Id == id);
        }
    }
}
