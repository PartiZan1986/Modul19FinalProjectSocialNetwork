using Moq;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;

namespace SocialNetwork.Test
{
    [TestFixture]
    public class FriendServiceTests
    {
        private Mock<IUserRepository> mockUserRepository;
        private Mock<IFriendRepository> mockFriendRepository;
        private FriendService friendService;

        [SetUp]
        public void SetUp()
        {
            mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(r => r.FindByEmail("notexisting.user@example.com")).Returns((UserEntity)null!);

            mockFriendRepository = new Mock<IFriendRepository>();
            friendService = new FriendService(mockUserRepository.Object, mockFriendRepository.Object);
        }

        [Test]
        public void AddFriend_UserIdIsZero_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var friendAddingData = new FriendAddingData { UserId = 0, FriendEmail = "test@example.com" };

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => friendService.AddFriend(friendAddingData));
        }

        [Test]
        public void AddFriend_ThrowsUserNotFoundException()
        {
            // Arrange
            var friendAddingData = new FriendAddingData { UserId = 25, FriendEmail = "test@example.com" };

            // Act & Assert
            Assert.Throws<UserNotFoundException>(() => friendService.AddFriend(friendAddingData));
        }
    }
}
