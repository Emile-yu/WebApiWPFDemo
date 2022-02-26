using Caliburn.Micro;
using RetailWPFUserInterface.Library.Api;
using RetailWPFUserInterface.Library.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RetailWPFUserInterface.ViewModels
{
    public class UserDisplayViewModel : Screen
    {
        private BindingList<UserModel> _users;
        private IWindowManager _windowManager;
        private IUserEndpoint _userEndpoint;
        private StatusInfoViewModel _status;
        private UserModel _selectedUser;
        private string _selectedUserName;
        private string _selectedRoleToRemove;
        private string _selectedRoleToAdd;
        private BindingList<string> _selectedUserRoles;
        private BindingList<string> _availableRoles;

        public UserDisplayViewModel(IWindowManager windowManager, IUserEndpoint userEndpoint, StatusInfoViewModel status)
        {
            this._windowManager = windowManager;
            this._userEndpoint = userEndpoint;
            this._status = status;
            _users = new BindingList<UserModel>();

            _selectedUserRoles = new BindingList<string>();

            _availableRoles = new BindingList<string>();
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            try
            {
                await LoadUsers();
            }
            catch (Exception ex)
            {

                //controller can not do the ui staff
                //so we create a message box ui to handle it
                dynamic settings = new ExpandoObject();
                settings.WindowsStartUpLocation = WindowStartupLocation.CenterOwner;
                settings.ResizeMode = ResizeMode.NoResize;
                settings.Title = "System Error";

                if (ex.Message == "Unauthorized")
                {
                    _status.UpdateMessage("Unauthorized Access", "You do not have permission to interact with the Sales form");
                    _windowManager.ShowDialog(_status, null, settings);

                    //((IConductor)this.Parent).ActivateItem(IoC.Get<LoginViewModel>());
                }
                else
                {
                    _status.UpdateMessage("Fatal Exception", ex.Message);
                    _windowManager.ShowDialog(_status, null, settings);
                }

                TryClose();
            }
        }

        private async Task LoadUsers()
        {
            var userList = await _userEndpoint.GetAll();
            Users = new BindingList<UserModel>(userList);
        }
        private async Task LoadRoles()
        {
            var roleList = await _userEndpoint.GetAllRoles();
            foreach (var role in roleList)
            {
                if (SelectedUserRoles.IndexOf(role.Value) < 0)
                {
                    AvailableRoles.Add(role.Value);
                }
            }
        }


        public BindingList<UserModel> Users
        {
            get { return _users; }
            set 
            { 
                _users = value;
                NotifyOfPropertyChange(() => Users);
            }
        }

        public UserModel SelectedUser
        {
            get { return _selectedUser; }
            set 
            { 
                _selectedUser = value;
                SelectedUserName = _selectedUser.Email;
                SelectedUserRoles = new BindingList<string>(value.Roles.Values.ToList());
                LoadRoles();
                NotifyOfPropertyChange(() => SelectedUser);
                NotifyOfPropertyChange(() => SelectedUserName);
            }
        }

        public string SelectedUserName
        {
            get { return _selectedUserName; }
            set 
            {
                _selectedUserName = value;
                NotifyOfPropertyChange(() => SelectedUserName);
            }
        }
        public string SelectedRoleToRemove
        {
            get { return _selectedRoleToRemove; }
            set
            {
                _selectedRoleToRemove = value;
                NotifyOfPropertyChange(() => SelectedRoleToRemove);
                NotifyOfPropertyChange(() => CanRemoveSelectedRole);
            }
        }
        public string SelectedRoleToAdd
        {
            get { return _selectedRoleToAdd; }
            set 
            { 
                _selectedRoleToAdd = value;
                NotifyOfPropertyChange(() => SelectedRoleToAdd);
                NotifyOfPropertyChange(() => CanAddSelectedRole);
            }
        }


        public BindingList<string> SelectedUserRoles
        {
            get { return _selectedUserRoles; }
            set 
            { 
                _selectedUserRoles = value;
                NotifyOfPropertyChange(() => SelectedUserRoles);
            }
        }

        public BindingList<string> AvailableRoles
        {
            get { return _availableRoles; }
            set 
            { 
                _availableRoles = value;
                NotifyOfPropertyChange(() => AvailableRoles);
            }
        }

        public bool CanRemoveSelectedRole
        {
            get
            {
                bool output = false;

                if (!String.IsNullOrWhiteSpace(SelectedRoleToRemove))
                {
                    output = true;
                }
                return output;
            }
        }
        public async Task RemoveSelectedRole()
        {
            await _userEndpoint.RemoveRole(SelectedUser.Id, SelectedRoleToRemove);

            SelectedUserRoles.Remove(SelectedRoleToRemove);
            AvailableRoles.Add(SelectedRoleToRemove);
        }

        public bool CanAddSelectedRole
        {
            get
            {
                bool output = false;

                if (!String.IsNullOrWhiteSpace(SelectedRoleToAdd))
                {
                    output = true;
                }

                return output;
            }
        }

        public async Task AddSelectedRole()
        {
            await _userEndpoint.AddRole(SelectedUser.Id, SelectedRoleToAdd);

            SelectedUserRoles.Add(SelectedRoleToAdd);
            AvailableRoles.Remove(SelectedRoleToAdd);
        }


    }
}
