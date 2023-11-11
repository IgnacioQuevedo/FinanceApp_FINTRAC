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
    public class ControllerCategoryTests
    {
        #region Initialize

        private GenericController _controller;
        private SqlContext _testDb;
        private readonly IAppContextFactory _contextFactory = new InMemoryAppContextFactory();

        private UserRepositorySql _userRepo;
        private UserDTO _userConnected;

        private CategoryDTO categoryDTO;

        [TestInitialize]
        public void Initialize()
        {
            _testDb = _contextFactory.CreateDbContext();
            _userRepo = new UserRepositorySql(_testDb);
            _controller = new GenericController(_userRepo);

            _userConnected = new UserDTO("Jhon", "Sans", "jhonnie@gmail.com", "Jhoooniee123!", "");
            _userConnected.UserId = 1;

            categoryDTO = new CategoryDTO("Food", StatusEnum.Enabled, TypeEnum.Income, 1);
            categoryDTO.CategoryId = 1;

            _controller.RegisterUser(_userConnected);
            _controller.SetUserConnected(_userConnected.UserId);
        }

        #endregion

        #region Cleanup

        [TestCleanup]
        public void CleanUp()
        {
            _testDb.Database.EnsureDeleted();
        }

        #endregion

        #region Create Category

        [TestMethod]
        public void CreateCategoryMethodWithCorrectData_ShoudlAddCategoryToDb()
        {
            CategoryDTO dtoToAdd = new CategoryDTO("Food", StatusEnum.Enabled, TypeEnum.Income, _userConnected.UserId);
            CategoryDTO dtoToAdd2 =
                new CategoryDTO("Party", StatusEnum.Enabled, TypeEnum.Income, _userConnected.UserId);
            Category categoryInDb = new Category();

            _controller.CreateCategory(dtoToAdd);
            _controller.CreateCategory(dtoToAdd2);

            categoryInDb = _testDb.Users.First().MyCategories.First();

            Assert.IsNotNull(categoryInDb.CategoryUser);
            Assert.AreEqual(dtoToAdd.UserId, categoryInDb.UserId);
            Assert.AreEqual(dtoToAdd.Name, categoryInDb.Name);
            Assert.AreEqual(dtoToAdd.Status, categoryInDb.Status);
            Assert.AreEqual(dtoToAdd.Type, categoryInDb.Type);
            Assert.AreEqual(2, _testDb.Users.First().MyCategories.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateCategoryMethodWithIncorrectData_ShouldThrowException()
        {
            CategoryDTO dtoToAdd = new CategoryDTO("", StatusEnum.Enabled, TypeEnum.Income, _userConnected.UserId);

            _controller.CreateCategory(dtoToAdd);
        }

        #endregion

        #region Find Category

        [TestMethod]
        public void GivenCategoryDTO_ShouldBePossibleToFindItOnDb()
        {
            _controller.CreateCategory(categoryDTO);
            Category categoryFound = _controller.FindCategoryInDb(categoryDTO.CategoryId);

            Assert.AreEqual(categoryDTO.CategoryId, categoryFound.CategoryId);
            Assert.AreEqual(categoryDTO.UserId, categoryFound.UserId);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GivenCategoryDTOThatIsNotInDb_WhenTryingToFound_ShouldThrowException()
        {
            _controller.FindCategory(categoryDTO.CategoryId);
        }

        [TestMethod]
        public void GivenCategoryDTO_ShouldReturnCategoryDTO_OnDb()
        {
            _controller.CreateCategory(categoryDTO);
            CategoryDTO categoryFound = _controller.FindCategory(categoryDTO.CategoryId);

            Assert.AreEqual(categoryDTO.CategoryId, 0);
            Assert.AreEqual(categoryDTO.UserId, categoryFound.UserId);
        }


        #endregion

        #region Update Category

        [TestMethod]
        public void GivenCategoryDTOToUpdate_ShouldBeUpdatedInDb()
        {
            _controller.CreateCategory(categoryDTO);

            CategoryDTO categoryDtoWithUpdates = new CategoryDTO("Party", StatusEnum.Disabled, TypeEnum.Outcome, 1);
            categoryDtoWithUpdates.CategoryId = 1;

            _controller.UpdateCategory(categoryDtoWithUpdates);

            Category categoryInDbWithSupossedChanges = _controller.FindCategoryInDb(categoryDTO.CategoryId);

            Assert.AreEqual(categoryInDbWithSupossedChanges.CategoryId, categoryDtoWithUpdates.CategoryId);
            Assert.AreEqual(categoryInDbWithSupossedChanges.UserId, categoryDtoWithUpdates.UserId);
            Assert.AreEqual(categoryInDbWithSupossedChanges.Name, categoryDtoWithUpdates.Name);
            Assert.AreEqual(categoryInDbWithSupossedChanges.Status, categoryDtoWithUpdates.Status);
            Assert.AreEqual(categoryInDbWithSupossedChanges.Type, categoryDtoWithUpdates.Type);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TryingToUpdateWithoutAnyChanges_ShouldThrowException()
        {
            CategoryDTO categoryRegistered =
                new CategoryDTO("Food", StatusEnum.Enabled, TypeEnum.Income, _userConnected.UserId);
            categoryRegistered.CategoryId = 1;

            _controller.CreateCategory(categoryRegistered);

            CategoryDTO categoryWithoutUpdates =
                new CategoryDTO("Food", StatusEnum.Enabled, TypeEnum.Income, _userConnected.UserId);
            categoryWithoutUpdates.CategoryId = categoryRegistered.CategoryId;
            _controller.UpdateCategory(categoryWithoutUpdates);
        }

        #endregion

        #region Delete Category

        [TestMethod]
        public void GivenACategoryDTOToDelete_ShouldBeDeletedOnDb()
        {
            CategoryDTO categoryDTO2 =
                new CategoryDTO("Party", StatusEnum.Enabled, TypeEnum.Income, _userConnected.UserId);
            categoryDTO2.CategoryId = 2;

            _controller.CreateCategory(categoryDTO);
            _controller.CreateCategory(categoryDTO2);
            List<Category> categoryListsOfUser = _testDb.Users.First().MyCategories;

            int amountOfCategoriesInDb = categoryListsOfUser.Count;
            _controller.DeleteCategory(categoryDTO.CategoryId);
            _controller.DeleteCategory(categoryDTO2.CategoryId);

            int amountOfCategoriesPostDelete = _testDb.Users.First().MyCategories.Count;

            Assert.AreEqual(amountOfCategoriesInDb - 2, amountOfCategoriesPostDelete);
        }

        // [TestMethod]
        // [ExpectedException(typeof(Exception))]
        // public void GivenACategoryAssignedToATransaction_ShouldThrowException()
        // {
        //     //We will implement it in a near future when create methods of account and transaction are done.
        // }

        #endregion

        #region Receive Category List

        [TestMethod]
        public void ReceiveCategoryListMethod_ShouldReturnCategoryList()
        {
            CategoryDTO category2 =
                new CategoryDTO("Party", StatusEnum.Enabled, TypeEnum.Income, _userConnected.UserId);
            CategoryDTO category3 = new CategoryDTO("Gym", StatusEnum.Enabled, TypeEnum.Outcome, _userConnected.UserId);

            _controller.CreateCategory(categoryDTO);
            _controller.CreateCategory(category2);
            _controller.CreateCategory(category3);

            List<Category> allCategories = new List<Category>();

            allCategories = _controller.ReceiveCategoryListFromUser(_userConnected.UserId);

            Assert.AreEqual(3, allCategories.Count);
        }

        [TestMethod]
        public void ReceiveCategoryListMethod_WhenListHasNoCategories_ShouldNotReturnNull()
        {
            Assert.IsNotNull(_controller.ReceiveCategoryListFromUser(_userConnected.UserId));
        }

        #endregion

        #region Get All Categories

        [TestMethod]
        public void GetAllCategoriesMethod_ShouldReturnCategoryDTOList()
        {
            CategoryDTO category2 =
                new CategoryDTO("Party", StatusEnum.Enabled, TypeEnum.Income, _userConnected.UserId);
            CategoryDTO category3 = new CategoryDTO("Gym", StatusEnum.Enabled, TypeEnum.Outcome, _userConnected.UserId);

            _controller.CreateCategory(categoryDTO);
            _controller.CreateCategory(category2);
            _controller.CreateCategory(category3);

            List<CategoryDTO> allCategories = new List<CategoryDTO>();

            allCategories = _controller.GetAllCategories(_userConnected.UserId);

            Assert.AreEqual(3, allCategories.Count);
        }

        [TestMethod]
        public void GetAllCategoriesMethod_WhenListHasNoCategories_ShouldNotReturnNull()
        {
            Assert.IsNotNull(_controller.GetAllCategories(_userConnected.UserId));
        }

        #endregion
    }
}