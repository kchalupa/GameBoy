        using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using kchalupa.WIlderness.GBMapCompress;

namespace kchalupa.Wilderness.GBMapCompress.Models
{
  /// <summary>
  /// Compression Model.
  /// </summary>
  public class DictionaryCompressionModel : CompressionModel
  {

    #region fields

    /// <summary>
    /// The block size.
    /// </summary>
    private int m_blockSize = 5;

    #endregion

    #region properties

    /// <summary>
    /// Gets or sets the block size.
    /// </summary>
    public int BlockSize
    {
      get { return m_blockSize; }
      set { m_blockSize = value; }
    } // BlockSize

    #endregion

    #region methods

    /// <summary>
    /// Writes the header file out to the string.
    /// </summary>
    public override string WriteHeaderFile(KeyValuePair<int[], int[]> compressedData, string filename)
    {
      int size = compressedData.Key.Length + compressedData.Value.Length;
      StringBuilder builder = new StringBuilder();

      builder.AppendLine("/**");
      builder.AppendLine();
      builder.AppendLine(filename.ToUpper() + ".h");
      builder.AppendLine();
      builder.AppendLine("Map Header File.");
      builder.AppendLine();
      builder.AppendLine("Info:");
      builder.AppendLine("  Map Size: " + MapWidth + " x " + MapHeight);
      builder.AppendLine("  Size in Bytes: " + size);
      builder.AppendLine();
      builder.AppendLine("*/");
      builder.AppendLine();
      //writer.AppendLine("#define " + output + "_Algorithm " + 1);
      builder.AppendLine("#define " + filename + "_Width " + MapWidth);
      builder.AppendLine("#define " + filename + "_Height " + MapHeight);
      builder.AppendLine("#define " + filename + "_BlockSize " + m_blockSize);
      builder.AppendLine("#define " + filename + "_LayoutLength " + compressedData.Key.Length);
      builder.AppendLine("#define " + filename + "_EntriesLength " + compressedData.Value.Length * m_blockSize);
      builder.AppendLine();
      builder.AppendLine("extern unsigned char " + filename + "_Layout[];");
      builder.AppendLine("extern unsigned char " + filename + "_Entries[];");

      return builder.ToString();
    } // WriteHeaderFile( compressedData, filename )


    /// <summary>
    /// Writes the source file out to the string.
    /// </summary>
    public override string WriteSourceFile(KeyValuePair<int[], int[]> compressedData, string filename)
    {
      int size = compressedData.Key.Length + compressedData.Value.Length;

      StringBuilder builder = new StringBuilder();

      builder.AppendLine("/**");
      builder.AppendLine();
      builder.AppendLine(filename.ToUpper() + ".c");
      builder.AppendLine();
      builder.AppendLine("Map Source File.");
      builder.AppendLine();
      builder.AppendLine("Info:");
      builder.AppendLine("  Map Size: " + MapWidth + " x " + MapHeight);
      builder.AppendLine("  Size in Bytes: " + size);
      builder.AppendLine("*/");
      builder.AppendLine();
      //writer.AppendLine("#define " + output + "_Algorithm " + 1);
      builder.AppendLine("#define " + filename + "_Width " + MapWidth);
      builder.AppendLine("#define " + filename + "_Height " + MapHeight);
      builder.AppendLine("#define " + filename + "_BlockSize " + m_blockSize);
      builder.AppendLine("#define " + filename + "_LayoutLength " + compressedData.Key.Length);
      builder.AppendLine("#define " + filename + "_EntriesLength " + compressedData.Value.Length);

      builder.AppendLine();

      builder.AppendLine("unsigned char " + filename + "_Layout[] =");
      builder.AppendLine("{");
      builder.Append("  ");

      for (int i = 0; i < compressedData.Key.Length; ++i)
      {
        if (i % 8 == 0 && i != 0)
        {
          // After X entries next line.
          builder.AppendLine();
          builder.Append("  ");
        }

        builder.Append("0x" + compressedData.Key[i].ToString("X2"));

        if (i < compressedData.Key.Length - 1)
        {
          builder.Append(",");
        }
      }

      builder.AppendLine();
      builder.AppendLine("};");
      builder.AppendLine();
      builder.AppendLine("unsigned char " + filename + "_Entries[] =");
      builder.AppendLine("{");
      builder.Append("  ");

      for (int i = 0; i < compressedData.Value.Length; ++i)
      {
        if (i % 8 == 0 && i != 0)
        {
          // After X entries next line.
          builder.AppendLine();
          builder.Append("  ");
        }

        builder.Append("0x" + compressedData.Value[i].ToString("X2"));

        if (i < compressedData.Value.Length - 1)
        {
          builder.Append(",");
        }
      }

      builder.AppendLine();
      builder.AppendLine("};");

      return builder.ToString();
    } // WriteSourceFile( compressedData, filename )


