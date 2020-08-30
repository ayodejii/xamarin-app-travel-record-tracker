using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TravulRecd.Model;

namespace TravulRecd.ViewModel.Command
{
    public class SavePostCommand : ICommand
    {
        private readonly NewTravelViewModel newTravelPageViewModel;

        public SavePostCommand(NewTravelViewModel newTravelPageViewModel)
        {
            this.newTravelPageViewModel = newTravelPageViewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var post = parameter as Post;
            if (post != null)
            {
                if (string.IsNullOrEmpty(post.Experience))
                    return false;
                if (post.PostVenue == null)
                    return false;
                return true;
            }
            return false;
        }

        public void Execute(object parameter) => newTravelPageViewModel.savePost();
    }
}
