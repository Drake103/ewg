using System;
using System.IO;
using Autofac;
using EWG.DependencyResolver;
using EWG.Infrastructure.Services.Common;
using EWG.Infrastructure.Services.Impl.Tests.ReplayParsing;
using EWG.Infrastructure.Services.Replays;
using NUnit.Framework;

namespace EWG.Infrastructure.Services.Impl.Tests.Replays
{
    [TestFixture]
    public class ReplayServiceFuncTests
    {
        private const string NhConfigurationFilePath = "test.hibernate.cfg.xml";

        private IContainer _container;
        private IReplayService _service;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var builder = new AutofacContainerBuilderForTests(NhConfigurationFilePath).Get();

            _container = builder.Build();

            var dbService = _container.Resolve<IDatabaseService>();
            dbService.DropCreateAndInit(NhConfigurationFilePath);

            _service = _container.Resolve<IReplayService>();
        }

        [Test]
        public void ReplaySaveTest01()
        {
            var replayBytes = ReplayParserTestResources.Steel_Balalaika_vs_We_suck_at_wargame_3vs3;
            _service.SaveReplay(new MemoryStream(replayBytes), Guid.NewGuid().ToString());
        }

        [Test]
        public void ReplaySaveTest02()
        {
            var replayBytes = ReplayParserTestResources.WRG_B5P_vs_1144_Tough_Jungle;
            _service.SaveReplay(new MemoryStream(replayBytes), Guid.NewGuid().ToString());
        }

        [Test]
        public void ReplaySaveTest03()
        {
            var replayBytes = ReplayParserTestResources._3v3_Tourney_Viteska_Brigada_B5P;
            _service.SaveReplay(new MemoryStream(replayBytes), Guid.NewGuid().ToString());
        }

        [Test]
        public void ReplaySaveTest04()
        {
            var replayBytes = ReplayParserTestResources.DDR_Elite_armor_push;
            _service.SaveReplay(new MemoryStream(replayBytes), Guid.NewGuid().ToString());
        }

        [Test]
        public void ReplaySaveTest05()
        {
            var replayBytes = ReplayParserTestResources.DuroSVK_vs_HeiligeHopfastauern_rematch;
            _service.SaveReplay(new MemoryStream(replayBytes), Guid.NewGuid().ToString());
        }

        [Test]
        public void ReplaySaveTest06()
        {
            var replayBytes = ReplayParserTestResources.Eco_Tourney_Firestarter_vs_Duro_Game_1;
            _service.SaveReplay(new MemoryStream(replayBytes), Guid.NewGuid().ToString());
        }

        [Test]
        public void ReplaySaveTest07()
        {
            var replayBytes = ReplayParserTestResources.Eco_Tourney_Firestarter_vs_Duro_Game_2;
            _service.SaveReplay(new MemoryStream(replayBytes), Guid.NewGuid().ToString());
        }

        [Test]
        public void ReplaySaveTest08()
        {
            var replayBytes = ReplayParserTestResources.NortTA_vs_Demolitionmech;
            _service.SaveReplay(new MemoryStream(replayBytes), Guid.NewGuid().ToString());
        }

        [Test]
        public void ReplaySaveTest09()
        {
            var replayBytes = ReplayParserTestResources.north_vs_duro;
            _service.SaveReplay(new MemoryStream(replayBytes), Guid.NewGuid().ToString());
        }
    }
}