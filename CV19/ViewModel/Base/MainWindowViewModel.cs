using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
