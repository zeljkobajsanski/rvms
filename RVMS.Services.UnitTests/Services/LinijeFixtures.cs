using System.Collections.Generic;
using System.Diagnostics;
using Bootstrap;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RVMS.Model.DTO;
using RVMS.Model.Entities;
using RVMS.Model.Repository;
using RVMS.Model.Repository.Interfaces;
using RVMS.Services.Services;
using RVMS.Services.Services.Interfaces;
using System.Linq;
using Bootstrap.AutoMapper;

namespace RVMS.Services.UnitTests.Services
{
    [TestClass]
    public class LinijeFixtures
    {
        private Linije fCut;

        private readonly Mock<IRepositories> fRepositories = new Mock<IRepositories>();

        private readonly Mock<ILinijeRepository> fLinijeRepository = new Mock<ILinijeRepository>(); 

        private readonly Mock<IStajalista> fStajalistaService = new Mock<IStajalista>(); 

        private readonly Mock<IRelacijeRepository> fRelacijeRepository = new Mock<IRelacijeRepository>();

        private readonly Mock<IStajalistaLinijeRepository> fStajalistaLinijeRepository = new Mock<IStajalistaLinijeRepository>();

        private readonly Mock<StajalistaRepository> fStajalistaRepository = new Mock<StajalistaRepository>();

        private readonly Mock<MedjustanicnaRastojanjaRepository> fMedjustanicnaRastojanjaRepository = new Mock<MedjustanicnaRastojanjaRepository>();

        private StajalisteDTO[] fStajalista;

        private IQueryable<Relacija> fRelacije;

        [ClassInitialize]
        public static void ClassInit(TestContext ctx)
        {
            Bootstrapper.With.AutoMapper().Start();
        }

        [TestInitialize]
        public void TestInit()
        {
            fCut = new Linije(fRepositories.Object, fStajalistaService.Object);
            fRepositories.SetupGet(x => x.StajalistaLinijeRepository).Returns(fStajalistaLinijeRepository.Object);
            fRepositories.SetupGet(x => x.RelacijeRepository).Returns(fRelacijeRepository.Object);
            fRepositories.SetupGet(x => x.LinijeRepository).Returns(fLinijeRepository.Object);
            fRepositories.SetupGet(x => x.StajalistaRepository).Returns(fStajalistaRepository.Object);
            fRepositories.SetupGet(x => x.MedjustanicnaRastojanjaRepository).Returns(fMedjustanicnaRastojanjaRepository.Object);

            KreirajRelacijuIStajalista();
        }

       

        [TestMethod]
        public void SacuvajNoviLiniju()
        {
            var dto = new LinijaDTO
            {
                Id = 0,
                Naziv = "Sremska Mitrovica AS - Beograd 'Sava Centar' - Beograd AS (autoputem)"
            };
            fCut.SacuvajLiniju(dto);

            fLinijeRepository.Verify(x => x.Add(It.Is<Linija>(linija => linija.Naziv == dto.Naziv)));
            fRepositories.Verify(x => x.Save());
        }

        [TestMethod]
        public void SacuvajPostojecuLiniju()
        {
            var dto = new LinijaDTO
            {
                Id = 1,
                Naziv = "Sremska Mitrovica AS - Beograd 'Sava Centar' - Beograd AS (autoputem)"
            };
            fCut.SacuvajLiniju(dto);

            fLinijeRepository.Verify(x => x.Update(It.Is<Linija>(linija => linija.Naziv == dto.Naziv)));
            fRepositories.Verify(x => x.Save());
        }

        [TestMethod]
        public void Kreiraj_String_Opisa_Relacije_Sa_Jednim_Medjustanicnim_Rastojanjem()
        {
            var relacija = new Relacija
            {
                MedjustanicnaRastojanja = new List<MedjustanicnoRastojanje>
                {
                    new MedjustanicnoRastojanje
                    {
                        PolaznoStajaliste = new Stajaliste {Naziv = "Sremska Mitrovica AS"},
                        DolaznoStajaliste = new Stajaliste {Naziv = "Sremska Mitrovica - Rumska malta"}
                    }
                }
            };
            var opis = Linije.KreirajStringOpisaRelacije(relacija);
            Assert.AreEqual("Sremska Mitrovica AS,Sremska Mitrovica - Rumska malta", opis);
        }

