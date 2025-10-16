using UnityEngine;

namespace WeitingZhao
{
    public enum HairLength
    {
        None = 0,    
        Long = 1,   
        Medium = 2, 
        Short = 3     
    }


    public enum HairCurl
    {
        None = 0,    
        Light = 1,   
        Medium = 2,  
        Heavy = 3    
    }



    public class HairStyle
    {

        public HairLength length = HairLength.None;
        public HairCurl curl = HairCurl.None;




        public HairStyle()
        {
            length = HairLength.None;
            curl = HairCurl.None;
        }



        public HairStyle(HairLength lengthValue, HairCurl curlValue)
        {
            length = lengthValue;
            curl = curlValue;
        }


        public override string ToString()
        {
            return $"Length: {GetLengthDescription()}, Curl: {GetCurlDescription()}";
        }


        public string GetLengthDescription()
        {
            switch (length)
            {
                case HairLength.None: return "None Cut";
                case HairLength.Long: return "Long";
                case HairLength.Medium: return "Medium";
                case HairLength.Short: return "Short";
                default: return "Unknown";
            }
        }


        public string GetCurlDescription()
        {
            switch (curl)
            {
                case HairCurl.None: return "None Curl";
                case HairCurl.Light: return "Light Curl";
                case HairCurl.Medium: return "Medium Curl";
                case HairCurl.Heavy: return "Heavy Curl";
                default: return "Unknown";
            }
        }


        public bool Equals(HairStyle other)
        {
            if (other == null) return false;
            return length == other.length && curl == other.curl;
        }
    }
}
