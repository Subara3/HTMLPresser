using System.Windows.Input;

namespace HTMLPresser.input
{

    /// <summary>
    /// UIコマンド定義
    /// </summary>
    public static class Commands
    {
        private static RoutedCommand _OutputBookListTag = new RoutedCommand("OutputBookListTag", typeof(Commands));
        /// <summary>
        /// OutputBookListTag
        /// </summary>
        public static RoutedCommand OutputBookListTag { get { return _OutputBookListTag; } }

        private static RoutedCommand _Refer = new RoutedCommand("Refer", typeof(Commands));
        /// <summary>
        /// Refer
        /// </summary>
        public static RoutedCommand Refer { get { return _Refer; } }
        
        private static RoutedCommand _AddBook = new RoutedCommand("AddBook", typeof(Commands));
        /// <summary>
        /// AddBook
        /// </summary>
        public static RoutedCommand AddBook { get { return _AddBook; } }

        private static RoutedCommand _DeleteBook = new RoutedCommand("DeleteBook", typeof(Commands));
        /// <summary>
        /// DeleteBook
        /// </summary>
        public static RoutedCommand DeleteBook { get { return _DeleteBook; } }
    }
}
