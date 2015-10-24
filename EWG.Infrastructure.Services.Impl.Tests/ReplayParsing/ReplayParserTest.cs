using System.IO;
using EWG.Infrastructure.Services.Impl.ReplayParsing;
using NUnit.Framework;

namespace EWG.Infrastructure.Services.Impl.Tests.ReplayParsing
{
    [TestFixture]
    public class ReplayParserTest
    {
        [Test]
        public void ParseTest01()
        {
            var replayParser = new ReplayParser();

            var memoryStream = new MemoryStream(ReplayParserTestResources.Steel_Balalaika_vs_We_suck_at_wargame_3vs3);

            var replayDto = replayParser.ParseFile(memoryStream);

            Assert.IsNotNull(replayDto);
            Assert.AreEqual(1, replayDto.IsNetworkMode);
            Assert.AreEqual("430000610", replayDto.Version);
            Assert.AreEqual("1", replayDto.GameMode);
            Assert.AreEqual(6, replayDto.NbMaxPlayer);
            Assert.AreEqual(0, replayDto.NbAI);
            Assert.AreEqual("0", replayDto.GameType);
            Assert.AreEqual(1, replayDto.Private);
            Assert.AreEqual(3000, replayDto.InitMoney);
            Assert.AreEqual(2400, replayDto.TimeLimit);
            Assert.AreEqual(500, replayDto.ScoreLimit);
            Assert.AreEqual("Игра (|:Serega:|)", replayDto.ServerName);
        }

        [Test]
        public void ParseTest02()
        {
            var replayParser = new ReplayParser();

            var memoryStream = new MemoryStream(ReplayParserTestResources.WRG_B5P_vs_1144_Tough_Jungle);

            var replayDto = replayParser.ParseFile(memoryStream);

            Assert.IsNotNull(replayDto);
            Assert.AreEqual(1, replayDto.IsNetworkMode);
            Assert.AreEqual("430000610", replayDto.Version);
        }

        [Test]
        public void ParseTest03()
        {
            var replayParser = new ReplayParser();

            var memoryStream = new MemoryStream(ReplayParserTestResources._3v3_Tourney_Viteska_Brigada_B5P);

            var replayDto = replayParser.ParseFile(memoryStream);

            Assert.IsNotNull(replayDto);
            Assert.AreEqual(1, replayDto.IsNetworkMode);
            Assert.AreEqual("430000610", replayDto.Version);
        }
    }
}
