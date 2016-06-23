using System;
using System.Collections;
using System.Diagnostics;

namespace CacheKeyTester
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.Write("Number of Tests: ");
      string numberOfTestsInput = Console.ReadLine();
      int numberOfTests;

      Console.Write("Number of Cache Items: ");
      string numberOfCacheItemsInput = Console.ReadLine();
      int numberOfCacheItems;

      if (int.TryParse(numberOfTestsInput, out numberOfTests) &&
          int.TryParse(numberOfCacheItemsInput, out numberOfCacheItems))
      {
        // Setup keyPrefix (item ID) and load up cache
        string keyPrefix = "{F2CB4028-0F98-4DAC-A154-28C06CAD6466}";

        Hashtable cache = new Hashtable();

        for (int i = 0; i < numberOfCacheItems; i++)
        {
          cache.Add(Guid.NewGuid() + "en-US|1", new object());
        }

        for (int i = 0; i < numberOfTests; i++)
        {
          // Start code that runs on each cache remove to JUST get cache keys to clear
          Stopwatch stopwatch = new Stopwatch();
          stopwatch.Start();

          // Sitecore.Caching.Cache.GetCacheKeys()
          ArrayList allKeys = new ArrayList(cache.Keys);

          // Sitecore.Caching.Cache.GetCacheKeys(string keyPrefix)
          ArrayList keysToClear = new ArrayList();

          foreach (object keyObject in allKeys)
          {
            var key = keyObject as string;

            if (key != null && (keyPrefix.Length == 0 || keyPrefix.StartsWith(key, StringComparison.InvariantCulture)))
            {
              keysToClear.Add(key);
            }
          }

          stopwatch.Stop();

          Console.WriteLine("Test #{0}: {1}ms", i + 1, stopwatch.ElapsedMilliseconds);
        }

        Console.WriteLine("Press 'Enter' to quit...");
        Console.ReadLine();
      }
    }
  }
}
