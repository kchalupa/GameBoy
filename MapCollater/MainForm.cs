using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kchalupa.WIlderness.MapCollater
{
  /// <summary>
  /// The main form.
  /// </summary>
  public partial class MainForm : Form
  {

    #region fields

    /// <summary>
    /// The directory to process.
    /// </summary>
    private string m_directory = string.Empty;

    #endregion

    #region construction

    /// <summary>
    /// Constructor
    /// </summary>
    public MainForm()
    {
      InitializeComponent();
    } // MainForm()

    #endregion

    #region methods

    /// <summary>
    /// Handles the directory navigator's click event.
    /// </summary>
    /// <param name="sender">The origin of the event.</param>
    /// <param name="args">Arguments to take into account.</param>
    private void OnDirectoryNavigatorClickHandler(object sender, EventArgs args)
    {
      using (var dialog = new FolderBrowserDialog())
      {
        if(dialog.ShowDialog() == DialogResult.OK)
        {
          m_directory = dialog.SelectedPath;
        }
      }
    } // OnDirectoryNavigatorClickHandler( sender, args)



    /// <summary>
    /// Handles the start button being clicked to run the process.
    /// </summary>
    /// <param name="sender">The origin of the event.</param>
    /// <param name="args">Arguments to take into account.</param>
    private void OnStartClickHandler(object sender, EventArgs args)
    {
      string[] files = Directory.GetFiles(m_directory, m_searchPatternTextBox.Text);
      using (TextWriter writer = new StreamWriter(Path.Combine(m_directory, "..\\worldmap.h")))
      {
        writer.WriteLine("#ifndef __WORLDMAP_H");
        writer.WriteLine("#define __WORLDMAP_H");

        // Contents go here.
        foreach(string file in files.Where((p) => p.ToLower().EndsWith(".h")))
        {
          writer.WriteLine();
          using (TextReader reader = new StreamReader(file))
          {
            writer.Write(reader.ReadToEnd());
          }
          writer.WriteLine();
        }

        writer.WriteLine("#endif");
      }

      using (TextWriter writer = new StreamWriter(Path.Combine(m_directory, "..\\worldmap.c")))
      {
        foreach (string file in files.Where((p) => p.ToLower().EndsWith(".c")))
        {
          writer.WriteLine();
          using (TextReader reader = new StreamReader(file))
          {
            writer.Write(reader.ReadToEnd());
          }
          writer.WriteLine();
        }
      }
    } // OnStartClickHandler( sender, args )

    #endregion

  } // class MainForm
} // kchalupa.WIlderness.MapCollater
