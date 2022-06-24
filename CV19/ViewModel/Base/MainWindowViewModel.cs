using CV19.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModel.Base
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Заголовок окна
        private string _Title = "Анализ статистики CV19";
        /// <summary>/// Заголовок окна/// </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Status: string - Статус программы

        private string _Status = "Готово!";

        /// <summary>
        /// Статус программы
        /// </summary>
        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }

        #endregion


        #region Команды

        public ICommand CloseApplicationComand { get; }

        private void OnCloseApplicationComandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseApplicationComandExecute(object p) => true;
        #endregion
        public MainWindowViewModel()
        {
            CloseApplicationComand = new LambdaCommand(OnCloseApplicationComandExecuted, CanCloseApplicationComandExecute);

        }
    }
}
