using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RVMS.Model.DTO;
using RVMS.Model.Entities;
using RVMS.Model.Repository;
using RVMS.Model.Repository.Interfaces;
using RVMS.Services.Services;
using RVMS.Services.Services.Interfaces;
using System.Linq;

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

        public LinijeFixtures()
        {
            fCut = new Linije(fRepositories.Object, fStajalistaService.Object);
        }

        [TestMethod]
        public void SacuvajNoviLiniju()
        {
            fRepositories.SetupGet(x => x.LinijeRepository).Returns(fLinijeRepository.Object);
            var dto = new LinijaDTO
            {
                Id = 0,
                Naziv = "Novi Sad AS - Beograd AS"
            };
            fCut.SacuvajLiniju(dto);

            fLinijeRepository.Verify(x => x.Add(It.Is<Linija>(linija => linija.Naziv == dto.Naziv)));
            fRepositories.Verify(x => x.Save());
        }

        [TestMethod]
        public void SacuvajPostojecuLiniju()
        {
            fRepositories.SetupGet(x => x.LinijeRepository).Returns(fLinijeRepository.Object);
            var dto = new LinijaDTO
            {
                Id = 1,
                Naziv = "Novi Sad AS - Beograd AS"
            };
            fCut.SacuvajLiniju(dto);

            fLinijeRepository.Verify(x => x.Update(It.Is<Linija>(linija => linija.Naziv == dto.Naziv)));
            fRepositories.Verify(x => x.Save());
        }

        [TestMethod]
        public void DodajStajalisteNaLiniju()
        {
            // arrange
            fRepositories.SetupGet(x => x.StajalistaLinijeRepository).Returns(fStajalistaLinijeRepository.Object);
            fRepositories.SetupGet(x => x.RelacijeRepository).Returns(fRelacijeRepository.Object);
            var linija = new Linija();
            var stajalista = new[]
                                 {
                                     new StajalisteDTO {Id = 1, Naziv = "Valjevo AS"}
                                 };
            var relacije = new[] {new Relacija()
                                      {
                                          Naziv = "Novi Sad AS - Beograd AS",
                                          MedjustanicnaRastojanja = new List<MedjustanicnoRastojanje>()
                                                                        {
                                                                            new MedjustanicnoRastojanje()
                                                                                {
                                                                                    PolaznoStajaliste = new Stajaliste{Naziv = "Novi Sad AS"},
                                                                                    DolaznoStajaliste = new Stajaliste{Naziv = "Beograd AS"},
                                                                                }
                                                                        }
                                      }}.AsQueryable();
            fRelacijeRepository.Setup(x => x.VratiRelacijeKojeProlazeKrozStanicu(1)).Returns(relacije);
            fLinijeRepository.Setup(x => x.UcitajLinijuIStajalista(1)).Returns(linija);
            fStajalistaService.Setup(x => x.VratiSusednaStajalista(1)).Returns(stajalista);

            // act
            var dto = fCut.DodajStajalisteNaLiniju(1, 1);

            // assert
            fStajalistaLinijeRepository.Verify(x => x.Add(It.IsAny<StajalisteLinije>()));
            fRepositories.Verify(x => x.Save());
            fStajalistaService.Verify(x => x.VratiSusednaStajalista(1));
            fRelacijeRepository.Verify(x => x.VratiRelacijeKojeProlazeKrozStanicu(1));
            Assert.AreEqual(stajalista, dto.Stajalista);
            Assert.AreEqual(1, dto.Relacije.Count);
            Assert.AreEqual("Novi Sad AS - Beograd AS", dto.Relacije[0].Naziv);
            Assert.AreEqual("Novi Sad AS,Beograd AS,", dto.Relacije[0].Napomena);
        }
    }
}