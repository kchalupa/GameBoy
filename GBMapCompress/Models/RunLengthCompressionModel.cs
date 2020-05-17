using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kchalupa.Wilderness.GBMapCompress.Models
{
  /// <summary>
  /// Attempts to compress the map information using run-length encoding.
  /// </summary>
  public class RunLengthCompressionModel : CompressionModel
  {

    #region methods

    /// <summary>
    /// Writes the encoded contents out to a file.
    /// </summary>
    /// <param name="outputFile">The file to write to.</param>
    /// <param name="encoding">The encoded contents.</param>
    public override void WriteFile(string outputFile, KeyValuePair<int[], int[]> encoding)
    {
      int size = encoding.Key.Length + encoding.Value.Length;

      string output = Path.GetFileNameWithoutExtension(outputFile);
      using (StreamWriter writer = new StreamWriter(Path.Combine(Path.GetDirectoryName(outputFile), Path.GetFileNameWithoutExtension(outputFile) + ".c")))
      {
        writer.WriteLine("/**");
        writer.WriteLine();
        writer.WriteLine(output.ToUpper() + ".c");
        writer.WriteLine();
        writer.WriteLine("Map Source File.");
        writer.WriteLine();
        writer.WriteLine("Info:");
        writer.WriteLine("  Map Size: " + MapWidth + " x " + MapHeight);
        writer.WriteLine("  Size in Bytes: " + size);
        writer.WriteLine("*/");
        writer.WriteLine();
        writer.WriteLine("#define " + output + "Algorithm " + 2);
        writer.WriteLine("#define " + output + "Width " + MapWidth);
        writer.WriteLine("#define " + output + "Height " + MapHeight);
        writer.WriteLine("#define " + output + "LayoutLength " + encoding.Key.Length);
        writer.WriteLine("#define " + output + "EntriesLength " + encoding.Value.Length);

        writer.WriteLine();

        writer.WriteLine("unsigned char " + output + "Layout[] =");
        writer.WriteLine("{");
        writer.Write("  ");

        for (int i = 0; i < encoding.Key.Length; ++i)
        {
          if (i % 8 == 0 && i != 0)
          {
            // After 8 entries next line.
            writer.WriteLine();
            writer.Write("  ");
          }

          writer.Write("0x" + encoding.Key[i].ToString("X2"));

          if (i < encoding.Key.Length - 1)
          {
            writer.Write(",");
          }
        }

        writer.WriteLine();
        writer.WriteLine("};");
        writer.WriteLine();
        writer.WriteLine("unsigned char " + output + "Entries[] =");
        writer.WriteLine("{");
        writer.Write("  ");

        for (int i = 0; i < encoding.Value.Length; ++i)
        {
          if (i % 8 == 0 && i != 0)
          {
            // After 8 entries next line.
            writer.WriteLine();
            writer.Write("  ");
          }

          writer.Write("0x" + encoding.Value[i].ToString("X2"));

          if (i < encoding.Value.Length - 1)
          {
            writer.Write(",");
          }
        }

        writer.WriteLine("};");

        writer.Close();
      }

      using (StreamWriter writer = new StreamWriter(Path.Combine(Path.GetDirectoryName(outputFile), Path.GetFileNameWithoutExtension(outputFile) + ".h")))
      {
        writer.WriteLine("/**");
        writer.WriteLine();
        writer.WriteLine(output.ToUpper() + ".h");
        writer.WriteLine();
        writer.WriteLine("Map Header File.");
        writer.WriteLine();
        writer.WriteLine("Info:");
        writer.WriteLine("  Map Size: " + MapWidth + " x " + MapHeight);
        writer.WriteLine("  Size in Bytes: " + size);
        writer.WriteLine();
        writer.WriteLine("*/");
        writer.WriteLine();
        writer.WriteLine("#ifndef _" + output.ToUpper() + "_H");
        writer.WriteLine("#define _" + output.ToUpper() + "_H");
        writer.WriteLine();
        writer.WriteLine("#define " + output + "Algorithm " + 2);
        writer.WriteLine("#define " + output + "Width " + MapWidth);
        writer.WriteLine("#define " + output + "Height " + MapHeight);
        writer.WriteLine("#define " + output + "LayoutLength " + encoding.Key.Length);
        writer.WriteLine("#define " + output + "OccurancesLength " + encoding.Value.Length);
        writer.WriteLine();
        writer.WriteLine("extern unsigned char " + output + "Layout[];");
        writer.WriteLine();
        writer.WriteLine("extern unsigned char " + output + "Entries[];");
        writer.WriteLine();
        writer.WriteLine("#endif");

        writer.Close();
      }
    } // WriteFile( outputFile, encoding )


    /// <summary>
    /// Compress the map.
    /// </summary>
    /// <param name="map"></param>
    /// <returns></returns>
    public override KeyValuePair<int[], int[]> Compress(int[] map)
    {
      List<int> layout = new List<int>();
      List<int> occurances = new List<int>();

      // How many times has this number appeared in a row.
      int repeatCount = 0;

      // Save the previous tile number to compare against the current tile number.  We set it to map[0] so that we're guaranteed to have one match.
      int previous = map[0];

      for(int i=0;i<map.Length;++i)
      {
        if(previous != map[i])
        {
          if(repeatCount == 1)
          {
            // Set the most significant bit to denote that it only appears once at that position.
            layout.Add(previous | 128);
          }
          else
          {
            layout.Add(previous);
            occurances.Add(repeatCount);
            repeatCount = 1;
          }
        }
        else
        {
          if(repeatCount >= MapWidth)
          {
            layout.Add(previous);
            occurances.Add(repeatCount);
            repeatCount = 0;
          }
          else
          {
            ++repeatCount;
          }
        }

        if(i == map.Length - 1)
        {
          // if this is the final result, write it out.
          layout.Add(previous);
          occurances.Add(repeatCount);
        }

        previous = map[i];
      }

      return new KeyValuePair<int[], int[]>(layout.ToArray(), occurances.ToArray());
    } // Compress( map )


    /// <summary>
    /// Validates the encoded data.
    /// </summary>
    /// <param name="encoding"></param>
    /// <returns>The number of inequalities, if zero we were able to recreate the map.</returns>
    public override int Validate(int[] map, KeyValuePair<int[], int[]> encoding)
    {
      int[,] tiles = new int[MapHeight, MapWidth];

      int x = 0, y = 0;
      int occurancesPosition = 0;

      for(int i=0;i<encoding.Key.Length;++i)
      {
        if (x >= MapWidth)
        {
          x = 0;
          ++y;
        }

        if ((encoding.Key[i] & 0x80) == 0x80)
        {
          byte tile = (byte)encoding.Key[i];

          // Zero out the most significant bit.
          tile <<= 1;
          tile >>= 1;

          tiles[y, x++] = tile;

          if (x >= MapWidth)
          {
            x = 0;
            ++y;
          }
        }
        else
        {
          RenderRunLengthSegment(ref x, ref y, encoding.Key[i], encoding.Value[occurancesPosition++], ref tiles);
        }
      }

      int[] result = new int[MapHeight * MapWidth];

      for(y=0;y<MapHeight;++y)
      {
        for(x=0;x<MapWidth;++x)
        {
          result[y * MapHeight + x] = tiles[y, x];
        }
      }

      int errors = 0;
      for(int i=0;i<map.Length;++i)
      {
        if(map[i] != result[i])
        {
          ++errors;
        }
      }

      return errors;
    } // Validate( encoding )


    /// <summary>
    /// Renders a segment of the run length compressed map to the map array.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="linePosition"></param>
    /// <param name="numLines"></param>
    /// <param name="tile"></param>
    /// <param name="occurances"></param>
    /// <param name="map"></param>
    /// <returns></returns>
    void RenderRunLengthSegment(ref int x, ref int y, int tile, int occurances, ref int[,] map)
    {
      int remaining = MapWidth - x;
      if(occurances > remaining)
      {
        occurances -= remaining;
        for(int i=0;i<remaining;++i)
        {
          map[y, x + i] = tile;
        }

        // Set the line position to 0 and increment the row.
        x = 0;
        ++y;

        if (occurances > 0)
        {
          RenderRunLengthSegment(ref x, ref y, tile, occurances, ref map);
        }
      }
      else
      {
        for(int i=0;i<occurances;++i)
        {
          map[y, x++] = tile;
        }
      }
    } // RenderRunLengthSegment( x, y, linePosition, numLines, tile, occurances, map )

    #endregion

  } // class RunLengthCompressionModel
} // namespace kchalupa.Wilderness.GBMapCompress.Models