        [TestMethod]
        public void Kreiraj_String_Opisa_Relacije_Sa_Vise_Medjustanicnih_Rastojanja()
        {
            var opis = Linije.KreirajStringOpisaRelacije(fRelacije.ElementAt(0));
            Assert.AreEqual("Sremska Mitrovica AS,Sremska Mitrovica - Rumska malta,Novi Beograd - Studentski grad,Novi Beograd - Sava Centar,Beograd AS", opis);
        }

        [TestMethod]
        public void DodajStajalisteNaLiniju()
        {
            // arrange
            var linija = new Linija
            {
                Id = 1,
                Naziv = "Sremska Mitrovica AS - Beograd 'Sava Centar' - Beograd AS (autoputem)",
                Stajalista = new List<StajalisteLinije>()
            };
            fRelacijeRepository.Setup(x => x.VratiRelacijeKojeProlazeKrozStanicu(1)).Returns(fRelacije);
            fLinijeRepository.Setup(x => x.UcitajLinijuIStajalista(1)).Returns(linija);
            fStajalistaService.Setup(x => x.VratiSusednaStajalista(1)).Returns(fStajalista);
            fStajalistaRepository.Setup(x => x.Get(1)).Returns(new Stajaliste {Id = 1, Naziv = "Sremska Mitrovica AS"});

            // act
            var dto = fCut.DodajStajalisteNaLiniju(1, 1);

            // assert
            fRepositories.Verify(x => x.Save());
            
            Assert.AreEqual(fStajalista, dto.Stajalista);
            Assert.AreEqual(1, dto.Relacije.Count);
            Assert.AreEqual(fRelacije.ElementAt(0).Naziv, dto.Relacije[0].Naziv);
            Assert.AreEqual("Sremska Mitrovica AS,Sremska Mitrovica - Rumska malta,Novi Beograd - Studentski grad,Novi Beograd - Sava Centar,Beograd AS", 
                dto.Relacije[0].Napomena);
            fStajalistaService.Verify(x => x.VratiSusednaStajalista(1));
            fRelacijeRepository.Verify(x => x.VratiRelacijeKojeProlazeKrozStanicu(1));

            Assert.AreEqual(linija.Id, dto.Linija.Id);
            Assert.AreEqual(linija.Naziv, dto.Linija.Naziv);
            Assert.AreEqual(1, dto.Linija.Stajalista.Count);
            Assert.AreEqual("Sremska Mitrovica AS", dto.Linija.Stajalista[0].NazivStajalista);
        }

