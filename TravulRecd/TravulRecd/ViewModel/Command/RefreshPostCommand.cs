using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TravulRecd.Model;

namespace TravulRecd.ViewModel.Command
{
    public class RefreshPostCommand: ICommand
    {
        private readonly HistoryViewModel historyViewModel;

        public RefreshPostCommand(HistoryViewModel historyViewModel)
        {
            this.historyViewModel = historyViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            var post = parameter as Post;
            historyViewModel.RefreshPosts();
        }
    }
}
