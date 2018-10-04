using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship.Data.DomainClasses;
using System.Data.Entity;

namespace Internship.Data.Repositories
{
   public class StageopdrachtenDbRepository : IStageopdrachtenRepository
    {

        private ApplicationDbContext _context;

        public StageopdrachtenDbRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Stageopdracht Get(int id)
        {
            return _context.Stageopdrachten.FirstOrDefault(s => s.Id == id);
        }

        public Stageopdracht GetWithFavorites(int id)
        {
            return _context.Stageopdrachten.Include(s => s.StudentFavorieten).FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Stageopdracht> GetAll()
        {
            return _context.Stageopdrachten.ToList();
        }

        public Stageopdracht Post(Stageopdracht stageOpdracht)
        {
            _context.Stageopdrachten.Add(stageOpdracht);
            _context.SaveChanges();
            return stageOpdracht;
        }

        public void Update(Stageopdracht stageOpdracht)
        {
            /*
            var stageopdracht = _context.Stageopdrachten.FirstOrDefault(s => s.Id == stageOpdracht.Id);
            stageopdracht.Status = stageOpdracht.Status;
            stageopdracht.Locatie = stageOpdracht.Locatie;
            stageopdracht.Omschrijving = stageOpdracht.Omschrijving;
            stageopdracht.Omgeving = stageOpdracht.Omgeving;
            stageopdracht.Randvoorwaarden = stageOpdracht.Randvoorwaarden;
            stageopdracht.Onderzoeksthema = stageOpdracht.Onderzoeksthema;
            stageopdracht.InfoOverThema = stageOpdracht.InfoOverThema;
            stageopdracht.BeschrijvingTechnischeOmgeving = stageOpdracht.BeschrijvingTechnischeOmgeving;
            stageopdracht.Verwachtingen = stageOpdracht.Verwachtingen;
            stageopdracht.AantalITWerknemers = stageOpdracht.AantalITWerknemers;
            stageopdracht.AantalITBegeleiders = stageOpdracht.AantalITBegeleiders;
            stageopdracht.AantalWerknemers = stageOpdracht.AantalWerknemers;
            stageopdracht.VoorkeurAfstudeerrichting = stageOpdracht.VoorkeurAfstudeerrichting;
            stageopdracht.InleidendeActiviteiten = stageOpdracht.InleidendeActiviteiten;
            stageopdracht.Periode = stageOpdracht.Periode;
            stageopdracht.Opdrachtgever = _context.Bedrijven.FirstOrDefault(s => s.Id == stageOpdracht.Opdrachtgever.Id);
            stageopdracht.Contactpersoon = _context.Contactpersonen.FirstOrDefault(s => s.Id == stageOpdracht.Contactpersoon.Id);
            stageopdracht.Bedrijfspromotor = _context.Bedrijfspromotors.FirstOrDefault(s => s.Id == stageOpdracht.Bedrijfspromotor.Id);
            stageopdracht.StagecoördinatorBehandelingStageopdrachtStudent = _context.Stagecoördinators.FirstOrDefault(s => s.Id == stageOpdracht.StagecoördinatorBehandelingStageopdrachtStudent.Id);
            stageopdracht.Stage = _context.Stages.FirstOrDefault(s => s.Id == stageOpdracht.Stage.Id);
            stageOpdracht.StudentFavorieten.ForEach(s => stageopdracht.StudentFavorieten.Add(_context.Studenten.FirstOrDefault(d => d.Id == s.Id)));
            _context.SaveChanges();
            */
        }

        public void UpdateFavorites(Stageopdracht stageopdracht, int studentId)
        {
            stageopdracht = _context.Stageopdrachten.FirstOrDefault(s => s.Id == stageopdracht.Id);
            Student student = stageopdracht.StudentFavorieten.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                stageopdracht.StudentFavorieten.Add(_context.Studenten.FirstOrDefault(s => s.Id == studentId));
            }
            else
            {
                stageopdracht.StudentFavorieten.Remove(student);
            }
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Stageopdracht stageOpdracht = _context.Stageopdrachten.FirstOrDefault(o => o.Id == id);
            _context.Stageopdrachten.Remove(stageOpdracht);
            _context.SaveChanges();
        }

        public IEnumerable<Stageopdracht> GetAllGoedgekeurdeStageopdrachten()
        {
            return _context.Stageopdrachten.Where(s => s.Status == Status.Goedgekeurd).ToList();
        }

        public Stageopdracht GetDetailStageopdracht(int id)
        {
            return
                _context.Stageopdrachten.Include(s => s.Opdrachtgever)
                    .Include(s => s.Contactpersoon)
                    .Include(s => s.Bedrijfspromotor).FirstOrDefault(s => s.Id == id);
        }
    }
}
