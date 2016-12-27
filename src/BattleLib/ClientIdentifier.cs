using System;

namespace BattleMode.Core
{
    public class ClientIdentifier
    {
        private readonly Guid _guid;

        public ClientIdentifier()
        {
            _guid = Guid.NewGuid();
        }

        public bool IsPlayer { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var id = obj as ClientIdentifier;
            return id != null && Equals(id);
        }

        public bool Equals(ClientIdentifier id)
        {
            return _guid.Equals(id._guid);
        }

        public override int GetHashCode()
        {
            return _guid.GetHashCode();
        }
    }
}