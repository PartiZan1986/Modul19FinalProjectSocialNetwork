using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views
{
    public class FriendAddingView
    {
        UserService userService;
        FriendService friendService;
        public FriendAddingView(UserService userService, FriendService friendService)
        {
            this.userService = userService;
            this.friendService = friendService;
        }

        public void Show(User user)
        {
            var friendAddingData = new FriendAddingData();

            Console.WriteLine("Введите почтовый адрес друга: ");
            friendAddingData.FriendEmail = Console.ReadLine();

            friendAddingData.UserId = user.Id;

            try
            {
                friendService.AddFriend(friendAddingData);

                SuccessMessage.Show("Друг успешно добавлен!");

                user = userService.FindById(user.Id);
            }

            catch (UserNotFoundException)
            {
                AlertMessage.Show("Друг с таким почтовым адресом не найден!");
            }

            catch (ArgumentNullException)
            {
                AlertMessage.Show("Введите корректный почтовый адрес!");
            }

            catch (Exception)
            {
                AlertMessage.Show("Произошла ошибка при добавлении друга!");
            }
        }
    }
}