        [TestMethod]
        public void DodajStajalisteNaLinijuKadaPostojeUnetaStajalista()
        {
            // arrange
            var linija = new Linija
            {
                Id = 1,
                Naziv = "Sremska Mitrovica AS - Beograd 'Sava Centar' - Beograd AS (autoputem)",
                Stajalista = new List<StajalisteLinije>()
                {
                    new StajalisteLinije{Id = 1, Stajaliste = new Stajaliste(){Id = 1, Naziv = "Sremska Mitrovica AS"}, Rastojanje = 0},
                    new StajalisteLinije{Id = 2, Stajaliste = new Stajaliste(){Id = 2, Naziv = "Sremska Mitrovica - Rumska Malta"}, Rastojanje = 3.1M},
                    new StajalisteLinije{Id = 3, Stajaliste = new Stajaliste(){Id = 3, Naziv = "Novi Beograd - Studentski grad"}, Rastojanje = 70.1M},
                }
            };
            fRelacijeRepository.Setup(x => x.VratiRelacijeKojeProlazeKrozStanicu(4)).Returns(fRelacije);
            fLinijeRepository.Setup(x => x.UcitajLinijuIStajalista(1)).Returns(linija);
            fStajalistaService.Setup(x => x.VratiSusednaStajalista(4)).Returns(fStajalista);
            fStajalistaRepository.Setup(x => x.Get(4)).Returns(new Stajaliste { Id = 4, Naziv = "Novi Beograd - Sava Centar" });
            fMedjustanicnaRastojanjaRepository.Setup(x => x.VratiMedjustanicnaRastojanja(3, 4)).Returns(3);

            // act
            var dto = fCut.DodajStajalisteNaLiniju(1, 4);

            // assert
            fRepositories.Verify(x => x.Save());

            Assert.AreEqual(fStajalista, dto.Stajalista);
            Assert.AreEqual(1, dto.Relacije.Count);
            Assert.AreEqual(fRelacije.ElementAt(0).Naziv, dto.Relacije[0].Naziv);
            Assert.AreEqual("Sremska Mitrovica AS,Sremska Mitrovica - Rumska malta,Novi Beograd - Studentski grad,Novi Beograd - Sava Centar,Beograd AS",
                dto.Relacije[0].Napomena);
            fStajalistaService.Verify(x => x.VratiSusednaStajalista(4));
            fRelacijeRepository.Verify(x => x.VratiRelacijeKojeProlazeKrozStanicu(4));

            Assert.AreEqual(linija.Id, dto.Linija.Id);
            Assert.AreEqual(linija.Naziv, dto.Linija.Naziv);
            Assert.AreEqual(4, dto.Linija.Stajalista.Count);
            Assert.AreEqual("Sremska Mitrovica AS", dto.Linija.Stajalista[0].NazivStajalista);
            Assert.AreEqual(0, dto.Linija.Stajalista[0].Rastojanje);
            Assert.AreEqual("Sremska Mitrovica - Rumska Malta", dto.Linija.Stajalista[1].NazivStajalista);
            Assert.AreEqual(3.1M, dto.Linija.Stajalista[1].Rastojanje);
            Assert.AreEqual("Novi Beograd - Studentski grad", dto.Linija.Stajalista[2].NazivStajalista);
            Assert.AreEqual(70.1M, dto.Linija.Stajalista[2].Rastojanje);
            Assert.AreEqual("Novi Beograd - Sava Centar", dto.Linija.Stajalista[3].NazivStajalista);
            Assert.AreEqual(73.1M, dto.Linija.Stajalista[3].Rastojanje);
        }

        [TestMethod]
        public void Obrisi_Stajaliste()
        {
            // arrange
            var linija = new Linija
            {
                Id = 1,
                Naziv = "Sremska Mitrovica AS - Beograd 'Sava Centar' - Beograd AS (autoputem)",
                Stajalista = new List<StajalisteLinije>()
                {
                    new StajalisteLinije{Id = 1, StajalisteId = 1, Stajaliste = new Stajaliste(){Id = 1, Naziv = "Sremska Mitrovica AS"}, Rastojanje = 0},
                    new StajalisteLinije{Id = 2, StajalisteId = 2, Stajaliste = new Stajaliste(){Id = 2, Naziv = "Sremska Mitrovica - Rumska Malta"}, Rastojanje = 3.1M},
                    new StajalisteLinije{Id = 3, StajalisteId = 3, Stajaliste = new Stajaliste(){Id = 3, Naziv = "Novi Beograd - Studentski grad"}, Rastojanje = 70.1M},
                }
            };
            fLinijeRepository.Setup(x => x.UcitajLinijuIStajalista(1)).Returns(linija);

            // act
            var dto = fCut.SkloniStajalisteSaLinije(1, 2);

            // assert
            fRepositories.Verify(x => x.Save());
            Assert.AreEqual(1, dto.Linija.Stajalista.Count);
            fStajalistaService.Verify(x => x.VratiSusednaStajalista(1));
            fRelacijeRepository.Verify(x => x.VratiRelacijeKojeProlazeKrozStanicu(1));
            
        }

