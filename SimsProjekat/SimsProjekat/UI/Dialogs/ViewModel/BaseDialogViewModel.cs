using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimsProjekat.CompositeCommon;
using SimsProjekat.CompositeCommon.Enums;
using SimsProjekat.Model;
using System.Collections.ObjectModel;
using System.Windows;
using SimsProjekat.UI.Dialogs.View;
using System.Web.UI.WebControls;

namespace SimsProjekat.UI.Dialogs.ViewModel
{
    public abstract class BaseDialogViewModel : ViewmodelBase
    {
        #region Fields

        private Type type;
        private string search = string.Empty;

        private RelayCommand addCommand;
        private RelayCommand findCommand;
        private RelayCommand deleteCommand;
        private RelayCommand editCommand;
        private RelayCommand okCommand;
        private RelayCommand cancelCommand;
        private RelayCommand exitCommand;
        private DialogState dialogState;
        protected Entity selectedItem;
        protected Entity oldItem;
        protected Window dialog;

      
        private ObservableCollection<Entity> items;

        #endregion Fields

        #region Constructors

        public BaseDialogViewModel(Window dialog, Type type)
        {
            this.dialog = dialog;
            this.type = type;
            dialogState = DialogState.View;
            items = new ObservableCollection<Entity>();

        }

        #endregion Constructors

        #region Command Properties

    

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(param => this.AddCommandExecute(), param => this.CanAddCommandExecute()));
            }
        }

        public RelayCommand OkCommand
        {
            get
            {
                return okCommand ?? (okCommand = new RelayCommand(param => this.OkCommandExecute(), param => this.CanOkCommandExecute()));
            }
        }

        public RelayCommand FindCommand
        {
            get
            {
                return findCommand ?? (findCommand = new RelayCommand(param => this.FindCommandExecute(), param => this.CanFindCommandExecute()));
            }
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(param => this.DeleteCommandExecute(), param => this.CanDeleteCommandExecute()));
            }
        }

        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ?? (editCommand = new RelayCommand(param => this.EditCommandExecute(), param => this.CanEditCommandExecute()));
            }
        }

        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new RelayCommand(param => this.CancelCommandExecute(), param => this.CanCancelCommandExecute()));
            }
        }

        public RelayCommand ExitCommand
        {
            get
            {
                return exitCommand ?? (exitCommand = new RelayCommand(param => this.ExitCommandExecute(), param => this.CanExitCommandExecute()));
            }
        }

        #endregion Command Properties

        #region Properties

        public string Search
        {
            get { return search; }
            set
            {
                bool doSearch = true;

                if (search.Length == 0 && value.Length == 0)
                {
                    doSearch = false;
                }

                search = value;
                OnPropertyChanged(nameof(Search));

                if (!doSearch)
                {
                    return;
                }

               DoSearch();
            }
        }
        public ObservableCollection<Entity> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public virtual Entity SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }


        public DialogState DialogState
        { 


            get { return dialogState; }
            set
               {
                if (dialogState == value)
                {
                    return ;
                }

                dialogState = value;
                OnPropertyChanged(nameof(DialogState));
            }
        }

         #endregion Properties

      public Entity GetInstance()
{
    return (Entity)Activator.CreateInstance(this.type);
}

#region Command Methods


      
        protected virtual void AddCommandExecute()
        {
            SelectedItem = GetInstance();

            DialogState = DialogState.Add;
         }

protected virtual bool CanAddCommandExecute()
{
    return dialogState == DialogState.View;
}

protected virtual void DeleteCommandExecute()
{
    MessageBoxResult result = MessageBox.Show("Do you want to delete this item", "Delete comfirm!", MessageBoxButton.YesNo);

    if (result == MessageBoxResult.No)
    {
        return;
    }

    if (DatabaseRemove(SelectedItem))
    {
        Items.Remove(SelectedItem);
    }
}

protected virtual bool DatabaseRemove(Entity item)
{
    return true;
}

protected virtual bool CanDeleteCommandExecute()
{
    return SelectedItem != null && DialogState == DialogState.View;
}

protected virtual void EditCommandExecute()
{
    DialogState = DialogState.Edit;
}

protected virtual bool CanEditCommandExecute()
{
    return SelectedItem != null && dialogState == DialogState.View;
}

protected virtual void FindCommandExecute()
{
    DialogState = DialogState.Find;
}

protected virtual bool CanFindCommandExecute()
{
    return SelectedItem != null && dialogState == DialogState.View;
}

protected virtual void OkCommandExecute()
{
    switch (DialogState)
    {
                case DialogState.Add:
            OkAfterAdd();
            break;
                case DialogState.Edit:
            OkAfterEdit();
            break;
    }

    DialogState = DialogState.View;

    if (SelectedItem == null)
    {
        return;
    }

    SelectedItem = null;
}


protected virtual bool CanOkCommandExecute()
{
    return DialogState != DialogState.View;
}

protected virtual void CancelCommandExecute()
{
    if (DialogState == DialogState.Edit)
    {
    }

    DialogState = DialogState.View;
    Init();
}

protected virtual bool CanCancelCommandExecute()
{
    return DialogState != DialogState.View;
}

protected virtual void ExitCommandExecute()
{
    DialogState = DialogState.View;
    //dialog.Close();
}

protected virtual bool CanExitCommandExecute()
{
    return DialogState == DialogState.View;
}

#endregion

      protected virtual void DoSearch()
       {

       }

protected virtual bool OkAfterAdd()
{

    if (OkAfterAddDatabase() == null)
    {
        return false;
    }

    Items.Add(SelectedItem);
    return true;
}

protected virtual bool OkAfterEdit()
{
    if (OkAfterEditDatabase() == null)
    {
        return false;
    }

    //todo

    return true;
}

protected virtual Entity OkAfterAddDatabase()
{
    return null;
}

protected virtual Entity OkAfterEditDatabase()
{
    return null;
}

#region Abstract Methods

protected abstract void Init();

#endregion AbstractMethods

    }
}
