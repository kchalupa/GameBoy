using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kchalupa.Wilderness.GBMapCompress.Models
{
  /// <summary>
  /// Defines a compression model.
  /// </summary>
  public abstract class CompressionModel
  {

    #region fields

    /// <summary>
    /// The map's width.
    /// </summary>
    private int m_mapWidth = 0;

    /// <summary>
    /// The map's height.
    /// </summary>
    private int m_mapHeight = 0;

    #endregion

    #region properties

    /// <summary>
    /// Gets the map width.
    /// </summary>
    public int MapWidth
    {
      get { return m_mapWidth; }
    } // MapWidth


    /// <summary>
    /// Gets the map height.
    /// </summary>
    public int MapHeight
    {
    get { return m_mapHeight; }
    } // MapHeight

    #endregion

    #region methods

    /// <summary>
    /// Reads the file and builds the list of tiles.
    /// </summary>
    /// <summary>
    /// Read the input file for the tile indexes.
    /// </summary>
    /// <param name="inputFile"></param>
    public virtual int[] ReadFile(string inputFile)
    {
      bool foundStartList = false;
      bool foundStartNumber = false;

      List<int> tiles = new List<int>();
      StringBuilder builder = new StringBuilder();

      using (StreamReader reader = new StreamReader(inputFile))
      {
        while (!reader.EndOfStream)
        {
          string line = reader.ReadLine();

          // Get the width.
          if (line.ToLower().Contains("width"))
          {
            int index = line.LastIndexOf(' ');
            m_mapWidth = int.Parse(line.Substring(index + 1));
          }

          // Get the height.
          if (line.ToLower().Contains("height"))
          {
            int index = line.LastIndexOf(' ');
            m_mapHeight = int.Parse(line.Substring(index + 1));
          }
        }
      }

      // @TODO: Look for the {
      using (StreamReader reader = new StreamReader(inputFile))
      {
        while (!reader.EndOfStream)
        {
          char read = (char)reader.Read();

          if (read == '{')
          {
            foundStartList = true;
          }

          if (foundStartNumber)
          {
            if (read == ',' || read == '}')
            {
              string value = builder.ToString().Trim();
              int num = int.Parse(value, System.Globalization.NumberStyles.HexNumber);
              tiles.Add(num);

              foundStartNumber = false;
              builder.Clear();
            }
            else
            {
              builder.Append(read);
            }
          }

          if (foundStartList && read == 'x')
          {
            foundStartNumber = true;
          }
        }
      }

      return tiles.ToArray();
    } // ReadFile( inputFile )


    /// <summary>
    /// Writes the header file out to the string.
    /// </summary>
    public abstract string WriteHeaderFile(KeyValuePair<int[], int[]> compressedData, string filename);

    /// <summary>
    /// Writes the source file out to the string.
    /// </summary>
    public abstract string WriteSourceFile(KeyValuePair<int[], int[]> compressedData, string filename);

    /// <summary>
    /// Compresses the input list and returns the compressed result.
    /// </summary>
    public abstract KeyValuePair<int[], int[]> Compress(int[] tiles);

    /// <summary>
    /// Validates whether we can recreate the input list of tiles with the compressed data.
    /// </summary>
    /// <param name="tiles">The set to validate against.</param>
    /// <param name="compressedData">The compressed data to validate.</param>
    /// <returns>The number of inequalities, if zero data is valid.</returns>
    public abstract int Validate(int[] tiles, KeyValuePair<int[], int[]> compressedData);

    #endregion

  } // abstract class CompressionModel
} // namespace kchalupa.Wilderness.GBMapCompress.Models
