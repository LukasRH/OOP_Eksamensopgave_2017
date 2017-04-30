using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ExamClassLibary
{
    public delegate void LineSystemEvent(object sender, string command);

    public interface ILineSystemUI
    {
        void DisplayUserNotFound(string username);

        void DisplayProductNotFound(string product);

        void DisplayUserInfo(User user);

        void DisplayTooManyArgumentsError(string command);

        void DisplayAdminCommandNotFoundMessage(string adminCommand);

        void DisplayAdminCommandMessage(string message);

        void DisplayUserBuysProduct(BuyTransaction transaction);

        void DisplayUserBuysProduct(int count, BuyTransaction transaction);

        void Close();

        void DisplayInsufficientCash(User user, Product product);

        void DisplayGeneralError(string errorString);
        void Start();

        event LineSystemEvent CommandEntered;
    }
}
