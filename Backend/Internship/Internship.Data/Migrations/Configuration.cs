using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using Internship.Api.Models;
using Internship.Data.DomainClasses;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Internship.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Internship.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Internship.Data.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<UserAccount>(new UserStore<UserAccount>(context));

            if (context.Users.ToList().Count == 0)
            {
                //Lector
                var lectorRole = new IdentityRole();
                lectorRole.Name = "Teacher";
                roleManager.Create(lectorRole);

                var rita = new UserAccount();
                rita.Email = "rita.lambrechts@pxl.be";
                rita.UserName = rita.Email;
                rita.EmailConfirmed = true;
                var resultRita = userManager.Create(rita, "lector");

                if (resultRita.Succeeded)
                    userManager.AddToRole(rita.Id, "Teacher");

                var bram = new UserAccount();
                bram.Email = "bram.heyns@pxl.be";
                bram.UserName = bram.Email;
                bram.EmailConfirmed = true;
                var resultBram = userManager.Create(bram, "lector");

                if (resultBram.Succeeded)
                    userManager.AddToRole(bram.Id, "Teacher");

                //Student
                var studentRole = new IdentityRole();
                studentRole.Name = "Student";
                roleManager.Create(studentRole);

                var ybe = new UserAccount();
                ybe.Email = "ybe.spapen@student.pxl.be";
                ybe.UserName = ybe.Email;
                ybe.EmailConfirmed = true;
                var resultYbe = userManager.Create(ybe, "student");

                if (resultYbe.Succeeded)
                    userManager.AddToRole(ybe.Id, "Student");

                var anissa = new UserAccount();
                anissa.Email = "anissa.schirock2@student.pxl.be";
                anissa.UserName = anissa.Email;
                anissa.EmailConfirmed = true;
                var resultAnissa = userManager.Create(anissa, "student");

                if (resultAnissa.Succeeded)
                    userManager.AddToRole(anissa.Id, "Student");

                //Bedrijf
                var bedrijfRole = new IdentityRole();
                bedrijfRole.Name = "Company";
                roleManager.Create(bedrijfRole);

                var cegeka = new UserAccount();
                cegeka.Email = "info@cegeka.com";
                cegeka.UserName = cegeka.Email;
                cegeka.EmailConfirmed = true;
                var resultCegeka = userManager.Create(cegeka, "bedrijf");

                if (resultCegeka.Succeeded)
                    userManager.AddToRole(cegeka.Id, "Company");

                var dataUnit = new UserAccount();
                dataUnit.Email = "info@dataunit.com";
                dataUnit.UserName = dataUnit.Email;
                dataUnit.EmailConfirmed = false;
                var resultDataUnit = userManager.Create(dataUnit, "bedrijf");

                if (resultDataUnit.Succeeded)
                    userManager.AddToRole(dataUnit.Id, "Company");

                var test = new UserAccount();
                test.Email = "info@test.com";
                test.UserName = test.Email;
                test.EmailConfirmed = false;
                var resulttest = userManager.Create(test, "bedrijf");

                if (resultDataUnit.Succeeded)
                    userManager.AddToRole(dataUnit.Id, "Company");

                //Coordinator
                var coordinatorRole = new IdentityRole();
                coordinatorRole.Name = "Coordinator";
                roleManager.Create(coordinatorRole);

                var marijke = new UserAccount();
                marijke.Email = "marijke.willems@pxl.be";
                marijke.UserName = marijke.Email;
                marijke.EmailConfirmed = true;
                var result = userManager.Create(marijke, "coordinator");

                if (result.Succeeded)
                    userManager.AddToRole(marijke.Id, "Coordinator");
            }

            context.Lectoren.AddOrUpdate(s => s.Voornaam,
                new Lector()
                {
                    Achternaam = "Lambrechts",
                    Voornaam = "Rita",
                    MagReviewen = true,
                    SchoolMail = "rita.lambrechts@pxl.be",
                    Telefoonnummer = "047890878",
                    Afstudeerrichting = Afstudeerrichting.ApplicatieOntwikkeling,
                    UserAccountId = context.Users.FirstOrDefault(s => s.Email == "rita.lambrechts@pxl.be").Id
                },
                new Lector()
                {
                    Achternaam = "Heyns",
                    Voornaam = "Bram",
                    MagReviewen = true,
                    SchoolMail = "bram.heyns@pxl.be",
                    Telefoonnummer = "0478678798",
                    Afstudeerrichting = Afstudeerrichting.SysteemEnNetwerkbeheer,
                    UserAccountId = context.Users.FirstOrDefault(s => s.Email == "bram.heyns@pxl.be").Id
                });
            context.SaveChanges();

            context.Studenten.AddOrUpdate(s => s.Achternaam,
                new Student()
                {
                    Afstudeerrichting = Afstudeerrichting.ApplicatieOntwikkeling,
                    Voornaam = "Ybe",
                    Achternaam = "Spapen",
                    Telefoonnummer = "0478098780",
                    Email = "Spapenybe@hotmail.com",
                    SchoolMail = "Ybe.Spapen@student.pxl.be",
                    Nummer = "11156786",
                    UserAccountId = context.Users.FirstOrDefault(s => s.Email == "ybe.spapen@student.pxl.be").Id
                },
                new Student()
                {
                    Afstudeerrichting = Afstudeerrichting.ApplicatieOntwikkeling,
                    Voornaam = "Anissa",
                    Achternaam = "Schirock",
                    Telefoonnummer = "0478095687",
                    Email = "AnissaSchirock@hotmail.com",
                    SchoolMail = "Anissa.Schirock2@student.pxl.be",
                    Nummer = "1109778",
                    UserAccountId = context.Users.FirstOrDefault(s => s.Email == "anissa.schirock2@student.pxl.be").Id
                });
            context.SaveChanges();

            context.Bedrijven.AddOrUpdate(b => b.Bedrijfsnaam,
                new Bedrijf()
                {
                    Telefoonnummer = "011897890",
                    AanwezigheidHandshake = true,
                    Bedrijfsnaam = "Cegeka",
                    Adres = "Berglaan",
                    Huisnummer = 200,
                    Gemeente = "Hasselt",
                    Postcode = 3910,
                    Email = "info@cegeka.com",
                    UserAccountId = context.Users.FirstOrDefault(s => s.Email == "info@cegeka.com").Id
                },
                new Bedrijf()
                {
                    Telefoonnummer = "011865457",
                    AanwezigheidHandshake = false,
                    Bedrijfsnaam = "Data Unit",
                    Adres = "Lindekensveld 5",
                    Huisnummer = 20,
                    Gemeente = "Lummen",
                    Postcode = 3560,
                    Email = "info@dataunit.com",
                    UserAccountId = context.Users.FirstOrDefault(s => s.Email == "info@dataunit.com").Id
                },
                new Bedrijf()
                {
                    Telefoonnummer = "011234865457",
                    AanwezigheidHandshake = false,
                    Bedrijfsnaam = "Test",
                    Adres = "test 5",
                    Huisnummer = 320,
                    Gemeente = "test",
                    Postcode = 35360,
                    Email = "info@test.com",
                    UserAccountId = context.Users.FirstOrDefault(s => s.Email == "info@test.com").Id
                }
                );
            context.SaveChanges();

            context.Stagecoördinators.AddOrUpdate(s => s.Voornaam,
                new Stagecoördinator()
                {
                    Voornaam = "Marijke",
                    Achternaam = "Willems",
                    Telefoonnummer = "0456789888",
                    Email = "Marijke.Willems@pxl.be",
                    Nummer = "02747593",
                    SchoolMail = "Marijke.Willems@pxl.be",
                    UserAccountId = context.Users.FirstOrDefault(s => s.Email == "marijke.willems@pxl.be").Id
                });
            context.SaveChanges();

            context.Bedrijfspromotors.AddOrUpdate(p => p.Achternaam,
                new Bedrijfspromotor()
                {
                    Voornaam = "Jos",
                    Achternaam = "Lamers",
                    Telefoonnummer = "011787898",
                    Email = "Jos.lamers@cegeka.com",
                    BedrijfInDienstId = 1
                },
                new Bedrijfspromotor()
                {
                    Voornaam = "Pieter",
                    Achternaam = "Rubens",
                    Telefoonnummer = "011279207",
                    Email = "Pieter.rubens@dataunit.be",
                    BedrijfInDienstId = 2
                });
            context.SaveChanges();

            context.Contactpersonen.AddOrUpdate(c => c.Achternaam,
                new Contactpersoon()
                {
                    Voornaam = "Elke",
                    Achternaam = "Vandenberk",
                    Telefoonnummer = "011678878",
                    Email = "Elke.vandenberk@cegeka.be",
                    BedrijfInDienstId = 1
                },
                new Contactpersoon()
                {
                    Voornaam = "Johan",
                    Achternaam = "Holseyns",
                    Telefoonnummer = "011279207",
                    Email = "Johan.holsteyns@dataunit.be",
                    BedrijfInDienstId = 2
                });
            context.SaveChanges();

            context.Stagevoorstellen.AddOrUpdate(s => s.Bemerkingen,
                //NIEUW STAGEVOORSTEL
                new Stagevoorstel()
                {
                    Stageopdracht = new Stageopdracht()
                    {
                        Opdrachtgever = context.Bedrijven.FirstOrDefault(b => b.Id == 2),
                        Status = Status.Nieuw,
                        ContactpersoonId = 2,
                        BedrijfspromotorId = 2,
                        AantalWerknemers = 78,
                        AantalITWerknemers = 70,
                        AantalITBegeleiders = 4,
                        VoorkeurAfstudeerrichting = Afstudeerrichting.SysteemEnNetwerkbeheer,
                        Omschrijving = "Onderhouden Windows server",
                        Omgeving = "Windows",
                        Locatie = "Lummen",
                        AantalGewensteStagiairs = 1,
                        Randvoorwaarden = "Er kunnen verplaatsingen zijn naar Bxl. De hoofdstage locatie is Lummen",
                        Onderzoeksthema = "Windows",
                        InleidendeActiviteiten = InleidendeActiviteit.CV,
                    },
                    StagecoördinatorBehandelingLector = context.Stagecoördinators.FirstOrDefault(s => s.Id == 1),
                    OpdrachtgeverId = 2,
                    TimeStamp = DateTime.Now,
                    Verstuurd = true,
                    Bemerkingen = "Stageopdracht1",
                },
                //STAGECO�RDINATOR HEEFT LECTOR TOEGEWEZEN
                new Stagevoorstel()
                {
                    Stageopdracht = new Stageopdracht()
                    {
                        Opdrachtgever = context.Bedrijven.FirstOrDefault(b => b.Id == 1),
                        Status = Status.ToegewezenLector,
                        ContactpersoonId = 1,
                        BedrijfspromotorId = 1,
                        AantalWerknemers = 100,
                        AantalITWerknemers = 50,
                        AantalITBegeleiders = 5,
                        VoorkeurAfstudeerrichting = Afstudeerrichting.ApplicatieOntwikkeling,
                        Omschrijving =
                            "Ontwikkeling van app voor ziekenhuis overpelt, deze app moet patienten info geven...",
                        Omgeving = "Java, C#",
                        Locatie = "Hasselt",
                        AantalGewensteStagiairs = 2,
                        Randvoorwaarden = "Verplaatsing naar Overpelt is mogelijk",
                        Onderzoeksthema = "Java",
                        InleidendeActiviteiten = InleidendeActiviteit.CV,
                    },
                    OpdrachtgeverId = 1,
                    TimeStamp = DateTime.Now,
                    Verstuurd = true,
                    Bemerkingen = "Stageopdracht2",
                },
                //GOEDGEKEURD DOOR LECTOR
                new Stagevoorstel()
                {
                    Stageopdracht = new Stageopdracht()
                    {
                        Opdrachtgever = context.Bedrijven.FirstOrDefault(b => b.Id == 2),
                        Status = Status.Goedgekeurd,
                        ContactpersoonId = 2,
                        BedrijfspromotorId = 2,
                        AantalWerknemers = 78,
                        AantalITWerknemers = 70,
                        AantalITBegeleiders = 4,
                        VoorkeurAfstudeerrichting = Afstudeerrichting.SysteemEnNetwerkbeheer,
                        Omschrijving = "Reeds geruime tijd is de Europese Unie bezig met het uitschrijven van een wetgeving in\r\nverband met data protectie. De General Data Protection Regulation (GDPR) zal een\r\ngrote invloed hebben op alle bedrijven die persoonsgegevens verzamelen en\r\nverwerken. Grote financi�le penaliteiten verplichten hen om conform de regulatie te\r\nwerken.\r\n\r\nDe stagiair zal een studie maken over GDPR . In een overzicht geeft hij aan, aan welke\r\neisen elk bedrijf moet voldoen en hoe dit praktisch wordt ingericht.\r\nNadien zal hij zijn bevindingen toepassen op Data Unit. Daarbij maakt hij gebruik van\r\nhet productgamma van Data Unit. De stagiair geeft aan:\r\n-   Welke oplossingen kunnen gebruikt worden bij de implementatie van GDPR\r\n-   Welke oplossingen Data Unit ontbreekt in zijn productgamma voor de uitrol van GDPR\r\n-   Testen van ��n of meerdere applicaties.\r\n-   Aangeven hoe Data Unit een ondersteunende rol kan spelen in de uitrol van GDPR bij bestaande klanten.",
                        Omgeving = "Linux",
                        Locatie = "Lummen",
                        AantalGewensteStagiairs = 1,
                        Randvoorwaarden = "Er kunnen verplaatsingen zijn naar Bxl. De hoofdstage locatie is Lummen",
                        Onderzoeksthema = "Linux",
                        InleidendeActiviteiten = InleidendeActiviteit.CV,
                    },
                    OpdrachtgeverId = 2,
                    ReviewLectorId = 2,
                    TimeStamp = DateTime.Now,
                    Verstuurd = true,
                    Bemerkingen = "Stageopdracht3",
                },
                new Stagevoorstel()
                {
                    Stageopdracht = new Stageopdracht()
                    {
                        Opdrachtgever = context.Bedrijven.FirstOrDefault(b => b.Id == 1),
                        Status = Status.Goedgekeurd,
                        ContactpersoonId = 1,
                        BedrijfspromotorId = 1,
                        AantalWerknemers = 100,
                        AantalITWerknemers = 50,
                        AantalITBegeleiders = 5,
                        VoorkeurAfstudeerrichting = Afstudeerrichting.ApplicatieOntwikkeling,
                        Omschrijving =
                            "Ontwikkeling van app voor mobile vikings, deze app moet klanten info geven...",
                        Omgeving = "Java, C#",
                        Locatie = "Antwerpen",
                        AantalGewensteStagiairs = 1,
                        Randvoorwaarden = "Verplaatsing naar Antwerpen is mogelijk",
                        Onderzoeksthema = "Java",
                        InleidendeActiviteiten = InleidendeActiviteit.CV,
                    },
                    OpdrachtgeverId = 1,
                    ReviewLectorId = 1,
                    TimeStamp = DateTime.Now,
                    Verstuurd = true,
                    Bemerkingen = "Stageopdracht4",
                },
                //AFGEKEURD DOOR LECTOR
                new Stagevoorstel()
                {
                    Stageopdracht = new Stageopdracht()
                    {
                        Opdrachtgever = context.Bedrijven.FirstOrDefault(b => b.Id == 1),
                        Status = Status.Afgekeurd,
                        ContactpersoonId = 1,
                        BedrijfspromotorId = 1,
                        AantalWerknemers = 100,
                        AantalITWerknemers = 50,
                        AantalITBegeleiders = 5,
                        VoorkeurAfstudeerrichting = Afstudeerrichting.ApplicatieOntwikkeling,
                        Omschrijving =
                            "Ontwikkeling van app voor supermarkt, deze app moet klanten info geven...",
                        Omgeving = "Java, C#",
                        Locatie = "Hasselt",
                        AantalGewensteStagiairs = 2,
                        Randvoorwaarden = "Verplaatsing naar hasselt is mogelijk",
                        Onderzoeksthema = "Java",
                        InleidendeActiviteiten = InleidendeActiviteit.CV,
                    },
                    OpdrachtgeverId = 1,
                    ReviewLectorId = 1,
                    TimeStamp = DateTime.Now,
                    Verstuurd = true,
                    Bemerkingen = "Stageopdracht5",
                });
            context.SaveChanges();

            context.Reviews.AddOrUpdate(r => r.Feedback,
                new Review()
                {
                    Stagevoorstel = context.Stagevoorstellen.FirstOrDefault(s => s.Id == 3),
                    Feedback = "Stagevoorstel goedgekeurd, er wordt verder contact met u opgenomen",
                    ReviewerId = 2
                },
                new Review()
                {
                    Stagevoorstel = context.Stagevoorstellen.FirstOrDefault(s => s.Id == 4),
                    Feedback =
                        "Stagevoorstel goedgekeurd, er zijn enkele wijziging gemaakt aan de omschrijving, er wordt verder contact met u opgenomen",
                    ReviewerId = 1
                },
                new Review()
                {
                    Stagevoorstel = context.Stagevoorstellen.FirstOrDefault(s => s.Id == 5),
                    Feedback =
                        "Stagevoorstel afgekeurd, er is onvoldoende informatie gegeven",
                    ReviewerId = 1
                });
            context.SaveChanges();

            //List<Student> studenten = new List<Student>();
            //studenten.Add(context.Studenten.FirstOrDefault(s => s.Id == 1));

            //context.Stages.AddOrUpdate(s => s.EindDatum,
            //    new Stage()
            //    {
            //        StageOpdracht = context.Stageopdrachten.FirstOrDefault(s => s.Id == 3),
            //        StartDatum = new DateTime(2017, 03, 24),
            //        EindDatum = new DateTime(2017, 06, 24),
            //        Students = studenten,
            //        StageStatus = StageStatus.Goedgekeurd
            //    });
            //context.SaveChanges();
        }
    }
}
