using BusinessLogic;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;

using BusinessLogic.Category_Components;
using System.Net.Security;
using BusinessLogic.User_Components;

namespace TestProject1;

[TestClass]
public class CategoryTests
{
    #region initializingAspects
    private Category genericCategory;
    private User genericUser;

    [TestInitialize]
    public void TestInitialize()
    {
        string name = "Outcomes";
        StatusEnum status = (StatusEnum)1;
        TypeEnum type = (TypeEnum)1;


        genericCategory = new Category(name, status, type);

        string firstName = "Austin";
        string lastName = "Ford";
        string email = "austinFord@gmail.com";
        string password = "AustinF2003";
        string address = "NW 2nd Ave";

        genericUser = new User(firstName, lastName, email, password, address);
    }
    #endregion

    #region Validate Category

    [TestMethod]
    public void GivenCorrectName_ShouldReturnTrue()
    {
        Assert.AreEqual(true, genericCategory.ValidateCategory());
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateCategory))]
    public void GivenEmptyName_ShouldThrowException()
    {
        Category myCategory = new Category();
        myCategory.Name = "";
        myCategory.ValidateCategory();
    }

    [TestMethod]
    public void GivenCategory_ShouldReturnDate()
    {
        DateTime dateNow = DateTime.Now;
        string dateNowString = dateNow.ToString("dd/MM/yyyy");
        Assert.AreEqual(dateNowString, genericCategory.CreationDate);
    }

    [TestMethod]
    public void GivenStatus_ShouldBelongToStatusEnum()
    {
        bool belongsToEnum = Enum.IsDefined(typeof(StatusEnum), genericCategory.Status);
        Assert.IsTrue(belongsToEnum);
    }

    [TestMethod]
    public void GivenType_ShouldBelongToTypeEnum()
    {
        bool belongsToEnum = Enum.IsDefined(typeof(TypeEnum), genericCategory.Type);
        Assert.IsTrue(belongsToEnum);
    }

    [TestMethod]
    public void GivenCorrectValuesToCreateCategory_ShouldBeEqualToProperties()
    {
        string categoryName = "Food";
        StatusEnum categoryStatus = (StatusEnum)1;
        TypeEnum categoryType = (TypeEnum)1;
        Category myCategory = new Category(categoryName, categoryStatus, categoryType);

        Assert.AreEqual(categoryName, myCategory.Name);
        Assert.AreEqual(categoryStatus, myCategory.Status);
        Assert.AreEqual(categoryType, myCategory.Type);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateCategory))]
    public void GivenWrongValueToCreateCategory_ShouldThrowException()
    {
        string categoryName = "";
        StatusEnum categoryStatus = (StatusEnum)1;
        TypeEnum categoryType = (TypeEnum)1;
        Category myCategory = new Category(categoryName, categoryStatus, categoryType);
    }

    #endregion

    #region Category Management

    [TestMethod]
    public void GivenCorrectCategoryToAdd_ShouldAddCategory()
    {
        int numberOfCategoriesAddedBefore = genericUser.MyCategories.Count;
        genericUser.AddCategory(genericCategory);
        Assert.AreEqual(numberOfCategoriesAddedBefore + 1, genericUser.MyCategories.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionCategoryManagement))]
    public void GivenAlreadyRegisteredCategoryToAdd_ShouldThrowException()
    {
        genericUser.AddCategory(genericCategory);
        genericUser.AddCategory(genericCategory);
    }

    [TestMethod]
    public void GivenCategoryToAdd_ShouldAssignId()
    {
        genericUser.AddCategory(genericCategory);
        Assert.AreEqual(genericUser.MyCategories.Count, genericCategory.Id);
    }

    [TestMethod]
    public void GivenNothing_ShouldReturnList()
    {
        Assert.AreEqual(genericUser.MyCategories, genericUser.GetCategories());
    }

    [TestMethod]
    public void GivenCategoryToUpdate_ShouldBeModifiedCorrectly()
    {
        genericUser.AddCategory(genericCategory);
        string name2 = "Fooding";
        StatusEnum status2 = (StatusEnum)1;
        TypeEnum type2 = (TypeEnum)1;
        Category category2 = new Category(name2, status2, type2);
        category2.Id = genericCategory.Id;
        genericUser.ModifyCategory(category2);
        int indexOfCategoryToUpdate = (int)genericCategory.Id - 1;

        Assert.AreEqual(category2, genericUser.MyCategories[indexOfCategoryToUpdate]);
    }

    [TestMethod]
    public void GivenCategoryToDelete_ShouldDelete()
    {
        genericUser.AddCategory(genericCategory);
        int previousLength = genericUser.MyCategories.Count;
        genericUser.DeleteCategory(genericCategory);

        int lengthAfterDelete = previousLength - 1;
        int actualLength = genericUser.MyCategories.Count;

        Assert.AreEqual(actualLength, lengthAfterDelete);
    }

    #endregion
}