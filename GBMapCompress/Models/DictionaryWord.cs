using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kchalupa.WIlderness.GBMapCompress
{
  /// <summary>
  /// 
  /// </summary>
  public class DictionaryWord
  {

    #region fields

    /// <summary>
    /// The id.
    /// </summary>
    Guid m_id = Guid.NewGuid();

    #endregion

    #region construction

    /// <summary>
    /// 
    /// </summary>
    /// <param name="word"></param>
    /// <param name="count"></param>
    public DictionaryWord(int[] word, int count)
    {
      Word = word;
      Count = count;
    } // DictionaryWord( word, count )

    #endregion

    #region properties

    /// <summary>
    /// Gets the id.
    /// </summary>
    public Guid Id
    {
      get { return m_id; }
    } // Id


    /// <summary>
    /// Gets or sets the dictionary word.
    /// </summary>
    public int[] Word
    {
      get;
      set;
    } // Word


    /// <summary>
    /// Gets or sets the number of times this word appears in the list.
    /// </summary>
    public int Count
    {
      get;
      set;
    } // Count

    #endregion

  } // class DictionaryPair
} // namespace kchalupa.WIlderness.GBMapCompress