    /// <summary>
    /// Compress the map.
    /// </summary>
    /// <param name="tiles"></param>
    /// <returns></returns>
    public override KeyValuePair<int[], int[]> Compress(int[] map)
    {
      DictionaryWord[] uniqueWords = RankDictionaryEntries(map);

      List<int> layout = new List<int>();
      int[] entry = new int[BlockSize];

      for(int i=0;i<map.Length / BlockSize;++i)
      {
        Array.Copy(map, i * BlockSize, entry, 0, BlockSize);
        for(int j=0;j<uniqueWords.Length;++j)
        {
          if(uniqueWords[j].Word.SequenceEqual(entry))
          {
            layout.Add(j);
            break;
          }
        }
      }

      // Build up the final array with all the unique words in their ranked order.
      int[] words = new int[uniqueWords.Length * m_blockSize];
      for(int i=0;i<uniqueWords.Length;++i)
      {
        Array.Copy(uniqueWords[i].Word, 0, words, i * BlockSize, BlockSize);
      }

      // Return the compressed information.
      return new KeyValuePair<int[], int[]>(layout.ToArray(), words.ToArray());
    } // Compress()


    /// <summary>
    /// Validate the compressed map against the orignal source map.
    /// </summary>
    /// <param name="tiles">The source map.</param>
    /// <param name="encoding">The compressed map.</param>
    /// <returns>The number of discrepencies.</returns>
    public override int Validate(int[] tiles, KeyValuePair<int[], int[]> encoding)
    {
      // --------------------------------------
      // Validate the compressed map.
      // --------------------------------------

      int[] map = new int[MapWidth * MapHeight];
      for(int i=0;i<encoding.Key.Length;++i)
      {
        Array.Copy(encoding.Value, encoding.Key[i] * BlockSize, map, i * BlockSize, BlockSize);
      }


      int errors = 0;
      for (int i = 0; i < tiles.Length; ++i)
      {
        // validate that we can reconstruct the original map.
        if (tiles[i] != map[i])
        {
          ++errors;
        }
      }

      return errors;
    } // Validate( tiles, encoding )


    /// <summary>
    /// 
    /// </summary>
    /// <param name="map"></param>
    /// <returns></returns>
    protected virtual DictionaryWord[] RankDictionaryEntries(int[] map)
    {
      List<DictionaryWord> uniqueWords = new List<DictionaryWord>();

      // The word 
      int[] entry = new int[m_blockSize];

      // Find all the unique dictionary "words".
      for (int i = 0; i < map.Length / BlockSize; ++i)
      {
        Array.Copy(map, i * BlockSize, entry, 0, BlockSize);
        DictionaryWord word = IsWordExist(uniqueWords.ToArray(), entry);
        if(word == null)
        {
          word = new DictionaryWord((int[])entry.Clone(), 1);
          uniqueWords.Add(word);
        }
        else
        {
          word.Count++;
        }
      }

      var sorted = from p in uniqueWords orderby p.Count descending select p;
      return sorted.ToArray();
    } // RankDictionaryEntries(int[] map)


    /// <summary>
    /// Determines whether this word exists and if so returns it, otherwise null.
    /// </summary>
    protected virtual DictionaryWord IsWordExist(DictionaryWord[] words, int[] entry)
    {
      foreach (DictionaryWord word in words)
      {
        bool fail = false;
        for(int i=0;i<BlockSize;++i)
        {
          if(entry[i] != word.Word[i])
          {
            fail = true;
            break;
          }
        }

        if(!fail)
        {
          return word;
        }
      }

      return null;
    } // IsWordExist(int[] word)

    #endregion

  } // class DictionaryCompressModel
} // namespace kchalupa.Wilderness.GBMapCompress.Models
