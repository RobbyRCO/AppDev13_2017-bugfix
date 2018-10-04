using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship.Data.DomainClasses;
using System.Data.Entity;

namespace Internship.Data.Repositories
{
    public class StagevoorstelDBRepository : IStagevoorstelRepository
    {
        private ApplicationDbContext _context;

        public StagevoorstelDBRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Stagevoorstel> GetAll()
        {
            return _context.Stagevoorstellen.ToList();
        }

        public Stagevoorstel Post(Stagevoorstel stagevoorstel)
        {
            if (stagevoorstel.Stageopdracht.Contactpersoon != null)
            {
                _context.Contactpersonen.Add(stagevoorstel.Stageopdracht.Contactpersoon);
            }
            if (stagevoorstel.Stageopdracht.Bedrijfspromotor != null)
            {
                _context.Bedrijfspromotors.Add(stagevoorstel.Stageopdracht.Bedrijfspromotor);
            }

            _context.Stagevoorstellen.Add(stagevoorstel);
            _context.SaveChanges();
            return stagevoorstel;
        }

        public Stagevoorstel Get(int id)
        {
            return _context.Stagevoorstellen.FirstOrDefault(s => s.Id == id);
        }

        public Stagevoorstel GetByTimeStamp(DateTime timeStamp)
        {
            return _context.Stagevoorstellen.FirstOrDefault(s => s.TimeStamp.Equals(timeStamp));
        }

        public void UpdateReview(Stagevoorstel stagevoorstel)
        {
            var contextStagevoorstel = _context.Stagevoorstellen.FirstOrDefault(s => s.Id == stagevoorstel.Id);
            contextStagevoorstel.Review = stagevoorstel.Review;
            contextStagevoorstel.Stageopdracht.Status = stagevoorstel.Stageopdracht.Status;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Stagevoorstel stagevoorstel = _context.Stagevoorstellen.FirstOrDefault(s => s.Id == id);
            _context.Stagevoorstellen.Remove(stagevoorstel);
            _context.SaveChanges();
        }

        public Stagevoorstel GetDetail(int id)
        {
            return _context.Stagevoorstellen.Include(s => s.Review).Include(s => s.Stageopdracht).Include(s => s.Stageopdracht.Bedrijfspromotor).Include(s => s.Stageopdracht.Contactpersoon).FirstOrDefault(s => s.Id == id);
        }

        public void Update(Stagevoorstel stagevoorstel)
        {
            var contextStagevoorstel = _context.Stagevoorstellen.Include(s => s.Stageopdracht).Include(s => s.Stageopdracht.Contactpersoon).Include(s => s.Stageopdracht.Bedrijfspromotor).FirstOrDefault(s => s.Id == stagevoorstel.Id);
            contextStagevoorstel.Bemerkingen = stagevoorstel.Bemerkingen;
            
            //Stageopdracht
            contextStagevoorstel.Stageopdracht.VoorkeurAfstudeerrichting =
                stagevoorstel.Stageopdracht.VoorkeurAfstudeerrichting;
            contextStagevoorstel.Stageopdracht.Omschrijving = stagevoorstel.Stageopdracht.Omschrijving;
            contextStagevoorstel.Stageopdracht.Omgeving = stagevoorstel.Stageopdracht.Omgeving;
            contextStagevoorstel.Stageopdracht.Randvoorwaarden = stagevoorstel.Stageopdracht.Randvoorwaarden;
            contextStagevoorstel.Stageopdracht.Onderzoeksthema = stagevoorstel.Stageopdracht.Onderzoeksthema;
            contextStagevoorstel.Stageopdracht.InleidendeActiviteiten =
                stagevoorstel.Stageopdracht.InleidendeActiviteiten;
            contextStagevoorstel.Stageopdracht.AantalGewensteStagiairs =
                stagevoorstel.Stageopdracht.AantalGewensteStagiairs;
            contextStagevoorstel.Stageopdracht.Periode = stagevoorstel.Stageopdracht.Periode;
            contextStagevoorstel.Stageopdracht.BeschrijvingTechnischeOmgeving =
                stagevoorstel.Stageopdracht.BeschrijvingTechnischeOmgeving;

            //Locatie
            contextStagevoorstel.Stageopdracht.Locatie = stagevoorstel.Stageopdracht.Locatie;
            contextStagevoorstel.Stageopdracht.AantalWerknemers = stagevoorstel.Stageopdracht.AantalWerknemers;
            contextStagevoorstel.Stageopdracht.AantalITWerknemers = stagevoorstel.Stageopdracht.AantalITWerknemers;
            contextStagevoorstel.Stageopdracht.AantalITBegeleiders = stagevoorstel.Stageopdracht.AantalITBegeleiders;

            //Contactpersoon
            if (stagevoorstel.Stageopdracht.Contactpersoon != null)
            {
                contextStagevoorstel.Stageopdracht.Contactpersoon = stagevoorstel.Stageopdracht.Contactpersoon;
            }
            else
            {
                contextStagevoorstel.Stageopdracht.ContactpersoonId = stagevoorstel.Stageopdracht.ContactpersoonId;
            }

            //Bedrijfspromotor
            if (stagevoorstel.Stageopdracht.Bedrijfspromotor != null)
            {
                contextStagevoorstel.Stageopdracht.Bedrijfspromotor = stagevoorstel.Stageopdracht.Bedrijfspromotor;
            }
            else
            {
                contextStagevoorstel.Stageopdracht.BedrijfspromotorId = stagevoorstel.Stageopdracht.BedrijfspromotorId;
            }

            _context.SaveChanges();
        }

        public void UpdateToekennenLector(Stagevoorstel stagevoorstel)
        {
            var contextStagevoorstel = _context.Stagevoorstellen.FirstOrDefault(s => s.Id == stagevoorstel.Id);
            contextStagevoorstel.Stageopdracht.Status = stagevoorstel.Stageopdracht.Status;
            contextStagevoorstel.ReviewLectorId = stagevoorstel.ReviewLectorId;
            _context.SaveChanges();
        }

        public void UpdateStatus(Stagevoorstel stagevoorstel)
        {
            var contextStagevoorstel = _context.Stagevoorstellen.FirstOrDefault(s => s.Id == stagevoorstel.Id);
            contextStagevoorstel.Stageopdracht.Status = stagevoorstel.Stageopdracht.Status;
            _context.SaveChanges();
        }

        public IEnumerable<Stagevoorstel> GetTeBehandeleStagevoorstellen()
        {
            return _context.Stagevoorstellen.Include(s => s.Stageopdracht).Where(s => s.Stageopdracht.Status == Status.GoedgekeurdDoorLector || s.Stageopdracht.Status == Status.AfgekeurdDoorLector).ToList();
        }
    }
}
