using HashidsNet;

namespace OpenTodo.Utils
{
    public class HashID
    {
        private readonly Hashids Hash;
        public HashID()
        {
            Hash = new Hashids("Open todo is the best todo app ever!", 12);
        }

        public string GenerateHash(int id)  {
           var hash = Hash.Encode(id);
            return hash;
        }

        public int ReverseHash(string hash)
        {
            return Hash.Decode(hash)[0];
        }


    }
}
