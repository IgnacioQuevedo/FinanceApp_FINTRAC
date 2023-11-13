using BusinessLogic.Account_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.Transaction_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.User_Components;
using Controller;
using Controller.Mappers;
using DataManagers;

namespace ControllerTests
{
    [TestClass]
    public class ControllerMonetaryAccountsTests
    {
        #region Initialize

        private GenericController _controller;
        private SqlContext _testDb;
        private readonly IAppContextFactory _contextFactory = new InMemoryAppContextFactory();

        private UserRepositorySql _userRepo;
        private UserDTO _userConnected;

        private MonetaryAccountDTO _monetToCreateDTO1;

        [TestInitialize]
        public void Initialize()
        {
            _testDb = _contextFactory.CreateDbContext();
            _userRepo = new UserRepositorySql(_testDb);
            _controller = new GenericController(_userRepo);

            _userConnected = new UserDTO("Jhon", "Sans", "jhonnie@gmail.com", "Jhoooniee123!", "");
            _userConnected.UserId = 1;

            _controller.RegisterUser(_userConnected);
            _controller.SetUserConnected(_userConnected.UserId);

            _monetToCreateDTO1 = new MonetaryAccountDTO("Brou", 1000, CurrencyEnumDTO.UY, DateTime.Now.Date, _userConnected.UserId);
            _monetToCreateDTO1.MonetaryAccountId = 1;
        }

        #endregion

        #region Cleanup

        [TestCleanup]
        public void CleanUp()
        {
            _testDb.Database.EnsureDeleted();
        }

        #endregion

        #region Create Monetary Account

        [TestMethod]
        public void GivenMonetaryAccountToCreate_ShouldBeCreated()
        {
            _monetToCreateDTO1 = new MonetaryAccountDTO("Brou", 1000, CurrencyEnumDTO.UY, DateTime.Now.Date, _userConnected.UserId);
            _monetToCreateDTO1.MonetaryAccountId = 1;

            MonetaryAccountDTO monetToCreateDTO2 = new MonetaryAccountDTO("Itau", 1300, CurrencyEnumDTO.USA, DateTime.Now.Date, _userConnected.UserId);
            monetToCreateDTO2.MonetaryAccountId = 2;

            _controller.CreateMonetaryAccount(_monetToCreateDTO1);
            _controller.CreateMonetaryAccount(monetToCreateDTO2);

            List<Account> myAccountsDb = _testDb.Users.First().MyAccounts;

            Assert.IsNotNull(myAccountsDb[0].AccountUser);
            Assert.AreEqual(myAccountsDb[0].UserId, _monetToCreateDTO1.UserId);
            Assert.AreEqual(myAccountsDb[0].Name, _monetToCreateDTO1.Name);
            Assert.AreEqual(myAccountsDb[0].AccountId, _monetToCreateDTO1.MonetaryAccountId);
            Assert.AreEqual(myAccountsDb[0].Currency, (CurrencyEnum)_monetToCreateDTO1.Currency);
            Assert.AreEqual(typeof(MonetaryAccount), myAccountsDb[0].GetType());
            Assert.IsInstanceOfType(myAccountsDb[0], typeof(MonetaryAccount));

            Assert.AreEqual(2, _testDb.Users.First().MyAccounts.Count);
        }

        #endregion


        #region Find Monetary Account

        [TestMethod]
        public void GivenMonetaryAccountIdAndUserId_ShouldReturnMonetaryAccountDTOFound_OnDb()
        {
            _controller.CreateMonetaryAccount(_monetToCreateDTO1);

            MonetaryAccountDTO monetAccountFound = _controller.FindMonetaryAccount(_monetToCreateDTO1.MonetaryAccountId, _userConnected.UserId);

            Assert.AreEqual(_monetToCreateDTO1.MonetaryAccountId, monetAccountFound.MonetaryAccountId);
            Assert.AreEqual(_monetToCreateDTO1.UserId, monetAccountFound.UserId);
        }

        [TestMethod]
        public void GivenMonetaryAccountDTO_ShouldReturnExactlyTheMonetaryAccountFound_OnDb()
        {
            _controller.CreateMonetaryAccount(_monetToCreateDTO1);

            MonetaryAccount monetFound = _controller.FindMonetaryAccountInDb(_monetToCreateDTO1);

            Assert.AreEqual(_monetToCreateDTO1.MonetaryAccountId, monetFound.AccountId);
            Assert.AreEqual(_monetToCreateDTO1.UserId, monetFound.UserId);
        }

        #endregion

        #region  Update Monetary Account

        [TestMethod]
        public void GivenMonetaryDTOToUpdate_ShouldBeUpdatedInDb()
        {
            _controller.CreateMonetaryAccount(_monetToCreateDTO1);

            MonetaryAccountDTO accountDTOWithUpdates = new MonetaryAccountDTO("Itau", 1300, CurrencyEnumDTO.USA, DateTime.Now.Date, _userConnected.UserId);
            accountDTOWithUpdates.MonetaryAccountId = 1;

            _controller.UpdateMonetaryAccount(accountDTOWithUpdates);

            MonetaryAccount accountInDbWithSupossedChanges = _controller.FindMonetaryAccountInDb(_monetToCreateDTO1);

            Assert.AreEqual(accountInDbWithSupossedChanges.AccountId, accountDTOWithUpdates.MonetaryAccountId);
            Assert.AreEqual(accountInDbWithSupossedChanges.Name, accountDTOWithUpdates.Name);
            Assert.AreEqual(accountInDbWithSupossedChanges.Amount, accountDTOWithUpdates.Amount);
            Assert.AreEqual(accountInDbWithSupossedChanges.Currency, (CurrencyEnum)accountDTOWithUpdates.Currency);
            Assert.AreEqual(accountInDbWithSupossedChanges.UserId, accountDTOWithUpdates.UserId);
        }

        #endregion
    }

}