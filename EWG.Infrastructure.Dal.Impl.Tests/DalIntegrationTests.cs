using System;
using System.Collections.Generic;
using System.Linq;
using EWG.Domain.Entities;
using EWG.Domain.Entities.Dictionaries;
using EWG.Domain.Entities.Security;
using NUnit.Framework;

namespace EWG.Infrastructure.Dal.Impl.Tests
{
    [TestFixture]
    public class DalIntegrationTests
    {
        private const string ConfigurationFile = "test.hibernate.cfg.xml";

        private IUnitOfWork _uow;
        private IUnitOfWorkFactory _factory;

        private IDbInitializer _dbInitializer;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _factory = new NhUnitOfWorkFactory(ConfigurationFile);

            _dbInitializer = new DbInitializer();

            //return;

            _dbInitializer.Initialize(ConfigurationFile);
        }

        [SetUp]
        public void SetUp()
        {
            _uow = _factory.CreateUnitOfWork();
        }

        [TearDown]
        public void TearDown()
        {
            _uow.Dispose();
            _uow = null;
        }

        [Test]
        public void GameMapTest()
        {
            GenericDicItemTest<GameMap>();
        }

        [Test]
        public void GameModeTest()
        {
            GenericDicItemTest<GameMode>();
        }

        [Test]
        public void GameTypeTest()
        {
            GenericDicItemTest<GameType>();
        }

        [Test]
        public void VictoryConditionTest()
        {
            GenericDicItemTest<VictoryCondition>();
        }

        [Test]
        public void ReplayTest()
        {
            GameMapTest();
            GameModeTest();
            GameTypeTest();
            VictoryConditionTest();

            var random = new Random((int) DateTime.Now.Ticks);

            var newReplay = new Replay
            {
                AI = true,
                Title = GenerateRandomString(80),
                AllowObservers = true,
                DateConstraint = GenerateRandomString(12),
                GameMap = GetFirst<GameMap>(),
                GameMode = GetFirst<GameMode>(),
                GameType = GetFirst<GameType>(),
                IncomeRate = random.Next(100, 3000),
                InitMoney = random.Next(100, 3000),
                IsNetworkMode = true,
                MaxPlayers = 32,
                NationConstraint = GenerateRandomString(12),
                Private = true,
                ScoreLimit = random.Next(100, 3000),
                Seed = GenerateRandomString(30),
                ServerName = GenerateRandomString(30),
                ThematicConstraint = GenerateRandomString(15),
                UploadDate = GetRandomDateTime(random),
                Version = GenerateRandomString(12),
                VictoryCondition = GetFirst<VictoryCondition>(),
                Link = GenerateRandomString(255),
                FileHash = Guid.NewGuid(),
                DownloadsCounter = random.Next()
            };

            var repository = new CrudRepository<Replay>(_uow);
            repository.Save(newReplay);

            var savedReplay = repository.GetById(newReplay.Id);

            Assert.NotNull(savedReplay);
            Assert.AreEqual(newReplay.AI, savedReplay.AI);
            Assert.AreEqual(newReplay.AllowObservers, savedReplay.AllowObservers);
            Assert.AreEqual(newReplay.DateConstraint, savedReplay.DateConstraint);
            Assert.AreEqual(newReplay.GameMap.Id, savedReplay.GameMap.Id);
            Assert.AreEqual(newReplay.GameMode.Id, savedReplay.GameMode.Id);
            Assert.AreEqual(newReplay.GameType.Id, savedReplay.GameType.Id  );
            Assert.AreEqual(newReplay.IncomeRate, savedReplay.IncomeRate);
            Assert.AreEqual(newReplay.InitMoney, savedReplay.InitMoney);
            Assert.AreEqual(newReplay.IsNetworkMode, savedReplay.IsNetworkMode);
            Assert.AreEqual(newReplay.MaxPlayers, savedReplay.MaxPlayers);
            Assert.AreEqual(newReplay.NationConstraint, savedReplay.NationConstraint);
            Assert.AreEqual(newReplay.Private, savedReplay.Private);
            Assert.AreEqual(newReplay.ScoreLimit, savedReplay.ScoreLimit);
            Assert.AreEqual(newReplay.Seed, savedReplay.Seed);
            Assert.AreEqual(newReplay.ServerName, savedReplay.ServerName);
            Assert.AreEqual(newReplay.ThematicConstraint, savedReplay.ThematicConstraint);
            Assert.AreEqual(newReplay.UploadDate, savedReplay.UploadDate);
            Assert.AreEqual(newReplay.Version, savedReplay.Version);
            Assert.AreEqual(newReplay.VictoryCondition.Id, savedReplay.VictoryCondition.Id);
            Assert.AreEqual(newReplay.Link, savedReplay.Link);
            Assert.AreEqual(newReplay.FileHash, savedReplay.FileHash);
            Assert.AreEqual(newReplay.DownloadsCounter, savedReplay.DownloadsCounter);
        }

        [Test]
        public void PlayerUserTest()
        {
            var random = new Random((int)DateTime.Now.Ticks);

            var newItem = new PlayerUser
            {
                EugenUserId = random.Next(1000000, 10000000),
                Name = GenerateRandomString(20)
            };

            var repository = new CrudRepository<PlayerUser>(_uow);
            repository.Save(newItem);

            var savedItem = repository.GetById(newItem.Id);

            Assert.NotNull(savedItem);
            Assert.AreEqual(newItem.EugenUserId, savedItem.EugenUserId);
            Assert.AreEqual(newItem.Name, savedItem.Name);
        }

