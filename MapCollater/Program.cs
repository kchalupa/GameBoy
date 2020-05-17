using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kchalupa.WIlderness.MapCollater
{
  /// <summary>
  /// Program's main entry point.
  /// </summary>
  static class Program
  {

    #region methods

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    } // Main()

    #endregion

  } // class Program
} // namespace kchalupa.WIlderness.MapCollater
