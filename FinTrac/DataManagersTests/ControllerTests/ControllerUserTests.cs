using BusinessLogic.Dto_Components;
using BusinessLogic.User_Components;
using DataManagers;


namespace DataManagersTests
{
    [TestClass]
    public class ControllerUserTests
    {

        #region Initialize
        
        private Controller _controller;
    
        [TestInitialize]

        public void Initialize()
        {
            _controller = new Controller();

        }
        
        #endregion

        #region To User
        
        [TestMethod]
        public void GivenUserDTO_ShouldBePossibleToConvertItToUser()
        {
            UserDTO userDto = new UserDTO("Jhon", "Sans", "jhonnie@gmail.com", "Jhoooniee123", "");

            User userConverted = _controller.ToUser(userDto);
            
            Assert.AreEqual(userDto.FirstName,userConverted.FirstName);
            Assert.AreEqual(userDto.LastName,userConverted.LastName);
            Assert.AreEqual(userDto.Email,userConverted.Email);
            Assert.AreEqual(userDto.Password,userConverted.Password);
            Assert.AreEqual(userDto.Address,userConverted.Address);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionController))]
        public void GivenUserDTOWithIncorrectData_ShoulThrowException()
        {
            UserDTO userDto = new UserDTO("Jhon", "Sans", "", "Jhoooniee123", "");
            User userConverted = _controller.ToUser(userDto);
        }
        
        #endregion

        #region To UserDTO

        [TestMethod]
        public void GivenUser_ShouldBePossibleToConvertItToUserDTO()
        {
            User userReceived = new User("Jhon", "Sans", "jhonnie@gmail.com", "Jhoooniee123", "");
            UserDTO userDto = _controller.ToDtoUser(userReceived);
            
            Assert.AreEqual(userDto.FirstName,userReceived.FirstName);
            Assert.AreEqual(userDto.LastName,userReceived.LastName);
            Assert.AreEqual(userDto.Email,userReceived.Email);
            Assert.AreEqual(userDto.Password,userReceived.Password);
            Assert.AreEqual(userDto.Address,userReceived.Address);
        }

        #endregion
      
        
    }
}