        [Test]
        public void PlayerTest()
        {
            PlayerUserTest();
            ReplayTest();

            var random = new Random((int)DateTime.Now.Ticks);

            var firstReplay = GetFirst<Replay>();

            var newItem = new Player
            {
                PlayerUser = GetFirst<PlayerUser>(),
                PlayerAlliance = random.Next(1, 20),
                PlayerAvatar = GenerateRandomString(255),
                PlayerDeckContent = GenerateRandomString(80),
                PlayerDeckName = GenerateRandomString(25),
                PlayerElo = random.Next(10000, 20000)/100.0,
                PlayerIALevel = GenerateRandomString(20),
                PlayerIncomeRate = random.Next(1, 2000),
                PlayerIsEnteredInLobby = true,
                PlayerLevel = random.Next(1, 2000),
                PlayerName = GenerateRandomString(20),
                PlayerNumber = random.Next(1, 100),
                PlayerRank = random.Next(1, 20000),
                PlayerReady = true,
                PlayerScoreLimit = random.Next(1, 20000),
                PlayerTeamName = GenerateRandomString(40)
            };
            firstReplay.Players = new List<Player> {newItem};
            newItem.Replay = firstReplay;

            var replayRepository = new CrudRepository<Replay>(_uow);
            replayRepository.Save(firstReplay);

            var repository = new CrudRepository<Player>(_uow);
            repository.Save(newItem);

            var savedReplay = replayRepository.GetById(firstReplay.Id);
            Assert.IsNotEmpty(savedReplay.Players);

            Assert.AreEqual(newItem.Id, savedReplay.Players.First().Id);

            var savedItem = repository.GetById(newItem.Id);

            Assert.NotNull(savedItem);
            Assert.AreEqual(newItem.PlayerAlliance, savedItem.PlayerAlliance);
            Assert.AreEqual(newItem.PlayerAvatar, savedItem.PlayerAvatar);
            Assert.AreEqual(newItem.PlayerDeckContent, savedItem.PlayerDeckContent);
            Assert.AreEqual(newItem.PlayerDeckName, savedItem.PlayerDeckName);
            Assert.AreEqual(newItem.PlayerElo, savedItem.PlayerElo);
            Assert.AreEqual(newItem.PlayerIALevel, savedItem.PlayerIALevel);
            Assert.AreEqual(newItem.PlayerIncomeRate, savedItem.PlayerIncomeRate);
            Assert.AreEqual(newItem.PlayerIsEnteredInLobby, savedItem.PlayerIsEnteredInLobby);
            Assert.AreEqual(newItem.PlayerLevel, savedItem.PlayerLevel);
            Assert.AreEqual(newItem.PlayerName, savedItem.PlayerName);
            Assert.AreEqual(newItem.PlayerNumber, savedItem.PlayerNumber);
            Assert.AreEqual(newItem.PlayerRank, savedItem.PlayerRank);
            Assert.AreEqual(newItem.PlayerReady, savedItem.PlayerReady);
            Assert.AreEqual(newItem.PlayerScoreLimit, savedItem.PlayerScoreLimit);
            Assert.AreEqual(newItem.PlayerTeamName, savedItem.PlayerTeamName);
            Assert.AreEqual(newItem.PlayerUser.Id, savedItem.PlayerUser.Id);
            Assert.AreEqual(newItem.Replay.Id, savedItem.Replay.Id);
        }

        [Test]
        public void UserTest()
        {
            var newItem = new IdentityUser
            {
                Username = GenerateRandomString(20),
                Email = GenerateRandomString(20),
                HashedPassword = GenerateRandomString(20)
            };

            var repository = new CrudRepository<IdentityUser>(_uow);
            repository.Save(newItem);

            var savedItem = repository.GetById(newItem.Id);

            Assert.NotNull(savedItem);
            Assert.AreEqual(newItem.Email, savedItem.Email);
            Assert.AreEqual(newItem.Username, savedItem.Username);
            Assert.AreEqual(newItem.HashedPassword, savedItem.HashedPassword);
        }

        private void GenericDicItemTest<T>() where T : GenericDictionaryItem, new()
        {
            var random = new Random((int) DateTime.Now.Ticks);

            var newItem = new T
            {
                DateStart = GetRandomDateTime(random),

                DateEnd = GetRandomDateTime(random),

                Name = GenerateRandomString(8),
                PublicCode = GenerateRandomString(8),
                ResourceName = GenerateRandomString(8)
            };

            var repository = new CrudRepository<T>(_uow);
            repository.Save(newItem);

            var savedItem = repository.GetById(newItem.Id);

            Assert.NotNull(savedItem);
            Assert.AreEqual(newItem.DateStart, savedItem.DateStart);
            Assert.AreEqual(newItem.DateEnd, savedItem.DateEnd);
            Assert.AreEqual(newItem.Name, savedItem.Name);
            Assert.AreEqual(newItem.PublicCode, savedItem.PublicCode);
            Assert.AreEqual(newItem.ResourceName, savedItem.ResourceName);
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random((int)DateTime.Now.Ticks);
            var result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return result;
        }

        private DateTime GetRandomDateTime(Random random)
        {
            return new DateTime(
                random.Next(1990, 2000),
                random.Next(1, 12),
                random.Next(1, 28),
                random.Next(1, 23),
                random.Next(1, 59),
                random.Next(1, 59));
        }

        private T GetFirst<T>() where T : EntityBase
        {
            var crud = new CrudRepository<T>(_uow);
            return crud.Get().FirstOrDefault();
        }
    }
}
