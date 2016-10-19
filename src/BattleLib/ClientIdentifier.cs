using System;

namespace BattleLib
{
    public class ClientIdentifier
    {
        private readonly Guid Guid;

        public ClientIdentifier()
        {
            Guid = Guid.NewGuid();
        }

        public bool IsPlayer { get; set; }
        public String Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var id = obj as ClientIdentifier;
            if (id == null)
                return false;

            return Equals(id);
        }

        public bool Equals(ClientIdentifier id)
        {
            return Guid.Equals(id.Guid);
        }

        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }
    }
}