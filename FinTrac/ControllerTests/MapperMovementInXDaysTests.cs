using System.Diagnostics.CodeAnalysis;
using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.Report_Components;
using BusinessLogic.User_Components;
using Controller;
using DataManagers;
using Controller.Mappers;
using Mappers;

namespace ControllerTests
{
    [TestClass]
    public class MapperMovementInXDaysTests
    {
        #region Initialize

        private GenericController _controller;
        private SqlContext _testDb;
        private readonly IAppContextFactory _contextFactory = new InMemoryAppContextFactory();

        private UserRepositorySql _userRepo;


        [TestInitialize]
        public void Initialize()
        {
            _testDb = _contextFactory.CreateDbContext();
            _userRepo = new UserRepositorySql(_testDb);
            _controller = new GenericController(_userRepo);
        }

        #endregion

        #region Cleanup

        [TestCleanup]
        public void CleanUp()
        {
            _testDb.Database.EnsureDeleted();
        }

        #endregion

        #region ToMovement

        [TestMethod]
        public void GivenMovementInXDaysDTO_ShouldBePossibleToConvertItToMovementInXDays()
        {
            RangeOfDatesDTO rangeOfDatesDto =
                new RangeOfDatesDTO(new DateTime(2023, 12, 1), new DateTime(2023, 12, 31));

            MovementInXDaysDTO movementsDTO = new MovementInXDaysDTO(rangeOfDatesDto);

            MovementInXDays movements = MapperMovementInXDays.ToMovement(movementsDTO);

            Assert.AreEqual(movementsDTO.RangeOfDates.InitialDate, movements.RangeOfDates.InitialDate);
            Assert.AreEqual(movementsDTO.RangeOfDates.FinalDate, movements.RangeOfDates.FinalDate);
        }

        #endregion

        #region ToMovementDTO

        [TestMethod]
        public void GivenMovementInXDays_ShouldBePossibleToConvertItToMovementInXDaysDTO()
        {
            RangeOfDates rangeOfDatesDto =
                new RangeOfDates(new DateTime(2023, 12, 1), new DateTime(2023, 12, 31));

            MovementInXDays movements = new MovementInXDays(rangeOfDatesDto);

            MovementInXDaysDTO movementsDTO = MapperMovementInXDays.ToMovementDTO(movements);

            Assert.AreEqual(movements.RangeOfDates.InitialDate, movementsDTO.RangeOfDates.InitialDate);
            Assert.AreEqual(movements.RangeOfDates.FinalDate, movementsDTO.RangeOfDates.FinalDate);
        }

        #endregion
    }
}