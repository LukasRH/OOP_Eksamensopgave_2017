using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ExamClassLibary
{
    public class SystemController
    {
        private readonly ILineSystem _system;
        private readonly ILineSystemUI _ui;

        private readonly Dictionary<string, Action<string, int>> _adminCommands = new Dictionary<string, Action<string, int>>();

        public SystemController(ILineSystemUI ui, ILineSystem lineSystem)
        {
            this._ui = ui;
            this._system = lineSystem;

            _adminCommands.Add(":quit", (s, i) => ui.Close());
            _adminCommands.Add(":q", (s, i) => ui.Close());
            _adminCommands.Add(":activate", (s, productId) =>
            {
                Product product = _system.GetProductByID(productId);
                product.Active = true;
                ui.DisplayAdminCommandMessage($"Product {product.Name} is set to active");
            });
            _adminCommands.Add(":deactivate", (s, productId) =>
            {
                Product product = _system.GetProductByID(productId);
                product.Active = false;
                ui.DisplayAdminCommandMessage($"Product {product.Name} is set to deactive");
            });
            _adminCommands.Add(":crediton", (s, productId) =>
            {
                Product product = _system.GetProductByID(productId);
                product.CanBeBoughtOnCredit = true;
                ui.DisplayAdminCommandMessage($"Product {product.Name} can now be bought on credits");
            });
            _adminCommands.Add(":creditoff", (s, productId) =>
            {
                Product product = _system.GetProductByID(productId);
                product.CanBeBoughtOnCredit = false;
                ui.DisplayAdminCommandMessage($"Product {product.Name} cannot be bought on credit now");

            });
            _adminCommands.Add(":addcredits", (username, amount) =>
            {
                User user = _system.GetUserByUsername(username);
                _system.AddCreditsToAccount(user, amount);
                ui.DisplayAdminCommandMessage($"{amount}DDK was added to {user.Username}'s account");
            });

            this._ui.CommandEntered += Ui_CommandEntered;
        }

        private void Ui_CommandEntered(object sender, string command)
        {
            if (command.StartsWith(":"))
                PraseAdminCommand(command);
            else
                ParseCommand(command);
        }

        private void ParseCommand(string command)
        {
            var commandArguments = command.Split(' ');
            int productId;
            int amount;
            bool converted;

            switch (commandArguments.Length)
            {
                case 1:
                    UserInformation(commandArguments[0].Trim());
                    break;
                case 2:
                    converted = Int32.TryParse(commandArguments[1].Trim(), out productId);
                    if (converted)
                        BuyProduct(commandArguments[0].Trim(), Convert.ToInt32(commandArguments[1].Trim()));
                    else
                        _ui.DisplayGeneralError("Invalid product id");
                    break;
                case 3:
                    converted = Int32.TryParse(commandArguments[1].Trim(), out amount) &&
                                Int32.TryParse(commandArguments[2].Trim(), out productId);
                    if (converted)
                        BuyProduct(commandArguments[0].Trim(), Convert.ToInt32(commandArguments[1].Trim()), Convert.ToInt32(commandArguments[2].Trim()));
                    else
                        _ui.DisplayGeneralError("invalid amount or product id");
                    break;
                default:
                    _ui.DisplayTooManyArgumentsError(command);
                    break;
            }
        }

        private void PraseAdminCommand(string command)
        {
            var commandArgumets = command.Split(' ').ToList();
            try
            {
                switch (commandArgumets.Count)
                {
                    case 1:
                        _adminCommands[commandArgumets[0]](String.Empty, 0);
                        break;
                    case 2:
                        _adminCommands[commandArgumets[0]](string.Empty, Convert.ToInt32(commandArgumets[1]));
                        break;
                    case 3:
                        _adminCommands[commandArgumets[0]](commandArgumets[1], Convert.ToInt32(commandArgumets[2]));
                        break;
                }
            }
            catch (KeyNotFoundException)
            {
                _ui.DisplayAdminCommandNotFoundMessage(command);
            }
            catch (UserNotFoundException ex)
            {
                _ui.DisplayUserNotFound(ex.Username);
            }
            catch (ProductNotFoundException ex)
            {
                _ui.DisplayProductNotFound(ex.ProductId.ToString());
            }
        }

        private void UserInformation(string username)
        {
            try
            {
                User user = _system.GetUserByUsername(username);
                _ui.DisplayUserInfo(user);
            }
            catch (UserNotFoundException ex)
            {
                _ui.DisplayUserNotFound(ex.Username);
            }
        }

        private void BuyProduct(string username, int productId)
        {
            try
            {
                User user = _system.GetUserByUsername(username);
                Product product = _system.GetProductByID(productId);

                _ui.DisplayUserBuysProduct(_system.BuyProduct(user, product));

            }
            catch (UserNotFoundException ex)
            {
                _ui.DisplayUserNotFound(ex.Username);
            }
            catch (ProductNotFoundException ex)
            {
                _ui.DisplayProductNotFound(ex.ProductId.ToString());
            }
            catch (InsufficientCreditsException ex)
            {
                _ui.DisplayInsufficientCash(ex.User, ex.Product);
            }
            catch (DeactivatedProductExcetion ex)
            {
                _ui.DisplayGeneralError($"Product {ex.Product.Name} is not active");
            }

        }

        private void BuyProduct(string username, int amount, int productId)
        {
            try
            {
                User user = _system.GetUserByUsername(username);
                Product product = _system.GetProductByID(productId);
                BuyTransaction lastTransaction = null;
                if (product.Active)
                {
                    if (user.Balance - product.Price * amount >= 0)
                    {
                        for (int i = 0; i < amount; i++)
                        {
                            lastTransaction = _system.BuyProduct(user, product);
                        }
                        _ui.DisplayUserBuysProduct(amount, lastTransaction);
                    }
                    else
                        _ui.DisplayInsufficientCash(user, product);
                }
                else
                    _ui.DisplayGeneralError("Product is not active");

            }
            catch (UserNotFoundException ex)
            {
                _ui.DisplayUserNotFound(ex.Username);
            }
            catch (ProductNotFoundException ex)
            {
                _ui.DisplayProductNotFound(ex.ProductId.ToString());
            }
        }
    }
}