        [TestMethod]
        public void Azuriraj_Stajaliste_Linije()
        {
            // arrange
            var dto = new StajalisteLinijeDTO
            {
                Id = 2,
                NazivStajalista = "Sremska Mitrovica AS",
                Rastojanje = 3.1M,
                LinijaId = 1,
                StajalisteId = 12,
                Latituda = 45.901M,
                Longituda = 19.8761M,
                Aktivan = true
            };
            

            // act
            fCut.AzurirajStajalisteLinije(dto);

            // assert
            fStajalistaLinijeRepository.Verify(x => x.Update(It.Is<StajalisteLinije>(s => s.Id == dto.Id)));
            fRepositories.Verify(x => x.Save());
            
        }

        [TestMethod]
        public void Dodaj_Stajalista_Relacije_Na_Liniju_Koja_Vec_Ima_Stajalista()
        {
            // arrange
            const int idRelacije = 1;
            const int idLinije = 1;
            var relacija = fRelacije.Single();
            fRelacijeRepository.Setup(x => x.VratiRelacijuSaRastojanjima(idRelacije)).Returns(relacija);
            var linija = new Linija
            {
                Id = idLinije,
                Naziv = "Sremska Mitrovica AS - Beograd 'Sava Centar' - Beograd AS (autoputem)",
                Stajalista = new List<StajalisteLinije>()
                {
                    new StajalisteLinije{Id = 1, StajalisteId = 1, Stajaliste = new Stajaliste(){Id = 1, Naziv = "Sremska Mitrovica AS"}, Rastojanje = 0},
                    new StajalisteLinije{Id = 2, StajalisteId = 2, Stajaliste = new Stajaliste(){Id = 2, Naziv = "Sremska Mitrovica - Rumska Malta"}, Rastojanje = 4M},
                    new StajalisteLinije{Id = 3, StajalisteId = 3, Stajaliste = new Stajaliste(){Id = 3, Naziv = "Novi Beograd - Studentski grad"}, Rastojanje = 71M},
                }
            };
            fLinijeRepository.Setup(x => x.UcitajLinijuIStajalista(idLinije)).Returns(linija);

            // act
            var dto = fCut.DodajStajalistaRelacijeNaLiniju(idLinije, idRelacije);
            fRepositories.Verify(x => x.Save());
            Assert.AreEqual(5, dto.Linija.Stajalista.Count);
            Assert.AreEqual("Novi Beograd - Sava Centar", dto.Linija.Stajalista[3].NazivStajalista);
            Assert.AreEqual(74M, dto.Linija.Stajalista[3].Rastojanje);
            Assert.AreEqual("Beograd AS", dto.Linija.Stajalista[4].NazivStajalista);
            Assert.AreEqual(78M, dto.Linija.Stajalista[4].Rastojanje);
        }

        [TestMethod]
        public void Dodaj_Stajalista_Relacije_Na_Liniju_Koja_Nema_Stajalista()
        {
            // arrange
            const int idRelacije = 1;
            const int idLinije = 1;
            var relacija = fRelacije.Single();
            fRelacijeRepository.Setup(x => x.VratiRelacijuSaRastojanjima(idRelacije)).Returns(relacija);
            var linija = new Linija
            {
                Id = idLinije,
                Naziv = "Sremska Mitrovica AS - Beograd 'Sava Centar' - Beograd AS (autoputem)",
            };
            fLinijeRepository.Setup(x => x.UcitajLinijuIStajalista(idLinije)).Returns(linija);

            // act
            var dto = fCut.DodajStajalistaRelacijeNaLiniju(idLinije, idRelacije);
            fRepositories.Verify(x => x.Save());
            Assert.AreEqual(5, dto.Linija.Stajalista.Count);
            Assert.AreEqual("Sremska Mitrovica AS", dto.Linija.Stajalista[0].NazivStajalista);
            Assert.AreEqual(0, dto.Linija.Stajalista[0].Rastojanje);
            Assert.AreEqual("Sremska Mitrovica - Rumska malta", dto.Linija.Stajalista[1].NazivStajalista);
            Assert.AreEqual(4M, dto.Linija.Stajalista[1].Rastojanje);
            Assert.AreEqual("Novi Beograd - Studentski grad", dto.Linija.Stajalista[2].NazivStajalista);
            Assert.AreEqual(71M, dto.Linija.Stajalista[2].Rastojanje);
            Assert.AreEqual("Novi Beograd - Sava Centar", dto.Linija.Stajalista[3].NazivStajalista);
            Assert.AreEqual(74M, dto.Linija.Stajalista[3].Rastojanje);
            Assert.AreEqual("Beograd AS", dto.Linija.Stajalista[4].NazivStajalista);
            Assert.AreEqual(78M, dto.Linija.Stajalista[4].Rastojanje);
        }

