using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectRC4
{
  public class RC4
    {
        private int [] Gkey;
        private const int N = 256;
        private int[] S;
        private string key;
        private string text;
        private string chippertxt;
        private string keystream;
        private string generatedK;
        private string plainText;

        public string PlainText
        {
            get { return plainText; }
        }
        
      public RC4(string userKey, string plainText)
      {
          this.key = userKey;
          this.text = plainText;
        
      }
      //Key-scheduling algorithm
      private void KSA()
      {
          S = new int[N];
          int[] K = new int[N];
          int n = key.Length;
          for (int i = 0; i < N; i++)
          {
              K[i] = (int)key[i % n];
              S[i] = i;
          }

          int j = 0;
          for (int i = 0; i < N; i++)
          {
              j = (j + S[i] + K[i]) % N;
              int temp = S[i];
              S[i] = S[j];
              S[j] = temp;
          }
      }
      //random generation algorithm
      private void RGA()
      {
          KSA();
          int i = 0, j = 0, k = 0;
          Gkey = new int[text.Length];
          for (int a = 0; a < text.Length; a++)
          {
              i = (i + 1) % N;
              j = (j + S[i]) % N;
              int temp = S[i];
              S[i] = S[j];
             S[j] = temp;

              k = S[(S[i] + S[j]) % N];
              Gkey[a] = k;
             
          }
      }
      public void Encryption()
      {
          RGA();
          StringBuilder cipher = new StringBuilder();
          for (int i = 0; i < text.Length; i++)
          {
              int cipherBy = ((int)text[i]) ^Gkey[i];
              cipher.Append(Convert.ToChar(cipherBy));
          }
          chippertxt = StrToHexStr(cipher.ToString());
      }
      public void Decoding()
      {
          RGA();
          text = HexStrToStr(chippertxt);
          StringBuilder cipher = new StringBuilder();
          for (int i = 0; i < text.Length; i++)
          {
              int cipherBy = ((int)text[i]) ^ Gkey[i];
              cipher.Append(Convert.ToChar(cipherBy));
          }
         plainText = (cipher.ToString());
      }
      public static string HexStrToStr(string hexStr)
      {
          StringBuilder sb = new StringBuilder();
          for (int i = 0; i < hexStr.Length; i += 2)
          {
              int n = Convert.ToInt32(hexStr.Substring(i, 2), 16);
              sb.Append(Convert.ToChar(n));
          }
          return sb.ToString();
      }
      public static string StrToHexStr(string str)
      {
          StringBuilder sb = new StringBuilder();
          for (int i = 0; i < str.Length; i++)
          {
              int v = Convert.ToInt32(str[i]);
              sb.Append(string.Format("{0:X2}", v));
          }
          return sb.ToString();
      }
      public string KeyStream
      {
          get
          {
              for (int i = 0; i < Gkey.Length; i++)
              {
                  keystream += Gkey[i].ToString("X2");
              }
              return keystream;
          }
      } 
      public string Text
      {
          get { return text; }
          set { text = value; }
      }
      public string ChipperText
      {
          get { return chippertxt; }
          set { chippertxt = value; }
      }
      public string GeneratedKey
      {
          get {
              for (int i = 0; i < Gkey.Length; i++)
              {
                  generatedK += Gkey[i].ToString()+"-";
              }
              
              return generatedK.Remove(generatedK.Length-1); }

      }
    }
}
