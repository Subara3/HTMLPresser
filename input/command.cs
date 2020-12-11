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
    }
}