        private void KreirajRelacijuIStajalista()
        {
            fStajalista = new[]
            {
                new StajalisteDTO {Id = 1, Naziv = "Sremska Mitrovica AS"},
                new StajalisteDTO {Id = 2, Naziv = "Sremska Mitrovica - Rumska malta"},
                new StajalisteDTO {Id = 3, Naziv = "Novi Beograd - Studentski grad"},
                new StajalisteDTO {Id = 4, Naziv = "Novi Beograd - Sava centar"},
                new StajalisteDTO {Id = 5, Naziv = "Beograd AS"},
            };
            fRelacije = new[]
            {
                new Relacija
                {
                    Id = 1,
                    Naziv = "Sremska Mitrovica AS - Beograd AS (autoputem)",
                    MedjustanicnaRastojanja = new List<MedjustanicnoRastojanje>()
                    {
                        new MedjustanicnoRastojanje
                        {
                            Id = 1,
                            PolaznoStajalisteId = 1,
                            PolaznoStajaliste = new Stajaliste
                            {
                                Id = 1,
                                Naziv = "Sremska Mitrovica AS"
                            },
                            DolaznoStajalisteId = 2,
                            DolaznoStajaliste = new Stajaliste
                            {
                                Id = 2,
                                Naziv = "Sremska Mitrovica - Rumska malta"
                            },
                            Rastojanje = 3.1M,
                            VremeVoznje = 8
                        },
                        new MedjustanicnoRastojanje
                        {
                            Id = 2,
                            PolaznoStajalisteId = 2,
                            PolaznoStajaliste = new Stajaliste
                            {
                                Id = 2,
                                Naziv = "Sremska Mitrovica - Rumska malta"
                            },
                            DolaznoStajalisteId = 3,
                            DolaznoStajaliste = new Stajaliste
                            {
                                Id = 3,
                                Naziv = "Novi Beograd - Studentski grad"
                            },
                            Rastojanje = 67M,
                            VremeVoznje = 48
                        },
                        new MedjustanicnoRastojanje
                        {
                            Id = 3,
                            PolaznoStajalisteId = 3,
                            PolaznoStajaliste = new Stajaliste
                            {
                                Id = 3,
                                Naziv = "Novi Beograd - Studentski grad"
                            },
                            DolaznoStajalisteId = 4,
                            DolaznoStajaliste = new Stajaliste
                            {
                                Id = 4,
                                Naziv = "Novi Beograd - Sava Centar"
                            },
                            Rastojanje = 3M,
                            VremeVoznje = 3
                        },
                        new MedjustanicnoRastojanje
                        {
                            Id = 4,
                            PolaznoStajalisteId = 4,
                            PolaznoStajaliste = new Stajaliste
                            {
                                Id = 4,
                                Naziv = "Novi Beograd - Sava Centar"
                            },
                            DolaznoStajalisteId = 5,
                            DolaznoStajaliste = new Stajaliste
                            {
                                Id = 5,
                                Naziv = "Beograd AS"
                            },
                            Rastojanje = 3.6M,
                            VremeVoznje = 7
                        },
                    }
                }
            }.AsQueryable();
        }
        
    }
}