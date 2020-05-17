using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using kchalupa.Wilderness.GBMapCompress.Models;

namespace kchalupa.Wilderness.GBMapCompress
{
  /// <summary>
  /// The main form.  Handles loading files and performing the compression.
  /// </summary>
  public partial class MainForm : Form
  {

    #region fields

    /// <summary>
    /// Input directory location.
    /// </summary>
    private string m_inputDirectory = string.Empty;

    /// <summary>
    /// Output file location.
    /// </summary>
    private string m_outputFile = string.Empty;

    #endregion

    #region construction

    /// <summary>
    /// Constructor.
    /// </summary>
    public MainForm()
    {
      InitializeComponent();
    } // MainForm()

    #endregion

    #region event handlers

    /// <summary>
    /// Handles the input file click to select the input file.
    /// </summary>
    private void OnInputFileClickHandler(object sender, EventArgs args)
    {
      FolderBrowserDialog dialog = new FolderBrowserDialog();
      if(dialog.ShowDialog() == DialogResult.OK)
      {
        m_inputDirectory = dialog.SelectedPath;
      }
    } // OnInputFileClickHandler( sender, args )


    /// <summary>
    /// Handles the output file click to select the output file.
    /// </summary>
    private void OnOutputFileClickHandler(object sender, EventArgs args)
    {
      SaveFileDialog dialog = new SaveFileDialog
      {
        FileName = "worldmap.c"
      };

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        m_outputFile = dialog.FileName;
      }
    } // OnOutputFileClickHandler(object sender, EventArgs args)


    /// <summary>
    /// Runs the task.
    /// </summary>
    private void OnStartClickHandler(object sender, EventArgs args)
    {
      List<string> sourceFiles = new List<string>();
      List<string> headerFiles = new List<string>();

      string[] inputFiles = Directory.GetFiles(m_inputDirectory, "*.c");
      foreach (string inputFile in inputFiles)
      {
        string sourceFile = string.Empty;
        string headerFile = string.Empty;

        if (!inputFile.ToLower().EndsWith("worldmap.c") && !inputFile.ToLower().EndsWith("worldmap.h"))
        {
          CompressMapFile(inputFile, out sourceFile, out headerFile);

          sourceFiles.Add(sourceFile);
          headerFiles.Add(headerFile);
        }
      }

      using (TextWriter writer = new StreamWriter(Path.Combine(Path.GetDirectoryName(m_outputFile), Path.GetFileNameWithoutExtension(m_outputFile) + ".c")))
      {
        foreach (string sourceFile in sourceFiles)
        {
          writer.WriteLine(sourceFile);
        }

        writer.Close();
      }

      using (TextWriter writer = new StreamWriter(Path.Combine(Path.GetDirectoryName(m_outputFile), Path.GetFileNameWithoutExtension(m_outputFile) + ".h")))
      {
        foreach (string headerFile in headerFiles)
        {
          writer.WriteLine(headerFile);
        }

        writer.Close();
      }
    } // OnStartClickHandler(object sender, EventArgs args)


    /// <summary>
    /// Compresses a single map file.
    /// </summary>
    /// <param name="inputFile">The file to read and process.</param>
    /// <param name="sourceFileContents">The contents of the compressed source file.</param>
    /// <param name="headerFileContents">The contents of the compressed header file.</param>
    private void CompressMapFile(string inputFile, out string sourceFileContents, out string headerFileContents)
    {
      DictionaryCompressionModel dictionaryModel = new DictionaryCompressionModel();

      // Get the tiles from the source file.
      int[] tiles = dictionaryModel.ReadFile(inputFile);
      dictionaryModel.BlockSize = 5;

      int bestBlockSize = 0;
      int bestSize = int.MaxValue;
      KeyValuePair<int[], int[]> bestEncoding = new KeyValuePair<int[], int[]>();

      foreach (int factor in FindFactors(dictionaryModel.MapWidth))
      {
        dictionaryModel.BlockSize = factor;
        KeyValuePair<int[], int[]> encoding = dictionaryModel.Compress(tiles);
        int size = encoding.Key.Length + encoding.Value.Length;

        if (size < bestSize)
        {
          bestBlockSize = factor;
          bestEncoding = encoding;
          bestSize = size;
        }
      }

      // Make sure that we set the block size back.
      dictionaryModel.BlockSize = bestBlockSize;

      // Write the output file.
      sourceFileContents = dictionaryModel.WriteSourceFile(bestEncoding, Path.GetFileNameWithoutExtension(inputFile));
      headerFileContents = dictionaryModel.WriteHeaderFile(bestEncoding, Path.GetFileNameWithoutExtension(inputFile));

      int errors = dictionaryModel.Validate(tiles, bestEncoding);
      if (errors > 0)
      {
        MessageBox.Show("Failed to validate compressed map against source.  Number of discrepencies: " + errors + "  Input file: " + inputFile);
      }

      if (bestSize >= tiles.Length)
      {
        MessageBox.Show("Size in bytes of compressed data meets or exceeds uncompressed size.  Size: " + bestSize + "  Input file: " + inputFile);
      }
    } // CompressFile( inputFile, sourceFileContents, headerFileContents )


    /// <summary>
    /// Find the factors of a given number.
    /// </summary>
    private int[] FindFactors(int number)
    {
      List<int> factors = new List<int>();
      for(int i = 1; i <= number; ++i)
      {
        if(((int)number / (int)i) * (int)i == number)
        {
          factors.Add(i);
        }
      }

      return factors.ToArray();
    } // FindFactors( number )

    #endregion

  } // class MainForm
} // namespace kchalupa.Wilderness.GBMapCompress
