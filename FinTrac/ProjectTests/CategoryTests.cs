using BusinessLogic;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;

using BusinessLogic.Category;
using System.Net.Security;

namespace TestProject1;

[TestClass]
public class CategoryTests
{
    private Category genericCategory;

    [TestInitialize]
    public void TestInitialize()
    {
        genericCategory = new Category();
        genericCategory.Name = "Clothes";
        genericCategory.Status = (StatusEnum)1;
        genericCategory.Type = TypeEnum.Income;

    }

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


}
